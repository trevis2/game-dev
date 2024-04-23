using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = -0.01f; // Velocità di scorrimento del background
    [SerializeField] float resetPositionY = -40.0f;
    [SerializeField] float startPositionY = 5.0f;

    void Update()
    {
        // Calcola lo spostamento delle stelle in base alla velocità e al tempo trascorso
        float offset = scrollSpeed;

        // Applica lo spostamento alle stelle lungo l'asse Y
        transform.position = new Vector3(0, transform.position.y + offset, 0);


        if (transform.localPosition.y <= resetPositionY)
        {
            transform.localPosition = new Vector3(0, startPositionY, 0);
        }
    }
}
