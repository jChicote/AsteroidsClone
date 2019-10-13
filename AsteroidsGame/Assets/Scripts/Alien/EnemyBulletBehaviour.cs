using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a simple bullet that is fired from aliena dn travels in a single direction
public class EnemyBulletBehaviour : MonoBehaviour
{
    public float bulletVelocity = 6.0f;
    private Rigidbody2D bulletRigid;

    void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletRigid.velocity = transform.up * bulletVelocity;
    }

    //Destroys object when invisible
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //enters trigger when collision is detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
            
    }
}
