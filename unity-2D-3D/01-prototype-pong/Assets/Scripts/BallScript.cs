using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] float ballVelocity = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        transform.position = (transform.position + new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z)) * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        float randomDirection = Random.Range(0, 2) * 2 - 1;
        Debug.Log(other.contacts[0].normal);

        if (other.contacts[0].normal.x != 0.0f)
        {
            float yValue = 1;
            Debug.Log(rb.velocity);
            if (rb.velocity.y != 0)
            {
                yValue = rb.velocity.y / Mathf.Sqrt(rb.velocity.y * rb.velocity.y);
            }
            Debug.Log(yValue);

            rb.velocity = new Vector2(other.contacts[0].normal.x, yValue) * ballVelocity;
            Debug.Log(rb.velocity);
        }
        else if (other.contacts[0].normal.y != 0.0f)
        {
            float xValue = 1;
            if (rb.velocity.x != 0)
            {
                xValue = rb.velocity.x / Mathf.Sqrt(rb.velocity.x * rb.velocity.x);
            }
            rb.velocity = new Vector2(xValue, other.contacts[0].normal.y) * ballVelocity;
        }
    }
}
