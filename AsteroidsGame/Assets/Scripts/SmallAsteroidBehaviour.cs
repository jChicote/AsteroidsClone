using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroidBehaviour : MonoBehaviour
{
    float velocity = 4.0f;
    private Animator anim;

    void Start()
    {
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
            PointsController.points += 20;
            anim.SetBool("isDestroyed", true);
            velocity = 0.2f;
            Destroy(gameObject, 1f);
        }
    }
}
