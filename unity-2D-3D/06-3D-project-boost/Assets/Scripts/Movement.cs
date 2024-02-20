using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float thrustRate = 750.0f;
    [SerializeField] float rotationSpeed = 75.0f;
    [SerializeField] AudioClip audioEngine;

    bool isAlive;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
    }

    void ApplyRotation(Vector3 vettore)
    {
        rb.freezeRotation = true; // freeze rotation so wecan manually rotate
        transform.Rotate(vettore * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustRate * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
