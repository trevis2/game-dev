using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField]
    float steerSpeed = 225f;

    [SerializeField]
    float moveSpeed = 17f;

    [SerializeField]
    float slowSpeed = 10f;

    [SerializeField]
    float boostSpeed = 30f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    // Time.deltaTime classe di unity che ci dice quanto tempo impiega un singolo frame ad essere eseguito
    //Quindi se Update dipende dai frame al secondo
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedUp")
        {
            moveSpeed = boostSpeed;
        }
        else if (other.tag == "SlowDown")
        {
            moveSpeed = slowSpeed;
        }
    }
}
