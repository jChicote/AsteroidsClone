using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBulletBehaviour : MonoBehaviour
{
    public GameObject target;
    public float acceleration = 1f;
    public float rotateSpeed = 200f;

    private Collider2D pulseCollider;
    private Animator anim;
    private bool isTargetting = false;
    private float totalVel = 3;
    private float smooth = 2f;
    private float timer = 0.4f;
    private bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pulseCollider = GetComponent<Collider2D>();
        CheckTargetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed)
        {
            CheckTargetting();
            timer -= Time.deltaTime;
            if (isTargetting == true)
            {
                Vector3 direction = target.transform.position - transform.position;
                float angle = (Mathf.Atan2(direction.x, direction.y)) * Mathf.Rad2Deg * -1;

                if (timer <= 0)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * smooth);
                }
                if (totalVel <= 7.0f) totalVel += 0.05f;
                
            } else if (isTargetting == false)
            {
                if (totalVel >= 1.0f) totalVel -= 0.05f;
            }
            transform.position += transform.up * totalVel * Time.deltaTime;
        }
    }

    void CheckTargetting()
    {
        if (PointerBehaviour.targetObj != null)
        {
            target = PointerBehaviour.targetObj;
            isTargetting = true;
        }
        else if (PointerBehaviour.targetObj == null)
        {
            isTargetting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LargeAsteroid" || collision.gameObject.tag == "SmallAsteroid" || collision.gameObject.tag == "Alien")
        {
            isDestroyed = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("isExploding", true);
            transform.localScale = new Vector3(12, 12, 0);
            Destroy(gameObject, 0.5f);
        }
    }
}
