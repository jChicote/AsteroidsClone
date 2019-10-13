using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component is 1 of 2 components that controls the player object
// This class controls mainly the inputs related to movement and the current weapons being used

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
        if(PlayerController.justDied == false)
        {
            if (Input.GetKey("space"))
            {
                if (Input.GetKeyDown("space"))
                {
                    CheckMode();
                }
                else if (weaponMode == 3)
                {
                    //keep true
                    laserBeam.isHeld = true;
                }
            }
            else if (Input.GetKeyUp("space"))
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
    }

    public GameObject GetWeaponLoc
    {
        get { return bulletLocation; }
    }

    /*
     * This section checks the mode of weapons and spawns projectile that corresponds
     * to the current weapon mode.
     */
    void CheckMode()
    {
        switch (weaponMode)
        {
            case 0:
                //Spawns normal bullet
                Cursor.visible = true;
                weaponAudio.clip = null;
                weaponAudio.PlayOneShot(classicBullet, 1);
                Instantiate(normalBullet, firingPos.position, firingPos.rotation);
                break;

            case 1:
                //Spawns double fading bullet projectiles
                Cursor.visible = true;
                weaponAudio.clip = null;
                weaponAudio.PlayOneShot(doubleAudio, 1);
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 45f));
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 45f));
                break;

            case 2:
                //Spawns triple powerful bullet projectiles
                Cursor.visible = true;
                weaponAudio.clip = null;
                weaponAudio.PlayOneShot(tripleAudio, 1);
                Instantiate(powerBullet, firingPos.position, firingPos.rotation);
                Instantiate(powerBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 35f));
                Instantiate(powerBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 35f));
                break;

            case 3:
                //Spawns the single laser projectile
                Cursor.visible = true;
                weaponAudio.clip = laserAudio;
                weaponAudio.loop = true;
                weaponAudio.Play();
                Instantiate(laserFire, firingPos.position, firingPos.rotation);
                break;

            case 4:
                //Spawns single explosive scatter round
                weaponAudio.clip = null;
                Cursor.visible = false;

                //Counts the number of pulse rounds on screen
                pulseCount = GameObject.FindGameObjectsWithTag("bullet").Length;

                //Ensures that there is no more than 6 rounds on screen
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
