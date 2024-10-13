using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0.5f, 30.0f)] float spawnTimer = 2.0f;
    [SerializeField][Range(0, 50)] int poolSize = 5;
    GameObject[] pool;
    private void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int counter = 0; counter < pool.Length; counter++)
        {
            pool[counter] = Instantiate(enemyPrefab, transform);
            pool[counter].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int counter = 0; counter < pool.Length; counter++)
        {
            if (pool[counter].activeInHierarchy == false)
            {
                pool[counter].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
