using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component is 1 of 2 components that controls the player object
// This class controls mainly the movement, rotation and destruction of the object

public class PlayerController : MonoBehaviour
{
    public static bool justDied = false;

    float curVelocity;
    float buildVelocity;
    Vector2 playerPos;
    Vector3 prevRotation;
    Transform firingPos;
    bool isMoving = false;

    public GameObject bullLoc;
    public float thrustPower = 10f;
    public AudioSource playerAudio;
    public AudioSource weaponAudio;
    public AudioClip playerThust;
    public AudioClip playerExplosion;
    public AudioClip gameOver;
    public AudioClip youDied;
    public AudioClip baseExplosion;

    //Velocity getter method
    public float GetVelocity
    {
        get { return curVelocity; }
    }

    private Animator anim;
    private IEnumerator coroutine;

    void Start()
    {
        justDied = false;
        playerAudio = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        weaponAudio = GameObject.Find("WeaponAudioSource").GetComponent<AudioSource>();

        firingPos = bullLoc.GetComponent<Transform>();
        playerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        anim = GetComponent<Animator>();
        coroutine = PlayerLoose();
    }

    void Update()
    {
        if (UIManager.isPaused == false && justDied == false)
        {
            //player rotation
            if (Input.GetKey("a"))
            {
                gameObject.transform.Rotate(0f, 0f, 5f);

            }
            else if (Input.GetKey("d"))
            {
                gameObject.transform.Rotate(0f, 0f, -5f);
            }

            if (Input.GetKey("w"))
            {
                if (!isMoving)
                {
                    playerAudio.clip = playerThust;
                    playerAudio.loop = true;
                    playerAudio.Play();
                    isMoving = true;
                }
                ApplyForce();
                anim.SetBool("isMoving", true);
            }
            else if (Input.GetKeyUp("w"))
            {
                playerAudio.loop = false;
                isMoving = false;

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

    //enters trigger when collision is detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "allowPass")
        {
            weaponAudio.loop = false;
            weaponAudio.Stop();

            gameObject.GetComponent<Collider2D>().enabled = false;
            if (PlayerLifes.numofLifes > 1)
            {
                
                playerAudio.loop = false;
                playerAudio.PlayOneShot(playerExplosion, 1f);
                WeaponController.weaponMode = 0;

                PlayerLifes.numofLifes -= 1;
                Debug.Log("num of lifes" + PlayerLifes.numofLifes);
                justDied = true;
                anim.SetBool("isDestroyed", true);
                Destroy(gameObject, 1f);
            } else
            {
                PlayerLifes.numofLifes -= 1;
                WeaponController.weaponMode = 0;
                justDied = true;
                StartCoroutine(coroutine);
            }
        }
    }

    //Method is used for object acceleration
    void ApplyForce()
    {
        buildVelocity += thrustPower * Time.deltaTime;
        curVelocity = buildVelocity;
        gameObject.transform.position += transform.up * curVelocity;
    }

    //Animates character death whilst running parallel to gameplay
    private IEnumerator PlayerLoose()
    {
        LevelSpawner.playerIsLost = true;
        transform.rotation = Quaternion.Euler(prevRotation);
        anim.SetBool("isMoving", false);

        playerAudio.loop = false;
        playerAudio.PlayOneShot(gameOver, 1f);

        yield return new WaitForSeconds(0.3f);
        Camera.main.orthographicSize = 8;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        yield return new WaitForSeconds(0.3f);
        Camera.main.orthographicSize = 5;

        yield return new WaitForSeconds(0.3f);
        Camera.main.orthographicSize = 2;

        yield return new WaitForSeconds(1f);

        playerAudio.PlayOneShot(playerExplosion, 1f);
        playerAudio.PlayOneShot(baseExplosion, 1f);

        anim.SetBool("isDestroyed", true);
        Destroy(gameObject, 1f);

        yield return new WaitForSeconds(2f);
        playerAudio.PlayOneShot(youDied, 1f);
    }

}
