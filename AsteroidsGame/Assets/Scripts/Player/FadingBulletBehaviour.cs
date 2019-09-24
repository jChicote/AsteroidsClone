using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingBulletBehaviour : MonoBehaviour
{
    public float bulletVelocity = 15.0f;

    private Rigidbody2D bulletRigid;

    void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletRigid.velocity = transform.up * (bulletVelocity + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetVelocity);
        Invoke("TimedDestroy", 0.5f);
    }

    void Update()
    {

    }

    //enters trigger when collision is detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LargeAsteroid" || collision.gameObject.tag == "SmallAsteroid" || collision.gameObject.tag == "Alien")
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
