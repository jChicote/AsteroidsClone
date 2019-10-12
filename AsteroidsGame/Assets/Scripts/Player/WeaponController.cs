using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static int weaponMode;

    public AudioSource weaponAudio;
    public AudioClip classicBullet;
    public AudioClip doubleAudio;
    public AudioClip tripleAudio;
    public AudioClip laserAudio;
    public AudioClip pulse1Audio;
    public AudioClip pulse2Audio;
    public AudioClip emptyAudio;

    //child transform object
    public GameObject bulletLocation;
    public GameObject normalBullet;
    public GameObject powerBullet;
    public GameObject fadingBullet;
    public GameObject laserFire;
    public GameObject pulseScatterFire;

    Transform firingPos;

    private int pulseCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        weaponAudio = GameObject.Find("WeaponAudioSource").GetComponent<AudioSource>();
        firingPos = bulletLocation.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if (Input.GetKeyDown("space"))
            {
                CheckMode();
            } else if (weaponMode == 3)
            {
                //keep true
                laserBeam.isHeld = true;
            }
        } else if(Input.GetKeyUp("space"))
        {
            //set to false
            if (weaponMode == 3)
            {
                weaponAudio.loop = false;
                weaponAudio.Stop();
            }
            laserBeam.isHeld = false;
        }
    }

    public GameObject GetWeaponLoc
    {
        get { return bulletLocation; }
    }

    void CheckMode()
    {
        switch (weaponMode)
        {
            case 0:
                Cursor.visible = true;
                weaponAudio.clip = null;
                weaponAudio.PlayOneShot(classicBullet, 1);
                Instantiate(normalBullet, firingPos.position, firingPos.rotation);
                break;
            case 1:
                Cursor.visible = true;
                weaponAudio.clip = null;
                weaponAudio.PlayOneShot(doubleAudio, 1);
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 45f));
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 45f));
                break;
            case 2:
                Cursor.visible = true;
                weaponAudio.clip = null;
                weaponAudio.PlayOneShot(tripleAudio, 1);
                Instantiate(powerBullet, firingPos.position, firingPos.rotation);
                Instantiate(powerBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 35f));
                Instantiate(powerBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 35f));
                break;
            case 3:
                //weaponAudio.PlayOneShot(classicBullet, 1);
                Cursor.visible = true;
                weaponAudio.clip = laserAudio;
                weaponAudio.loop = true;
                weaponAudio.Play();
                Instantiate(laserFire, firingPos.position, firingPos.rotation);
                break;
            case 4:
                weaponAudio.clip = null;
                Cursor.visible = false;
                pulseCount = GameObject.FindGameObjectsWithTag("bullet").Length;
                //Debug.Log(pulseCount);
                if (pulseCount < 6)
                {
                    weaponAudio.PlayOneShot(pulse1Audio, 0.8f);
                    weaponAudio.PlayOneShot(pulse2Audio, 1);
                    Instantiate(pulseScatterFire, firingPos.position, firingPos.rotation);
                } else
                {
                    weaponAudio.PlayOneShot(emptyAudio, 1);
                }
                break;
        }
    }
}
