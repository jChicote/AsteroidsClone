using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAlienBehaviour : MonoBehaviour
{
    public GameObject enemybullPrefab;

    private Vector3 playerTarget;
    private Vector3 newDir;
    private Vector3 bullRot;
    private Quaternion bulletDir;
    private Animator anim;

    float speed = 4f;
    Transform player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerTarget = player.transform.position - transform.position;

        //Note the maxradiansDelta is the maximum radians the object can orient from its starting directions
        newDir = Vector3.RotateTowards(transform.right, playerTarget, 4, 0.0f);
        InvokeRepeating("RandomFire", 1, 1);
    }

    void Update()
    {
        transform.position += newDir * speed * Time.deltaTime;
    }

    //enters trigger when collision is detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            PointsController.points += 400;
            CancelInvoke();
            anim.SetBool("IsDestroyed", true);
            speed = 0.2f;
            Destroy(gameObject, 1f);
        }
    }

    private void RandomFire()
    {
        //Checks player position relative to alien
        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
            playerTarget = player.transform.position - transform.position;


            Debug.DrawRay(transform.position, playerTarget, Color.red, 5);

            //Calculates the aim between the alien and player (coded to be inaccurate)
            //Calculates angle based off tanget between two vectors > include random angle offset > convert to degrees > invert forward orientation
            float angle = (Mathf.Atan2(playerTarget.x, playerTarget.y) + Random.Range(-1, 1)) * Mathf.Rad2Deg * -1;
            //Debug.Log(Quaternion.Euler(0,0,angle));

            //During each call only fire if val is greater than 6
            int randomVal = Random.Range(0, 10);
            if (randomVal >= 6) Instantiate(enemybullPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        }
    }
}
