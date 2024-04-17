using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScirpt : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private void Update()
    {
        transform.position += new Vector3(0, Input.GetAxis("Vertical") * Time.deltaTime * speed, 0);
    }
}
