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

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

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

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * thrustRate * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(Vector3.forward);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(Vector3.back);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }

    void StopRotating()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }

    void ApplyRotation(Vector3 vettore)
    {
        rb.freezeRotation = true; // freeze rotation so wecan manually rotate
        transform.Rotate(vettore * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
