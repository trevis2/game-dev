using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] float bulletTimeToLive= 2.0f;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
            Destroy(projectile, bulletTimeToLive);
        }
    }
}
