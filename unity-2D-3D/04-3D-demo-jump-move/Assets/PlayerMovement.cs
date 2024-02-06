using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Animator playerAnimator;
    public Transform playerTransform;
    private bool isJumping = false;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isGrounded = true;
    private bool isFalling = false;
    private bool isIdle = true;
    [SerializeField] float walkVelocity = 50.0f;
    [SerializeField] float rotationVelocity = 30.0f;
    [SerializeField] float jumpVelocity = 350.0f;

    Vector3 movementForward;
    Vector3 rotation;

    Vector3 movementUp;

    void Start()
    {
        playerAnimator.SetBool("isIdle", true);
        playerAnimator.SetBool("isGrounded", true);
        playerRigidbody.velocity = new Vector3();
    }
    void FixedUpdate()
    {
        movementForward = transform.forward * Input.GetAxisRaw("Vertical") * walkVelocity * Time.fixedDeltaTime;
        rotation = transform.up * Input.GetAxis("Horizontal") * rotationVelocity * Time.fixedDeltaTime;
        movementUp = transform.up * jumpVelocity * Time.fixedDeltaTime;
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            playerRigidbody.velocity = new Vector3(movementForward.x, playerRigidbody.velocity.y, movementForward.z);
            playerAnimator.SetBool("isMovingForward", true);
            playerAnimator.SetBool("isIdle", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerRigidbody.velocity = movementForward;
            playerAnimator.SetBool("isMovingBackward", true);
            playerAnimator.SetBool("isIdle", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(rotation);
        }
        if (Input.GetKey(KeyCode.LeftShift) && playerAnimator.GetBool("isMovingForward"))
        {
            playerAnimator.SetBool("isRunning", true);
            playerRigidbody.velocity = new Vector3(movementForward.x, playerRigidbody.velocity.y / 2, movementForward.z) * 2;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerRigidbody.velocity = new Vector3();
            playerAnimator.SetBool("isMovingBackward", false);
            playerAnimator.SetBool("isIdle", true);
            isWalking = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerRigidbody.velocity = new Vector3();
            playerAnimator.SetBool("isMovingForward", false);
            playerAnimator.SetBool("isIdle", true);
            isWalking = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && playerAnimator.GetBool("isRunning"))
        {
            playerAnimator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetBool("isJumping"))
        {
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isGrounded", false);
            playerAnimator.SetBool("isIdle", false);
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x * 4, movementUp.y, playerRigidbody.velocity.z * 4);
        }

        if (playerAnimator.GetBool("isJumping") && playerRigidbody.velocity.y < 2)
        {
            playerAnimator.SetBool("isFalling", true);
            playerAnimator.SetBool("isGrounded", false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision!");
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isGrounded", true);
        playerAnimator.SetBool("isFalling", false);
        playerAnimator.SetBool("isIdle", true);
    }
}
