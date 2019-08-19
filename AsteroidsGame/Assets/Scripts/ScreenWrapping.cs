using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    float constraintLeft = Screen.width;
    float constraintRight = Screen.width;
    float bottomConstraint = Screen.height;
    float topConstraint = Screen.height;
    float buffer = 1.0f;

    Camera cam;
    float distanceZ;
    
    void Start()
    {
        cam = Camera.main;

        //Calculates the positiion of the camera relative to the game object
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);

        //calculates the border constraints based on the game's worldspace coordinates
        constraintLeft = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        constraintRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
    }

    private void FixedUpdate()
    {
        if(transform.position.x < constraintLeft - buffer)
        {
            transform.position = new Vector2(constraintRight - 0.10f, transform.position.y);
        } else if (transform.position.x > constraintRight)
        {
            transform.position = new Vector2(constraintLeft, transform.position.y);
        } else if (transform.position.y < bottomConstraint - buffer)
        {
            transform.position = new Vector2(transform.position.x, topConstraint + buffer);
        }
        else if (transform.position.y > topConstraint + buffer)
        {
            transform.position = new Vector2(transform.position.x, bottomConstraint - buffer);
        }
    }
}
