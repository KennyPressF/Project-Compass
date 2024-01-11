using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] Transform spawnedEnemies;
    [SerializeField] GameObject spawnPointGroup;
    [SerializeField] Transform[] spawnPoints;

    [Header("Spawn Settings")]
    [SerializeField] float minTimeBetweenSpawns;
    [SerializeField] float maxTimeBetweenSpawns;
    [SerializeField] float[] spawnTimers;

    [Header("Enemies")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize;
    private List<GameObject> enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSpawnPointArray();
        InitializeEnemyObjectPool();
        InitializeSpawnTimers();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnTimers();
    }

    void InitializeSpawnPointArray()
    {
        int childCount = spawnPointGroup.transform.childCount;
        spawnPoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            spawnPoints[i] = spawnPointGroup.transform.GetChild(i);
        }
    }

    void InitializeEnemyObjectPool()
    {
        enemyPool = new List<GameObject>();

        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        //    enemy.SetActive(false);
        //    enemy.transform.parent = spawnedEnemies;
        //    enemyPool.Add(enemy);
        //}
    }

    void InitializeSpawnTimers()
    {
        spawnTimers = new float[spawnPoints.Length];

        for (int i = 0; i < spawnTimers.Length; i++)
        {
            spawnTimers[i] = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
    }

    void UpdateSpawnTimers()
    {
        for (int i = 0; i < spawnTimers.Length; i++)
        {
            spawnTimers[i] -= Time.deltaTime;

            // If a spawn timer reaches zero, reset it with a new random value and spawn enemies
            if (spawnTimers[i] <= 0f)
            {
                // Spawn enemies for the current spawn point
                SpawnEnemy(spawnPoints[i]);

                // Reset the spawn timer with a new random value
                spawnTimers[i] = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            }
        }
    }

    void SpawnEnemy(Transform spawnPoint)
    {
        // Search for an inactive enemy in the pool
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                // Reuse the inactive enemy
                enemyPool[i].transform.position = spawnPoint.position;
                enemyPool[i].transform.rotation = Quaternion.identity;
                enemyPool[i].SetActive(true);
                return;
            }
        }

        // If no inactive enemy is found, instantiate a new one and add it to the pool
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        newEnemy.transform.parent = spawnedEnemies;
        enemyPool.Add(newEnemy);
    }
}
