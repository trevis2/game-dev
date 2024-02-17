using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    float xRotationAngle = 0.0f;
    [SerializeField]
    float yRotationAngle = 1.0f;
    [SerializeField]
    float zRotationAngle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotationAngle, yRotationAngle, zRotationAngle);
    }
}
