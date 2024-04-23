using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
