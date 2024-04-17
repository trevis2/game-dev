using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentScript : MonoBehaviour
{
    [SerializeField] GameObject ballObject;
    // Update is called once per frame

    void Update()
    {
        transform.position = new Vector3(transform.position.x, ballObject.transform.position.y, transform.position.z);
    }
}
