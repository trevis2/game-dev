using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float velocity = 2.0f;
    [SerializeField] GameObject ball;
    [SerializeField] float forceImpulse = 10.0f;
    private void Awake()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), 0) * velocity * Time.fixedDeltaTime;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7f, 7f), -4.5f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ball.GetComponent<BallMovement>().isNotInMovement())
        {
            ball.GetComponent<BallMovement>().LaunchBall(forceImpulse);
        }
    }
}
