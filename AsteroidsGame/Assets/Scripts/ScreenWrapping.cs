using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    Camera cam;
    float camDistance;
    float leftLimit = Screen.width;
    float rightLimit = Screen.width;
    float bottomLimit = Screen.height;
    float topLimit = Screen.height;

    public float buffer = 0.4f;

    void Start()
    {
        cam = Camera.main;

        //Calculates the positiion of the camera relative to the game object
        camDistance = Mathf.Abs(cam.transform.position.z + transform.position.z);

        //calculates the border constraints based on the game's worldspace coordinates
        leftLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camDistance)).x;
        rightLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, camDistance)).x;
        bottomLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camDistance)).y;
        topLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, camDistance)).y;
    }

    private void FixedUpdate()
    {
        if(transform.position.x < leftLimit - buffer)
            transform.position = new Vector2(rightLimit - 0.10f, transform.position.y);

        else if (transform.position.x > rightLimit + buffer)
            transform.position = new Vector2(leftLimit, transform.position.y);

        else if (transform.position.y < bottomLimit - buffer)
            transform.position = new Vector2(transform.position.x, topLimit + buffer);

        else if (transform.position.y > topLimit + buffer + buffer)
            transform.position = new Vector2(transform.position.x, bottomLimit - buffer);
    }
}
