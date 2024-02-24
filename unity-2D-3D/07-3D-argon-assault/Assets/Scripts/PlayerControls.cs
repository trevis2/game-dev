using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // [SerializeField] InputAction movement;
    // Start is called before the first frame update
    [SerializeField] float xBoostThrow = 1.0f;
    [SerializeField] float yBoostThrow = 1.0f;
    void Start()
    {

    }

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

        float xDirectionThrow = Input.GetAxis("Horizontal");
        float yDirectionThrow = Input.GetAxis("Vertical");

        float xThrow = xDirectionThrow * xBoostThrow * Time.deltaTime;
        float yThrow = yDirectionThrow * yBoostThrow * Time.deltaTime;

        float newXPos = transform.localPosition.x + xThrow;
        float newYPos = transform.localPosition.y + yThrow;
        float newZPos = transform.localPosition.z;

        transform.localPosition = new Vector3(newXPos, newYPos, newZPos);
    }
}
