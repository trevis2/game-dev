using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] GameObject playerPad;
    // Start is called before the first frame update
    bool isAchoredOnPad = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAchoredOnPad)
        {
            transform.position = new Vector2(playerPad.transform.position.x, transform.position.y);
        }

    }

    public void LaunchBall(float forceImpulse)
    {
        isAchoredOnPad = false;
        Vector2 impulse = Vector2.up * forceImpulse; // Impulso lungo l'asse Y
        GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
    }
    public bool isNotInMovement()
    {
        return isAchoredOnPad;
    }
}
