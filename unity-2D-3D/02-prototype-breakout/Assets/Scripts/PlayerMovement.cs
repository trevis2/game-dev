using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float padVelocity = 350.0f;

    private void Awake()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), 0) * padVelocity * Time.fixedDeltaTime;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10f, 10f), -4.5f);
    }

}
