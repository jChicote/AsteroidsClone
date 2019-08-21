using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

    public GameObject[] largeAsteroids;
    public GameObject asteroidPrefab;

    Camera cam;
    float camDistance;
    readonly float buffer = 0.4f;
    int amount;

    float leftLimit = Screen.width;
    float rightLimit = Screen.width;
    float bottomLimit = Screen.height;
    float topLimit = Screen.height;

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

    // Update is called once per frame
    void Update()
    {
        largeAsteroids = GameObject.FindGameObjectsWithTag("LargeAsteroid");
        amount = largeAsteroids.Length;

        if (amount != 3 && amount <= 3)
        {
            SpawnAsteroids();
        }
    }

    void SpawnAsteroids()
    {
        Instantiate(asteroidPrefab, new Vector3(Random.Range(rightLimit, rightLimit + buffer), Random.Range(topLimit, bottomLimit),0), Quaternion.Euler(0,0,Random.Range(0, 360)));
    }
}
