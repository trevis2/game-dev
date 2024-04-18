using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BallScript : MonoBehaviour
{
    [SerializeField] float ballVelocity = 3f;
    Vector3 velocityDirection;
    [SerializeField] TMP_Text playerScore;
    [SerializeField] TMP_Text opponentScore;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        velocityDirection = new Vector3(-1.0f, -1.0f, 0.0f);

        playerScore.text = string.IsNullOrEmpty(PlayerPrefs.GetString("playerScore")) ? "0" : PlayerPrefs.GetString("playerScore");
        opponentScore.text = string.IsNullOrEmpty(PlayerPrefs.GetString("opponentScore")) ? "0" : PlayerPrefs.GetString("opponentScore");
    }

    private void Update()
    {
        transform.position += velocityDirection * ballVelocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal.x > 0.0f || other.contacts[0].normal.x < 0.0f)
        {
            float yValue = 1;
            Debug.Log(velocityDirection);
            if (velocityDirection.y != 0)
            {
                yValue = velocityDirection.y / Mathf.Sqrt(velocityDirection.y * velocityDirection.y);
            }
            velocityDirection = new Vector3(other.contacts[0].normal.x, yValue, 0.0f);

        }
        else if (other.contacts[0].normal.y > 0.0f || other.contacts[0].normal.y < 0.0f)
        {
            float xValue = 1;
            Debug.Log(velocityDirection);
            if (velocityDirection.x != 0)
            {
                xValue = velocityDirection.x / Mathf.Sqrt(velocityDirection.x * velocityDirection.x);
            }
            velocityDirection = new Vector3(xValue, other.contacts[0].normal.y, 0.0f);
        }
        if (ballVelocity < 30f)
        {
            ballVelocity += 1f;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool pointScored = false;
        if (other.gameObject.tag == "PlayerScoreTrigger")
        {
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
            pointScored = true;

        }
        if (other.gameObject.tag == "OpponentScoreTrigger")
        {
            opponentScore.text = (int.Parse(opponentScore.text) + 1).ToString();
            pointScored = true;
        }
        if (pointScored)
        {
            PlayerPrefs.SetString("playerScore", playerScore.text);
            PlayerPrefs.SetString("opponentScore", opponentScore.text);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
