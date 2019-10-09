using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBossBehaviour : MonoBehaviour
{

    public Transform[] gunPositions;
    public GameObject emp;
    public GameObject bullet;
    public GameObject healthBar;
    public float speed = 4.0f;
    public Material mat1;
    public Material mat2;
    public SpriteRenderer rend;
    public Animator anim;
    public AudioSource bossAudio;
    public AudioClip bossClip;

    private Vector3 newPosition;
    private Vector3 oldPosition;

    private float duration = 0.2f;
    private float totalHealth = 100;
    private float initialBarLength;

    // Start is called before the first frame update
    void Start()
    {
        bossAudio = GameObject.Find("WeaponAudioSource").GetComponent<AudioSource>();
        InvokeRepeating("BlastFire", 2f, 8f);
        InvokeRepeating("NewMovePosition", 1f, 3f);
        rend = GetComponent<SpriteRenderer>();
        initialBarLength = healthBar.transform.localScale.x;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = newPosition - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
        if (totalHealth < 25)
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            rend.material.Lerp(mat1, mat2, lerp);
        }
    }

    private void NewMovePosition()
    {
        newPosition = new Vector3(Random.Range(LevelSpawner.leftLimit, LevelSpawner.rightLimit), Random.Range(LevelSpawner.topLimit, LevelSpawner.bottomLimit),0);
    }

    private void BlastFire()
    {
        int rGun = Random.Range(0, 2);
        Instantiate(rGun == 0 ? emp : bullet, gunPositions[0].position, Quaternion.identity);
        Instantiate(rGun == 1 ? emp : bullet, gunPositions[1].position, Quaternion.Euler(0,0,270));
        Instantiate(rGun == 2 ? emp : bullet, gunPositions[2].position, Quaternion.Euler(0, 0, 90));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            switch (collision.gameObject.name)
            {
                case "Bullet(Clone)":
                    totalHealth -= 3;
                    break;
                case "DoubleFadeBullet(Clone)":
                    totalHealth -= 5;
                    break;
                case "powerBullet(Clone)":
                    totalHealth -= 8;
                    break;
                case "laserFieBeam(Clone)":
                    totalHealth -= 1;
                    break;
                case "PulseScatterBullet(Clone)":
                    totalHealth -= 20;
                    break;
            }
            healthBar.transform.localScale = new Vector3(initialBarLength * (totalHealth / 100), healthBar.transform.localScale.y);
            if (totalHealth <= 0)
            {
                anim.SetBool("isDestroyed", true);
                GetComponent<Collider2D>().enabled = false;
                bossAudio.PlayOneShot(bossClip, 1);
                Destroy(gameObject, 1f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            if(collision.gameObject.name == "laserFireBeam(Clone)")
            {
                totalHealth -= 20 * Time.deltaTime;
            }
        }
    }
}
