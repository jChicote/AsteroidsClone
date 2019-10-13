using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class defines begaviour of the powerful bullet which is the 'WHITE' round fired in three directions

public class powerFulBullet : MonoBehaviour
{
    public float bulletVelocity = 15.0f;
    private Rigidbody2D bulletRigid;

    void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletRigid.velocity = transform.up * (bulletVelocity + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetVelocity);
        Invoke("TimedDestroy", 2f);
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
