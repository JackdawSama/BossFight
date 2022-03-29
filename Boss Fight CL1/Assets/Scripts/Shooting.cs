using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firepoint;
    public GameObject bulletPrefab;

    public float bulletForce = 10f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();                            //calls the funcion Shoot() to fire a bullet on mouse down
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,firepoint.position,firepoint.rotation);            // creates an instance of the bullet using a prefab
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();                                                             //obtains the rigidbody of the bullet to add a force to it
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);                                    //adds an impluse force to the bullet
    }

}
