using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public GameObject[] largeAsteroids;
    public int amount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        largeAsteroids = GameObject.FindGameObjectsWithTag("LargeAsteroids");
        amount = largeAsteroids.Length;

        if (amount != 11)
        {
            InvokeRepeating("spawnAsteroids", 1f, 2f);
        }
    }

    void SpawnAsteroids()
    {

    }
}
