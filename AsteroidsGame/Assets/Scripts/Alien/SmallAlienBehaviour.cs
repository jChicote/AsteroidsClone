using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAlienBehaviour : MonoBehaviour
{
    public GameObject enemybullPrefab;
    public GameObject player;
    public AudioSource alienAudio;
    public AudioClip alienExplosion;
    public AudioClip alienFiring;

    private Collider2D alienCollide;
    private Vector3 playerTarget;
    private Vector3 newDir;
    private Vector3 bullRot;
    private Quaternion bulletDir;
    private Animator anim;
    private float verticalPosition;
    private float moveTimer;
    public float timer = 3f;

    float speed = 4f;
    Transform playerTrans;
    bool movePosSet = false;

    void Start()
    {
        alienCollide = GetComponent<Collider2D>();
        alienAudio = GameObject.Find("EnemyAudioSource").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        if (Random.Range(0, 10) > 5)  speed *= -1;
        InvokeRepeating("RandomFire", 1, 1);
        verticalPosition = transform.position.y;
    }

    /*
     * During the update method the alien is launched with a specified y position and direction.
     * The alien will release a timer and which continually trigger the alien's vertical position
     * towards the player's.
     *
     * NOTE: This version of the timer is coded without the use of an invoke method
     * */
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        transform.position += transform.right * speed * Time.deltaTime;

        if (player != null)
        {
            playerTrans = player.GetComponent<Transform>();
        }

        if ((int)timer <= 0)
        {
            if (movePosSet != true && player != null)
            {
                verticalPosition = playerTrans.position.y;
                moveTimer = Random.Range(3f, 8f);
                movePosSet = true;
            }
        } else
        {
            timer -= Time.deltaTime;
        }

        if (movePosSet == true)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            if ((int)transform.position.y == (int)verticalPosition || moveTimer <= 0)
            {
                timer = Random.Range(4f, 8f);
                movePosSet = false;
            } else
            {
                moveTimer -= Time.deltaTime;
            }
        }
    }

    //enters trigger when collision is detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player")
        {
            alienCollide.enabled = false;
            alienAudio.PlayOneShot(alienExplosion, 1f);

            CancelInvoke();
            anim.SetBool("isDestroyed", true);
            speed = 0.2f;
            PointsController.points += 1000;
            Destroy(gameObject, 1f);
        }
    }

    /*
     * This method randomly fires enemy bullet in the direction of player
     * Compared to large aliens the firing is a lot more accurate.
     */
    private void RandomFire()
    {

        if (player != null) {
            //Checks player position relative to alien
            playerTarget = playerTrans.transform.position - transform.position;

            //Calculates the aim between the alien and player (coded to be inaccurate)
            //Calculates angle using tan between two vectors > convert to degrees > invert forward orientation
            float angle = (Mathf.Atan2(playerTarget.x, playerTarget.y)) * Mathf.Rad2Deg * -1;
            //Debug.Log(Quaternion.Euler(0,0,angle));

            //During each call only fire if val is greater than 6
            int randomVal = Random.Range(0, 10);
            if (randomVal >= 6)
            {
                alienAudio.PlayOneShot(alienFiring, 1f);
                Instantiate(enemybullPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            }
        }
    }
}
