using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    public static bool playerIsLost = false;
	public static bool isJitting = false;

    public GameObject[] largeAsteroids;
    public GameObject asteroidPrefab;
    public GameObject largeAlienFab;
    public GameObject smallAlienFab;
    public GameObject bossAlienFab;
    public GameObject playerPrefab;
    public GameObject perkPowerUp;
    public GameObject deathUI;
	public ScreenJitter jitter;

    public AudioSource staticAudioSource;
    public AudioClip staticClip;

	public float jitVal = 0.2f;
	public float minJit = 0.0f;
	public float maxScreenJit = 0.6f;
	public float maxHoriJit = 0.1f;
	public float maxcoloraAb = 0.1f;
	public float timer = 6f;

	Camera cam;
    float camDistance;
    readonly float buffer = 0.4f;
    int amount;

    public static float leftLimit = Screen.width;
    public static float rightLimit = Screen.width;
    public static float bottomLimit = Screen.height;
    public static float topLimit = Screen.height;

    //private bool perkActive = false;
    private bool isStatic = false;
    private int sceneIndex;

    void Start()
    {
        staticAudioSource = GameObject.Find("StaticAudioSource").GetComponent<AudioSource>();
        cam = Camera.main;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Calculates the positiion of the camera relative to the game object
        camDistance = Mathf.Abs(cam.transform.position.z + transform.position.z);

        //calculates the border constraints based on the game's worldspace coordinates
        leftLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camDistance)).x;
        rightLimit = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, camDistance)).x;
        bottomLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camDistance)).y;
        topLimit = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, camDistance)).y;

        switch(sceneIndex)
        {
            case 1:
                //Spawn Design Level Aliens
                InvokeRepeating("SpawnAlien", 7f, 7f);
                InvokeRepeating("SpawnPowerUp", 15f, 20f);
                InvokeRepeating("SpawnBoss", 60f, 70f);
                break;
            case 2:
                //Spawn Design Level Aliens
                InvokeRepeating("SpawnAlien", 7f, 7f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneIndex == 1 || sceneIndex == 2)
        {
            if (!playerIsLost)
            {
                //Spawning the Asteroids
                largeAsteroids = GameObject.FindGameObjectsWithTag("LargeAsteroid");
                amount = largeAsteroids.Length;

                //respawning asteroid
                if (amount != 3 && amount <= 3) SpawnAsteroids();
                //respawning player
                if (GameObject.FindGameObjectWithTag("Player") == null && PlayerLifes.numofLifes > 0) SpawnPlayer();
            } else
            {
                Invoke("ActivateDeathScreen", 3f);
            }
        } else if (sceneIndex == 0)
        {
            //Spawning the Asteroids
            largeAsteroids = GameObject.FindGameObjectsWithTag("LargeAsteroid");
            amount = largeAsteroids.Length;

            if (amount != 3 && amount <= 3) SpawnAsteroids();
        }

        //If the sceneIndex is the designed level then JitterController can be triggered
        if(sceneIndex == 1) JitterController();
	}

    //Activates the UI Panel contining the UI during death
    private void ActivateDeathScreen()
    {
        Cursor.visible = true;
        deathUI.SetActive(true);
    }

    //Spawn Methods
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
       
        if (GameObject.FindGameObjectsWithTag("Alien").Length == 0)
        {
            int tempVal = Random.Range(0, 10);
            if (tempVal >= 5)
            {
                Instantiate(largeAlienFab, new Vector3(Random.Range(rightLimit, rightLimit + buffer), Random.Range(topLimit, bottomLimit), 0), Quaternion.identity);
            } else
            {
                Instantiate(smallAlienFab, new Vector3(Random.Range(rightLimit, rightLimit + buffer), Random.Range(topLimit, bottomLimit), 0), Quaternion.identity);
            }
        }
    }

    private void SpawnPowerUp()
    {
        if (GameObject.FindGameObjectsWithTag("allowPass").Length == 0)
        {
            Instantiate(perkPowerUp, new Vector2(Random.Range(leftLimit, rightLimit), Random.Range(topLimit, bottomLimit)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //perkActive = true;
        }
    }

    private void SpawnBoss()
    {
        if (GameObject.Find("SpecialBoss") == null && GameObject.Find("SpecialBoss(Clone)") == null)
        {
            Instantiate(bossAlienFab, new Vector3(Random.Range(rightLimit, rightLimit + buffer), Random.Range(topLimit, bottomLimit), 0), Quaternion.identity);

        }
    }

    //This method activates and deactivates the triggers associated with the Static Jitter
	private void JitterController()
	{
		if (isJitting == true)
        {
            if (isStatic == false)
            {
                staticAudioSource.Play();
                staticAudioSource.volume = 1f;
                isStatic = true;
            }
            jitter.scanLineJitter = maxScreenJit;
			jitter.horizontalJitter = maxHoriJit;
			jitter.colorAberrate = maxcoloraAb;
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				isJitting = false;
				timer = 6f;
			}
		}
		else
		{
            //Fades out the Static Jitter
            StaticFadeOut();
            jitter.scanLineJitter = Mathf.Clamp(jitter.scanLineJitter - jitVal * Time.deltaTime, minJit, maxScreenJit);
			jitter.horizontalJitter = Mathf.Clamp(jitter.horizontalJitter - jitVal * Time.deltaTime, minJit, maxHoriJit);
			jitter.colorAberrate = Mathf.Clamp(jitter.colorAberrate - jitVal * Time.deltaTime, minJit, maxcoloraAb);
		}
	}

    //Method for modifying the static audio associated with the Static Jittering effect.
    private void StaticFadeOut()
    {
        if (staticAudioSource.volume > 0)
        {
            staticAudioSource.volume -= 0.3f * Time.deltaTime;
        } else if (staticAudioSource.volume <= 0)
        {
            staticAudioSource.Stop();
            isStatic = false;
        }
    }
}
