using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletVelocity = 10.0f;

    private Rigidbody2D bulletRigid;

    void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletRigid.velocity = transform.up * bulletVelocity;
        Invoke("TimedDestroy", 3);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LargeAsteroid" || collision.gameObject.tag == "Alien")
        {
            Destroy(gameObject);
        }
    }

    //Destroys if havn't collided with anything
    private void TimedDestroy()
    {
        Destroy(gameObject);
    }
}
