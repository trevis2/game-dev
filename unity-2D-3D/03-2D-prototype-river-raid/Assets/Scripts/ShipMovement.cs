using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float velocity = 15.0f;
    // Start is called before the first frame update
    [SerializeField] float xMax = 7.5f;
    [SerializeField] float yMax = 8.5f;
    private void Awake()
    {
        Physics2D.gravity = new Vector3(0, 0, 0);
    }
    void Start()
    {
        transform.position = new Vector3(0, -yMax, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Mathf.Clamp(transform.position.x + (Input.GetAxis("Horizontal") * velocity * Time.deltaTime), -xMax, xMax);
        float yMove = Mathf.Clamp(transform.position.y + (Input.GetAxis("Vertical") * velocity * Time.deltaTime), -yMax, yMax);
        transform.position = new Vector3(xMove, yMove);
    }
}
