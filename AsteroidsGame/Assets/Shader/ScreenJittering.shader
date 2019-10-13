Shader "Hidden/ScreenJittering"
{
    Properties
    {
        _MainTex ("-", 2D) = "" {}
    }
    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    float2 _MainTex_TexelSize;

    float2 _ScanLineJitter; // (displacement, threshold)
    float _HorizontalShake;
    float2 _ColorAberration;// (amount, time) [Unused]

    float nrand(float x, float y)
    {
        return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 43758.5453);
    }

    //v2f_img struct for image effects, 
    half4 frag(v2f_img i) : SV_Target
    {
        float u = i.uv.x;
        float v = i.uv.y;

        // Scan line jitter
        float jitter = nrand(v, _Time.x) * 2 - 1;
        jitter *= step(_ScanLineJitter.y, abs(jitter)) * _ScanLineJitter.x; //a = vector ref, x = scalar 

        // Vertical jump
        float jump = lerp(v, frac(v + 0), 0);

        // Horizontal shake
        float shake = (nrand(_Time.x, 2) - 0.5) * _HorizontalShake;

        // Color drift (Unused but necessary in the frac calculation
        float drift = sin(jump + _ColorAberration.y) * _ColorAberration.x;


        //Calculates the medium precision vector from the frac decimal value of calculated jitter values
        half4 src1 = tex2D(_MainTex, frac(float2(u + jitter + shake, jump)));
        half4 src2 = tex2D(_MainTex, frac(float2(u + jitter + shake + drift, jump)));

        //return half4 vect to pass
        return half4(src1.r, src2.g, src1.b, 1);
    }

    ENDCG
    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0
            ENDCG
        }
    }
}
