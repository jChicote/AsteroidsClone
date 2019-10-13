using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NOTE: Laser is behaviour defined for single laser
 *
 * This is activated during collision with YELLOW coin
 */

public class laserBeam : MonoBehaviour
{
    public static bool isHeld = false;
    public Transform firingPos;
    public GameObject startPrefab;
    public GameObject ricochetPrefab;
    public AudioSource weaponAudio;
    public AudioClip burntOut;

    RaycastHit2D ray;
    GameObject laserStart;

    private float hitDistance;
    private bool isReleased = false;
    private bool isBurnt = false;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        weaponAudio = GameObject.Find("WeaponAudioSource").GetComponent<AudioSource>();
        firingPos = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>().GetWeaponLoc.transform;
        anim = GetComponent<Animator>();
        laserStart = Instantiate(startPrefab, firingPos.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //This searches for the firing position of the laser 
        firingPos = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>().GetWeaponLoc.transform;

        if (!isReleased)
        {
            if (isHeld == true && PlayerController.justDied == false && LaserUI.batteryCharge > 0)
            {
                LaserUI.batteryCharge -= 0.5f;
                laserStart.transform.position = firingPos.position;
                anim.SetBool("isLaserHeld", true);
                ray = Physics2D.Raycast(firingPos.position, new Vector2(firingPos.up.x, firingPos.up.y), 100, 1 << 10 | 1 << 11 | 1 << 13);

                /*take note the rendering distance of the middle laser is independant from raycast distance due to
                the pixel per unit controlling underlying sizing.*/
                hitDistance = ray.distance * 3f;
                if (ray.collider != null)
                {
                    transform.localScale = new Vector3(1, hitDistance, 1);
                    transform.position = new Vector3(firingPos.position.x, firingPos.position.y, 0);
                    transform.rotation = firingPos.rotation;

                    // This spawns ricochet flakes during collision with object
                    // Note this effect may not be noticeable
                    for (int i = 0; i < Random.Range(0, 3); i++)
                    {
                        Instantiate(ricochetPrefab, ray.point, Quaternion.Euler(0, 0, RangeRandDir()));
                    }
                }
                else
                {
                    transform.localScale = new Vector3(1, 150, 1);
                    transform.position = new Vector3(firingPos.position.x, firingPos.position.y, 0);
                    transform.rotation = firingPos.rotation;
                }

            }
            else if(isHeld == true && isBurnt == false)
            {
                //Below stops the audio source from continuing loop during burn out of laser.
                if (LaserUI.batteryCharge <= 0)
                {
                    weaponAudio.loop = false;
                    weaponAudio.Stop();
                    weaponAudio.PlayOneShot(burntOut, 1f);
                    isBurnt = true;
                }
            }
        }

        //This modifies the isReleased boolean variable
        if (isHeld == false) {
            isReleased = true;
        }

        //During encountering of below conditions, the laser animates and is destroyed
        if (isReleased == true || LaserUI.batteryCharge <= 0 || PlayerController.justDied == true)
        {
            Destroy(laserStart);
            anim.SetBool("isLaserHeld", false);
            Destroy(gameObject, 0.5f);
        }
    }

    //produces range of directions for ricochet to be travelling
    private float RangeRandDir()
    {
        Vector3 diff = firingPos.position - new Vector3(ray.point.x, ray.point.y, 0);
        float angle = (Mathf.Atan2(diff.x, diff.y) + Random.Range(-1.2f, 1.2f)) * Mathf.Rad2Deg * -1;

        return angle;
    }
}
