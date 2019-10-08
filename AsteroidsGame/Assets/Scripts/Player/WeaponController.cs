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
    public GameObject powerBullet;
    public GameObject fadingBullet;
    public GameObject laserFire;
    public GameObject pulseScatterFire;

    Transform firingPos;

    private int pulseCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        weaponAudio = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
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
                //Debug.Log("On Single Fire");
                weaponAudio.PlayOneShot(classicBullet, 1);
                Instantiate(normalBullet, firingPos.position, firingPos.rotation);
                break;
            case 1:
                //Debug.Log("On Double Fire");
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 45f));
                Instantiate(fadingBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 45f));
                break;
            case 2:
                //Debug.Log("On Triple Fire");
                Instantiate(powerBullet, firingPos.position, firingPos.rotation);
                Instantiate(powerBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z - 35f));
                Instantiate(powerBullet, firingPos.position, Quaternion.Euler(0, 0, firingPos.eulerAngles.z + 35f));
                break;
            case 3:
                //Debug.Log("On Laser Fire");
                Instantiate(laserFire, firingPos.position, firingPos.rotation);
                break;
            case 4:
                //Debug.Log("Pulse Scatter Fire");
                pulseCount = GameObject.FindGameObjectsWithTag("bullet").Length;
                //Debug.Log(pulseCount);
                if (pulseCount < 6)
                {
                    Instantiate(pulseScatterFire, firingPos.position, firingPos.rotation);
                }
                break;
        }
    }
}
