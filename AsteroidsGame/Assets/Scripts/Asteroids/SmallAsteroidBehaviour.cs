using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroidBehaviour : MonoBehaviour
{
    public AudioSource asteroidAudio;
    public AudioClip astExplodeSmall;

    float velocity = 4.0f;

    private Animator anim;
    private Collider2D asteroidCollide;
    private Rigidbody2D asteroidRB;

    void Start()
    {
        asteroidAudio = GameObject.Find("EnemyAudioSource").GetComponent<AudioSource>();

        asteroidCollide = GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += transform.up * velocity * Time.deltaTime;
    }

    //enters trigger when collision is detected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            asteroidCollide.enabled = false;

            PointsController.points += 60;
            velocity = 0.2f;

            anim.SetBool("isDestroyed", true);
            asteroidAudio.PlayOneShot(astExplodeSmall, 1);

            Destroy(gameObject, 1f);
        }
    }
}
