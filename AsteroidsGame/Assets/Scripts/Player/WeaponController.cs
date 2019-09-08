using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static int weaponMode;

    public AudioSource weaponAudio;
    public AudioClip classicBullet;

    //child transform object
    public GameObject bulletLocation;

    public GameObject normalBullet;
    public GameObject laserFire;
    public GameObject trackingBullet;

    Transform firingPos;

    // Start is called before the first frame update
    void Start()
    {
        weaponAudio = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        firingPos = bulletLocation.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey("space"))
        {
            weaponAudio.PlayOneShot(classicBullet, 1);
            Instantiate(normalBullet, firingPos.position, firingPos.rotation);
        }*/

        if (Input.GetKeyDown("space"))
        {
            CheckMode();
        }
    }

    void CheckMode()
    {
        switch (weaponMode)
        {
            case 0:
                weaponAudio.PlayOneShot(classicBullet, 1);
                Instantiate(normalBullet, firingPos.position, firingPos.rotation);
                break;
            case 1:
                Debug.Log("Powerup 1 Active >> DOUBLE FIRE");
                Instantiate(normalBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 35f));
                Instantiate(normalBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 35f));
                break;
            case 2:
                Instantiate(normalBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 35f));
                Instantiate(normalBullet, firingPos.position, firingPos.rotation);
                Instantiate(normalBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 35f));
                break;
        }
    }
}
