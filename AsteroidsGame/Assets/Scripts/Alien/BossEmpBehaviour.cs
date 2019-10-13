using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEmpBehaviour : MonoBehaviour
{
    public GameObject player;
    public float maxVel = 11.0f;

    private bool playerDestroyed = false;
    private float totalVel = 7f;
    private float empTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /* This block actively updates the position of the gameobject.
     * It calculates the direction between the player and the bossEmp and
     * slerps it's rotation throughout the player's movement*/
    void Update()
    {
        CheckTargetting();
        if (playerDestroyed == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg * -1;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 2f);
        }
        totalVel = (!playerDestroyed) ? Mathf.Clamp(totalVel + 0.05f, 0.0f, maxVel) : Mathf.Clamp(totalVel - 0.05f, 0.0f, maxVel);
        transform.position += transform.up * totalVel * Time.deltaTime;
        TimedDestroy();
    }

    //This method actively checks whether the player target exists within the scene.
    void CheckTargetting()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            playerDestroyed = true;
        }
        else
        {
            playerDestroyed = false;
        }
    }

    //This method acts as a timer until the object is destroyed.
    void TimedDestroy()
    {
        empTimer -= Time.deltaTime;
        if (empTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Triggers during collision with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "bullet")
        {
            //Activate screen jitter
            LevelSpawner.isJitting = true;
            Destroy(gameObject);
        }
    }

    //Destroy when object becomes invisible
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
