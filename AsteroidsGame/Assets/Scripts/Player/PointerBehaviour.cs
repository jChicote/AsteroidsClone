using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBehaviour : MonoBehaviour
{
    public Vector3 pointedLoc;

    public static GameObject targetObj;
    private bool targetLock = false;
    private Animator anim;
    private float timeCount = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    void Update()
    {
        //Get the raycast information based on the location of the mouse
        RaycastHit2D objInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (WeaponController.weaponMode != 4) Destroy(gameObject);

        //checks and run whether an object is selected
        if (targetLock == false)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            //check whether object is hovered
            if (objInfo.collider != null)
            {
                if (objInfo.collider.tag == "LargeAsteroid" || objInfo.collider.tag == "SmallAsteroid" || objInfo.collider.tag == "Alien")
                {
                    Debug.Log("On Hover");
                    anim.SetBool("isHover", true);

                    //interpolates rotation when hovered
                    transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -90), timeCount);
                    timeCount += Time.deltaTime * 2;

                    //select object that is being hovered
                    if (Input.GetMouseButtonDown(0))
                    {
                        targetObj = objInfo.collider.gameObject;
                        targetLock = true;
                        anim.SetBool("isLock", true);
                    }
                }

            }
            else
            {
                if (transform.rotation != Quaternion.Euler(0, 0, 0)) timeCount = 0;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                anim.SetBool("isHover", false);
            }
        }
        else
        {
            //checks if object has been destroyed
            if (targetObj == null)
            {
                anim.SetBool("isLock", false);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                targetLock = false;
            } else
            {
                transform.position = targetObj.transform.position;
                transform.Rotate(0f, 0f, 2f);
            }

            //checks whether user selects another object whilst object is currently targetting
            if (objInfo.collider != null)
            {
                if (objInfo.collider.tag == "LargeAsteroid" || objInfo.collider.tag == "SmallAsteroid" || objInfo.collider.tag == "Alien")
                {
                    if (Input.GetMouseButtonDown(0)) targetObj = objInfo.collider.gameObject;
                }
            }
        }
    }
}
