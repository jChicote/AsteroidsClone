using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    public float bulletVelocity = 6.0f;

    private Rigidbody2D bulletRigid;

    void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletRigid.velocity = transform.up * bulletVelocity;
    }

    void Update()
    {

    }

    //Destroys object when invisible
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
            
    }
}
