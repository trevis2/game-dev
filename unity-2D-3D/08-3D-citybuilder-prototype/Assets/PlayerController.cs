using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    bool isMoving = false;

    bool canMove = true;

    [SerializeField] float movementSpeed = 1.0f;
    // [SerializeField] float maxSpeed = 2.0f;
    [SerializeField] float friction = 0.5f;

    Animator animator;
    Vector2 movementInput;
    new Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (canMove && movementInput != Vector2.zero)
        {
            MoveAndAnimateCharacther();
        }
        else
        {
            StopMovingCharacter();
        }
    }

    private void StopMovingCharacter()
    {
        //rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, Vector2.zero, friction);
        rigidbody.velocity = Vector2.zero;
        IsMoving = false;
    }

    private void MoveAndAnimateCharacther()
    {
        //rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity + (movementInput * movementSpeed * Time.fixedDeltaTime), maxSpeed);
        rigidbody.position = rigidbody.position + (movementInput * movementSpeed * Time.fixedDeltaTime);
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        IsMoving = true;
    }

    void OnMove(InputValue movValue)
    {
        movementInput = movValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    void LockMovement()
    {
        canMove = false;
    }
    void UnlockMovement()
    {
        canMove = true;
    }
}
