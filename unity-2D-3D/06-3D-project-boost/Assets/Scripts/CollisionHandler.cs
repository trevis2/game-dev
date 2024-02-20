using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float levelDelay = 1.0f;
    [SerializeField] AudioClip audioCrash;
    [SerializeField] AudioClip audioSuccess;

    [SerializeField] ParticleSystem particleCrash;
    [SerializeField] ParticleSystem particleSuccess;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                isTransitioning = true;
                StartNextLevelSequence();
                break;
            case "Fuel":
                break;
            default:
                isTransitioning = true;
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        //sfx effect
        audioSource.Stop();
        audioSource.PlayOneShot(audioCrash);

        //particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelDelay);
    }

    void StartNextLevelSequence()
    {
        //sfx effect
        audioSource.Stop();
        audioSource.PlayOneShot(audioSuccess);
        //particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
