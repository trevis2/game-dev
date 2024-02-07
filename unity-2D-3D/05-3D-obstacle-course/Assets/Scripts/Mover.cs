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
        transform.Translate(Input.GetAxis("Vertical") * speed * Time.deltaTime, Input.GetAxisRaw("Jump") * 100 * Time.deltaTime, -Input.GetAxis("Horizontal") * speed * Time.deltaTime);
    }
}
