using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBehaviour : MonoBehaviour
{
    public GameObject weaponLock;

    private Animator anim;
    private Collider2D perkCollider;
    private float speed = 10f;
    private float vertForce;
    private int perkType;

    void Start()
    {
        anim = GetComponent<Animator>();
        perkCollider = GetComponent<Collider2D>();
        if (Random.Range(0, 10) > 5) speed *= -1;
        vertForce = Random.Range(-10, 10);

        perkType = Random.Range(0, 3);
        //perkType = 3;
        switch (perkType)
        {
            case 0:
                anim.SetBool("isDouble", true);
                break;
            case 1:
                anim.SetBool("isTriple", true);
                break;
            case 2:
                anim.SetBool("isLaser", true);
                break;
            case 3:
                anim.SetBool("isPulse", true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        //transform.position += new Vector3(0, vertForce * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player")
        {
            if (perkType == 0)
            {
                WeaponController.weaponMode = 1;
            } else if (perkType == 1)
            {
                WeaponController.weaponMode = 2;
            } else if (perkType == 2)
            {
                WeaponController.weaponMode = 3;
            } else if (perkType == 3)
            {
                WeaponController.weaponMode = 4;
                Instantiate(weaponLock, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10), Quaternion.identity);
            }
            perkCollider.enabled = false;
            Destroy(gameObject);
        }
    }
}
