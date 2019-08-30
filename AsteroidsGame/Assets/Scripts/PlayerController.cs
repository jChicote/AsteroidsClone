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
    bool playerLost = false;

    public GameObject bullLoc;
    public GameObject bullet;
    public float thrustPower = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private IEnumerator coroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firingPos = bullLoc.GetComponent<Transform>();
        playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        anim = GetComponent<Animator>();
        coroutine = PlayerLoose();
    }

    void Update()
    {
        if (GameStateManager.isPaused == false && playerLost == false)
        {
            CheckFire();

            //player rotation
            if (Input.GetKey("a"))
            {
                gameObject.transform.Rotate(0f, 0f, 5f);

            }
            else if (Input.GetKey("d"))
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "bullet")
        {
            if (PlayerLifes.numofLifes != 1)
            {
                PlayerLifes.numofLifes -= 1;
                Destroy(gameObject);
            } else
            {
                PlayerLifes.numofLifes -= 1;
                playerLost = true;
                StartCoroutine(coroutine);
            }
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

    private IEnumerator PlayerLoose()
    {
        transform.rotation = Quaternion.Euler(prevRotation);
        anim.SetBool("isMoving", false);

        yield return new WaitForSeconds(0.3f);
        Camera.main.orthographicSize = 8;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        yield return new WaitForSeconds(0.3f);
        Camera.main.orthographicSize = 5;

        yield return new WaitForSeconds(0.3f);
        Camera.main.orthographicSize = 2;

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject, 1f);
    }

}
