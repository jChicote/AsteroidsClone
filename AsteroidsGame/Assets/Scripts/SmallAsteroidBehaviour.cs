using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroidBehaviour : MonoBehaviour
{
    public float velocity = 7.0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.up * velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
        }
    }
}
