using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeam : MonoBehaviour
{
    public static bool isHeld = false;
    public Transform firingPos;

    RaycastHit2D ray;

    private float batteryPower = 100f;
    private float hitDistance;
    private GameObject middle;


    // Start is called before the first frame update
    void Start()
    {
        firingPos = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>().GetWeaponLoc.transform;
    }

    // Update is called once per frame
    void Update()
    {
        firingPos = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>().GetWeaponLoc.transform;

        if (isHeld == true)
        {
            ray = Physics2D.Raycast(firingPos.position, new Vector2(firingPos.up.x, firingPos.up.y), 100, 1 << 10 | 1 << 11 | 1 << 13 );
            //hitDistance = Vector2.Distance(ray.collider.transform.position, transform.position);

            /*take note the rendering distance of the middle laser is independant from raycast distance due to
            the pixel per unit controlling underlying sizing.*/
            hitDistance = ray.distance * 3f;

            if (ray.collider != null)
            {
                transform.localScale = new Vector3(1, hitDistance, 1);
                transform.position = new Vector3(firingPos.position.x, firingPos.position.y, 0);
                transform.rotation = firingPos.rotation;

                Debug.Log(hitDistance);
                Debug.Log(hitDistance * 2);
                Debug.Log(ray.collider.gameObject.name);
                Debug.DrawRay(firingPos.transform.position, firingPos.transform.up * ray.distance, Color.yellow, 4.0f);
            } else
            {
                transform.localScale = new Vector3(1, 100, 1);
                transform.position = new Vector3(firingPos.position.x, firingPos.position.y, 0);
                transform.rotation = firingPos.rotation;


                Debug.DrawRay(firingPos.transform.position, firingPos.transform.up * 30, Color.yellow, 4.0f);
            }
            
        }

        if (isHeld == false)
        {
            Destroy(gameObject);
        }
    }
}
