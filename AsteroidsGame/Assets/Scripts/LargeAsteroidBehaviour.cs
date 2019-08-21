using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAsteroidBehaviour : MonoBehaviour
{
    public float asteroidVel = 2.0f;
    public GameObject smallAstPrefab;

    private Rigidbody2D asteroidRB;
    private Animator anim;

    void Start()
    {
        asteroidRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    
    void Update()
    {
        transform.position += transform.up * asteroidVel * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            anim.SetBool("isDestroyed", true);
            asteroidVel = 0f;

            int smallAstCount = Random.Range(1, 3);
            Debug.Log(smallAstCount);
            switch (smallAstCount)
            {
                case 1:
                    Instantiate(smallAstPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    break;
                case 2:
                    Instantiate(smallAstPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    Instantiate(smallAstPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    break;
                case 3:
                    Instantiate(smallAstPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    Instantiate(smallAstPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    Instantiate(smallAstPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    break;
            }
            Destroy(gameObject, 1f);
        }
    }
}
