using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    public int score = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Plane" && other.gameObject.tag != "Hit")
        {
            score++;
        }
    }
}
