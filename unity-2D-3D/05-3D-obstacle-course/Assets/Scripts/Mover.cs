using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float yAxis = Input.GetAxisRaw("Jump") * 100 * Time.deltaTime;
        float zAxis = -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(xAxis, yAxis, zAxis);
    }
}
