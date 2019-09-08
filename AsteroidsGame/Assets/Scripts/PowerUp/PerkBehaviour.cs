using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBehaviour : MonoBehaviour
{
    private Collider2D perkCollider;
    private float speed = 10f;
    private float vertForce;

    void Start()
    {
        perkCollider = GetComponent<Collider2D>();
        if (Random.Range(0, 10) > 5) speed *= -1;
        vertForce = Random.Range(-10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        transform.position += new Vector3(0, vertForce * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player")
        {
            WeaponController.weaponMode = 1;
            perkCollider.enabled = false;
            Destroy(gameObject);
        }
    }
}
