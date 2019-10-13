using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBehaviour : MonoBehaviour
{
    public GameObject weaponLock;
    public AudioSource perkAudio;
    public AudioClip perkClip;

    private Animator anim;
    private Collider2D perkCollider;
    private float speed = 10f;
    private float vertForce;
    private int perkType;

    void Start()
    {
        perkAudio = GameObject.FindGameObjectWithTag("PlayerAudio").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        perkCollider = GetComponent<Collider2D>();
        if (Random.Range(0, 10) > 5) speed *= -1;
        vertForce = Random.Range(-10, 10);

        //This generates the perk type that the object is assigned to.
        perkType = Random.Range(0, 4);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        perkAudio.PlayOneShot(perkClip, 0.5f);
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player")
        {

            //Below modifies the current weapon mode from the Player's weapon controller
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
