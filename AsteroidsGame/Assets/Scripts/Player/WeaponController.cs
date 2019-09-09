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
    public GameObject fadingBullet;
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
                Debug.Log("On Single Fire");
                weaponAudio.PlayOneShot(classicBullet, 1);
                Instantiate(normalBullet, firingPos.position, firingPos.rotation);
                break;
            case 1:
                Debug.Log("On Double Fire");
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 45f));
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 45f));
                break;
            case 2:
                Debug.Log("On Triple Fire");
                Instantiate(normalBullet, firingPos.position, firingPos.rotation);
                Instantiate(normalBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 35f));
                Instantiate(normalBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 35f));
                break;
        }
    }
}
