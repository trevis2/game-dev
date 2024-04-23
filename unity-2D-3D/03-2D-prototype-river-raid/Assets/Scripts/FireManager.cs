using System;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 30.0f;

    [SerializeField] float bulletAlSecondo = 7.0f;
    bool timeFirePassed = true;
    bool tris = false;


    void Update()
    {
        if (Input.GetButton("Fire1") && timeFirePassed)
        {

            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;

            if (tris)
            {
                Vector3 traslationPlus = new Vector3(transform.position.x + 1, transform.position.y, 0);
                Vector3 traslationMinus = new Vector3(transform.position.x - 1, transform.position.y, 0);
                GameObject projectile1 = Instantiate(projectilePrefab, traslationPlus, transform.rotation);
                GameObject projectile2 = Instantiate(projectilePrefab, traslationMinus, transform.rotation);
                projectile1.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
                projectile2.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
            }

            AudioSource audioSource = projectile.GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            timeFirePassed = false;
            StartCoroutine(ExecuteActionDelayed(TimeFirePassed, 1 / bulletAlSecondo));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            tris = true;
            StartCoroutine(ExecuteActionDelayed(AmmoPassed, 20.0f));
        }
    }

    private IEnumerator ExecuteActionDelayed(Action timeFirePassed, float v)
    {
        yield return new WaitForSeconds(v);
        timeFirePassed();
    }
    void TimeFirePassed()
    {
        timeFirePassed = true;
    }
    void AmmoPassed()
    {
        tris = false;
    }
}
