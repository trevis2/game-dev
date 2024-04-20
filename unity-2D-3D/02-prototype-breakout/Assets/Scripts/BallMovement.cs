using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    [SerializeField] GameObject playerPad;
    // Start is called before the first frame update
    [SerializeField] float ballVelocity = 7.0f;
    [SerializeField] float angleRotationBump = 10.0f;
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

        if (Input.GetKeyDown(KeyCode.Space) && isNotInMovement())
        {
            LaunchBall(ballVelocity);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 normale = other.contacts[0].normal * ballVelocity;
        int randomNumber = Random.Range(0, 2) * 2 - 1;
        Vector2 normaleRotated = Quaternion.AngleAxis(angleRotationBump * randomNumber, Vector3.back) * normale;

        GetComponent<Rigidbody2D>().AddForce(normaleRotated, ForceMode2D.Impulse);
        if (other.gameObject.tag == "Block")
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
