using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static bool playerIsLost = false;
    public static bool isJitting = false;

    public GameObject[] largeAsteroids;
    public GameObject asteroidPrefab;
    public GameObject largeAlienFab;
    public GameObject smallAlienFab;
    public GameObject playerPrefab;
<<<<<<< Updated upstream
=======
    public GameObject perkPowerUp;
    public ScreenJitter jitter;

    public float jitVal = 0.2f;
    public float minJit = 0.0f;
    public float maxScreenJit = 0.2f;
    public float maxHoriJit = 0.2f;
    public float maxcoloraAb = 0.2f;
    public float timer = 2f;
>>>>>>> Stashed changes

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
        jitter = cam.GetComponent<ScreenJitter>();

        //Calculates the positiion of the camera relative to the game object
        camDistance = Mathf.Abs(cam.transform.position.z + transform.position.z);

        //calculates the border constraints based on the game's worldspace coordinates
        leftLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camDistance)).x;
        rightLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, camDistance)).x;
        bottomLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camDistance)).y;
        topLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, camDistance)).y;

        //Spawn alien after every 7 seconds
        InvokeRepeating("SpawnAlien", 7f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsLost)
        {
            largeAsteroids = GameObject.FindGameObjectsWithTag("LargeAsteroid");
            amount = largeAsteroids.Length;

            if (amount != 3 && amount <= 3)
            {
                SpawnAsteroids();
            }

            //respawning player
            if (GameObject.FindGameObjectWithTag("Player") == null && PlayerLifes.numofLifes > 0)
            {
                Debug.Log("Spawning player at lives " + PlayerLifes.numofLifes);
                SpawnPlayer();
            }
        }
        JitterController();
    }

    private void SpawnAsteroids()
    {
        Instantiate(asteroidPrefab, new Vector3(Random.Range(rightLimit, rightLimit + buffer), Random.Range(topLimit, bottomLimit), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(topLimit, bottomLimit), 0), Quaternion.identity);
    }

    private void SpawnAlien()
    {
       
        if (GameObject.FindGameObjectsWithTag("Alien").Length != 1)
        {
            int tempVal = Random.Range(0, 10);
            if (tempVal >= 5)
            {
                Instantiate(largeAlienFab, new Vector3(Random.Range(rightLimit, rightLimit + buffer), Random.Range(topLimit, bottomLimit), 0), Quaternion.identity);
            } else
            {
                Debug.Log("Small Alien Spawned)");
                Instantiate(smallAlienFab, new Vector3(Random.Range(rightLimit, rightLimit + buffer), Random.Range(topLimit, bottomLimit), 0), Quaternion.identity);
            }
        }
    }
<<<<<<< Updated upstream
=======

    private void SpawnPowerUp()
    {
        if (perkActive == false)
        {
            Instantiate(perkPowerUp, new Vector2(Random.Range(leftLimit, rightLimit), Random.Range(topLimit, bottomLimit)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            perkActive = true;
        }
    }

    private void JitterController()
    {
        if (isJitting == true)
        {
            jitter.scanLineJitter = maxScreenJit;
            jitter.horizontalJitter = maxHoriJit;
            jitter.colorAberrate = maxcoloraAb;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isJitting = false;
                timer = 2f;
            }
        } else
        {
            jitter.scanLineJitter = Mathf.Clamp(jitter.scanLineJitter - jitVal * Time.deltaTime, minJit, maxScreenJit);
            jitter.horizontalJitter = Mathf.Clamp(jitter.horizontalJitter - jitVal * Time.deltaTime, minJit, maxHoriJit);
            jitter.colorAberrate = Mathf.Clamp(jitter.colorAberrate - jitVal * Time.deltaTime, minJit, maxcoloraAb);
        }
    }
>>>>>>> Stashed changes
}
