using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float velocity = 15.0f;
    // Start is called before the first frame update
    [SerializeField] float xMax = 7.5f;
    [SerializeField] float yMax = 8.5f;
    [SerializeField] GameObject fireEnginePrefab;
    private AudioSource audioSource;

    private void Awake()
    {
        Physics2D.gravity = new Vector3(0, 0, 0);
    }
    void Start()
    {
        transform.position = new Vector3(0, -yMax, 0);
        fireEnginePrefab.GetComponent<SpriteRenderer>().enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Mathf.Clamp(transform.position.x + (Input.GetAxis("Horizontal") * velocity * Time.deltaTime), -xMax, xMax);
        float yMove = Mathf.Clamp(transform.position.y + (Input.GetAxis("Vertical") * velocity * Time.deltaTime), -yMax, yMax);
        transform.position = new Vector3(xMove, yMove);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            fireEnginePrefab.GetComponent<SpriteRenderer>().enabled = true;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            fireEnginePrefab.GetComponent<SpriteRenderer>().enabled = false;
            audioSource.Stop();
        }

    }
}
