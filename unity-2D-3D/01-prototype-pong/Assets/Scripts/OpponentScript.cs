
using UnityEngine;

public class OpponentScript : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    // Update is called once per frame

    void Update()
    {
        float yToFollow = objectToFollow.transform.position.y;
        float yClamped = Mathf.Clamp(yToFollow, -1.0f, 1.0f);
        transform.position = new Vector3(transform.position.x, yClamped, transform.position.z);

    }
}
