using System;
using System.Collections;
using UnityEngine;

public class GenerateObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstablePrefab;
    [SerializeField] float generationTime = 5.0f;
    [SerializeField] float obstacleSpeed = 2.0f;
    [SerializeField] float obstacleCoppia = 5.0f;

    [SerializeField] Vector3 starterPosition; // Posizione intorno alla quale oscillare
    [SerializeField] float xAmplitude = 1.0f;
    [SerializeField] float yAmplitude = 0.0f;   // Ampiezza dell'oscillazione
    [SerializeField] float speed = 1.0f;       // Velocità dell'oscillazione

    private float timeOffset;      // Offset temporale

    void Start()
    {
        timeOffset = UnityEngine.Random.Range(0f, 2f * Mathf.PI); // Inizializza l'offset temporale per variare l'oscillazione
        StartCoroutine(ExecuteActionDelayed(ObstacleGen));
    }

    private void Update()
    {
        float x = starterPosition.x + xAmplitude * Mathf.Sin(speed * (Time.time + timeOffset));
        float y = starterPosition.y + yAmplitude * Mathf.Cos(speed * (Time.time + timeOffset));
        float z = starterPosition.z;

        // Imposta la posizione del GameObject
        transform.position = new Vector3(x, y, z);
    }

    private IEnumerator ExecuteActionDelayed(Action function)
    {
        while (true)
        {
            yield return new WaitForSeconds(generationTime);
            function();
        }
    }

    void ObstacleGen()
    {
        GameObject projectile = Instantiate(obstablePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * (-1) * obstacleSpeed;
        projectile.GetComponent<Rigidbody2D>().AddTorque(obstacleCoppia);
    }
}
