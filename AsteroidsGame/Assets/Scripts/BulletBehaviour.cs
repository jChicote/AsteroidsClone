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
    }

    void Update()
    {
        
    }

    //Destroys object when invisible
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
