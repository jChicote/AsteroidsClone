using UnityEngine;

//This executes the following in edit mode
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Screen Jitter Effect")]

public class ScreenJitter : MonoBehaviour
{
    //region organisers
    #region Public Properties

    // Line Jitter with public slider

    [SerializeField, Range(0, 1)]
    float _scanLineJitter = 0;

    public float scanLineJitter
    {
        get { return _scanLineJitter; }
        set { _scanLineJitter = value; }
    }

    // Horizontal Jitter with public slider

    [SerializeField, Range(0, 1)]
    float _horizontalJitter = 0;

    public float horizontalJitter
    {
        get { return _horizontalJitter; }
        set { _horizontalJitter = value; }
    }

    // Color Abberation with public slider

    [SerializeField, Range(0, 1)]
    float _colorAberration = 0;

    public float colorAberrate
    {
        get { return _colorAberration; }
        set { _colorAberration = value; }
    }

    #endregion

    #region Private Properties

    [SerializeField] Shader _shader;

    Material _material;

    float _verticalJumpTime;

    #endregion

    #region MonoBehaviour Functions

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null)
        {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }

        var sl_thresh = Mathf.Clamp01(1.0f - _scanLineJitter * 1.2f);
        var sl_disp = 0.002f + Mathf.Pow(_scanLineJitter, 3) * 0.05f;
        _material.SetVector("_ScanLineJitter", new Vector2(sl_disp, sl_thresh));


        _material.SetFloat("_HorizontalShake", _horizontalJitter * 0.2f);

        var cd = new Vector2(_colorAberration * 0.04f, Time.time * 606.11f);
        _material.SetVector("_ColorAberration", cd);

        Graphics.Blit(source, destination, _material);
    }

    #endregion
}
