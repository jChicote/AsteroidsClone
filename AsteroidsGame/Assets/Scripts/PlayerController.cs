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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firingPos = bullLoc.GetComponent<Transform>();
        playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    void Update()
    { 
        //Rotates player based on mouse direction
        //Vector3 mouseScreen = Input.mousePosition; //gets position and stores into vector3
        //Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen); //retrieves vector pos of mouse from screen to world space

        //converts coordinaties to angle in radians and is returned into objects rotation.
        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);

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
        }
        else if (Input.GetKeyUp("w"))
        {
            buildVelocity = 0;
            prevRotation = transform.up;
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
