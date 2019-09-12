﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBehaviour : MonoBehaviour
{
    private float speed = 1f;
    private int collisionCount;

    void Start()
    {
        speed = Random.Range(1, 3);
    }

    void Update()
    {
        //speed *= 0.90f;
        transform.position += transform.up * speed * Time.deltaTime;

        Destroy(gameObject, 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionCount++;
        if (collisionCount==3)
        {
            Destroy(gameObject);
        }
    }
}
