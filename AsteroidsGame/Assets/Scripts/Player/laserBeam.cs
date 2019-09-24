using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeam : MonoBehaviour
{
    public static bool isHeld = false;
    public Transform firingPos;
    public GameObject startPrefab;
    public GameObject ricochetPrefab;

    RaycastHit2D ray;
    GameObject laserStart;

    private Collider2D laserCollider;
    //private float batteryPower = 100f;
    private float hitDistance;
    private bool isReleased = false;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        firingPos = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>().GetWeaponLoc.transform;
        anim = GetComponent<Animator>();
        laserStart = Instantiate(startPrefab, firingPos.position, Quaternion.identity);
        laserCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        firingPos = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>().GetWeaponLoc.transform;
        if (!isReleased)
        {
            if (isHeld == true)
            {
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

                    for (int i = 0; i < Random.Range(0, 3); i++)
                    {
                        Instantiate(ricochetPrefab, ray.point, Quaternion.Euler(0, 0, RangeRandDir()));
                    }

                    //Debug.DrawRay(firingPos.transform.position, firingPos.transform.up * ray.distance, Color.yellow, 4.0f);
                }
                else
                {
                    transform.localScale = new Vector3(1, 150, 1);
                    transform.position = new Vector3(firingPos.position.x, firingPos.position.y, 0);
                    transform.rotation = firingPos.rotation;

                    //Debug.DrawRay(firingPos.transform.position, firingPos.transform.up * 30, Color.yellow, 4.0f);
                }

            }
        }

        if (isHeld == false) {
            isReleased = true;
        }

        if (isReleased == true)
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
