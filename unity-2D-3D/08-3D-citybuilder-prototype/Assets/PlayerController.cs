using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float collisionOffset = 0.05f;
    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] ContactFilter2D movementFilter;
    [SerializeField] List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Animator animator;
    Vector2 movementInput;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            if (!success)
            {
                TryMove(new Vector2(0, movementInput.y));
            }
            Debug.Log("success:" + success);
            Debug.Log("move to:" + rb.position + movementInput * movementSpeed * Time.fixedDeltaTime);

            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (movementInput.x < 0)
        {
            sr.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            sr.flipX = false;
        }
        //set direction of sprite
    }

    private bool TryMove(Vector2 movementInput)
    {

        rb.MovePosition(rb.position + movementInput * movementSpeed * Time.fixedDeltaTime);
        return true;
    }

    void OnMove(InputValue movValue)
    {
        movementInput = movValue.Get<Vector2>();
    }
}
