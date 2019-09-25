using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletBehaviour : MonoBehaviour
{
    public GameObject player;
    public float maxVel = 7.0f;

    private bool playerDestroyed = false;
    private float totalVel = 3f;
    private float empTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetting();
        if (playerDestroyed == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg * -1;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 2f);
            Debug.Log("Checking");
        }
        totalVel = (!playerDestroyed) ? Mathf.Clamp(totalVel + 0.05f, 0.0f, maxVel) : Mathf.Clamp(totalVel - 0.05f, 0.0f, maxVel);
        transform.position += transform.up * totalVel * Time.deltaTime;
        TimedDestroy();
    }

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

    void TimedDestroy()
    {
        empTimer -= Time.deltaTime;
        if (empTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
