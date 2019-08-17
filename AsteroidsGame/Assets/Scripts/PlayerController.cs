using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float presentVel;
    bool runDecel = false;
    Vector3 prevRotation;
    Transform firingPos;

    public GameObject bullLoc;
    public GameObject bullet;
    public int playerSpeed = 5;
    public float decelVelocity = 0.2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firingPos = bullLoc.GetComponent<Transform>();
    }

    void Update()
    { 
        //Rotates player based on mouse direction
        Vector3 mouseScreen = Input.mousePosition; //gets position and stores into vector3
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen); //retrieves vector pos of mouse from screen to world space

        //converts coordinaties to angle in radians and is returned into objects rotation.
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);

        CheckFire();

        if (Input.GetKey("w"))
        {
            presentVel = (float)playerSpeed;
            rb.velocity = transform.up * (float)playerSpeed;
            runDecel = true;

        }
        else if (Input.GetKeyUp("w"))
        {
            //preserves velocity and rotation after movement
            prevRotation = transform.up;
        }
        else if (runDecel == true)
        {
            presentVel -= Time.deltaTime * playerSpeed;
            rb.velocity = prevRotation * presentVel;

            Debug.Log(transform.position);

            //Ensure velocity remains at zero
            if (presentVel <= 0.0f)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                runDecel = false;
            }
        }
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
