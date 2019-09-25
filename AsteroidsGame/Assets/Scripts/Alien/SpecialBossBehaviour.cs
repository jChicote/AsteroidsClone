using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBossBehaviour : MonoBehaviour
{

    public Transform[] gunPositions;
    public GameObject emp;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BlastFire", 2f, 8f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BlastFire()
    {
        int rGun = Random.Range(0, 2);
        Instantiate(rGun == 0 ? emp : bullet, gunPositions[0].position, Quaternion.identity);
        Instantiate(rGun == 1 ? emp : bullet, gunPositions[1].position, Quaternion.Euler(0,0,270));
        Instantiate(rGun == 2 ? emp : bullet, gunPositions[2].position, Quaternion.Euler(0, 0, 90));
    }
}
