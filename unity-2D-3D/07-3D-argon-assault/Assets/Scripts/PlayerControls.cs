using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // [SerializeField] InputAction movement;
    // Start is called before the first frame update
    [SerializeField][Range(-20f, 0f)] float minXPos = -10f;
    [SerializeField][Range(0f, 20f)] float maxXPos = 10f;
    [SerializeField][Range(-20f, 0f)] float minYPos = -10f;
    [SerializeField][Range(0f, 20f)] float maxYPos = 10f;
    [SerializeField][Range(0f, 100f)] float xBoostThrow = 1f;
    [SerializeField][Range(0f, 100f)] float yBoostThrow = 1f;

    [SerializeField][Range(-20f, 20f)] float positionPitchFactor = -1f;
    [SerializeField][Range(-20f, 20f)] float positionYawFactor = -1f;

    [SerializeField][Range(-200f, 100f)] float controlPitchFactor = -10f;
    [SerializeField][Range(-200f, 100f)] float controlRollFactor = -10f;

    float xThrow, yThrow;

    //see Execution Order of Unity docs
    // void OnEnable()
    // {
    //     movement.Enable();
    // }

    // void OnDisable()
    // {
    //     movement.Disable();
    // }

    // Update is called once per frame
    void Update()
    {
        // float horizontalThrow = movement.ReadValue<Vector2>().x;
        // float verticalThrow = movement.ReadValue<Vector2>().y;

        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessFiring()
    {
        if (Input.GetAxis("Fire3") == 1)
        {
            Debug.Log("FIRE!");
        }
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal") * xBoostThrow * Time.deltaTime;
        yThrow = Input.GetAxis("Vertical") * yBoostThrow * Time.deltaTime;

        float newXPos = transform.localPosition.x + xThrow;
        float newYPos = transform.localPosition.y + yThrow;
        float newZPos = transform.localPosition.z;

        float clampedXPos = Mathf.Clamp(newXPos, minXPos, maxXPos);
        float clampedYPos = Mathf.Clamp(newYPos, minYPos, maxYPos);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, newZPos);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float rollDueToControl = xThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = yawDueToPosition;
        float roll = rollDueToControl;
        float clampedRoll = Mathf.Clamp(roll, -20f, 20f);
        float clampedPitch = Mathf.Clamp(pitch, -20f, 20f);

        transform.localRotation = Quaternion.Euler(clampedPitch, yaw, clampedRoll);
    }
}
