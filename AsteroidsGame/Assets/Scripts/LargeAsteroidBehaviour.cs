using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAsteroidBehaviour : MonoBehaviour
{
    public float asteroidVel = 4.0f;

    private Rigidbody2D asteroidRB;

    void Start()
    {
        asteroidRB = GetComponent<Rigidbody2D>();
        asteroidRB.velocity = transform.up * asteroidVel;
    }

    
    void Update()
    {
        
    }
}
