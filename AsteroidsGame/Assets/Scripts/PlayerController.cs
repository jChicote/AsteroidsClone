using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float curVelocity;
    float buildVelocity;
    Vector2 playerPos;
    Vector3 prevRotation;
    Transform firingPos;

    public GameObject bullLoc;
    public GameObject bullet;
    public float thrustPower = 10f;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firingPos = bullLoc.GetComponent<Transform>();
        playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        anim = GetComponent<Animator>();
    }

    void Update()
    { 
        CheckFire();

        //player rotation
        if (Input.GetKey("a"))
        {
            gameObject.transform.Rotate(0f, 0f, 5f);

        } else if (Input.GetKey("d"))
        {
            gameObject.transform.Rotate(0f, 0f, -5f);
        }

        //gameObject.transform.position += prevRotation * currentVelocity * Time.deltaTime;
        if (Input.GetKey("w"))
        {
            ApplyForce();
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetKeyUp("w"))
        {
            buildVelocity = 0;
            prevRotation = transform.up;
            anim.SetBool("isMoving", false);
        }
        else
        {
            //simple inertia
            gameObject.transform.position += prevRotation * curVelocity;

        }
    }

    void ApplyForce()
    {
        buildVelocity += thrustPower * Time.deltaTime;
        curVelocity = buildVelocity;
        gameObject.transform.position += transform.up * curVelocity;
        //Debug.Log(curVelocity);
    }

    //Checks whether playerhas fired bullet
    void CheckFire()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(bullet, firingPos.position, firingPos.rotation);
        }
    }
}
