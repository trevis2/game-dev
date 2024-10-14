using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyMover))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    int currentHitPoint = 0;

    Enemy enemy;
    EnemyMover enemyMover;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyMover = GetComponent<EnemyMover>();
    }

    void OnEnable()
    {
        currentHitPoint = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoint--;
        if (currentHitPoint <= 0)
        {
            enemy.RewardGold();
            gameObject.SetActive(false);
            maxHitPoints += enemy.DifficultyRamp;
            enemy.IncreaseDifficulty();
            enemyMover.IncreaseSpeed();
        }
    }
}
