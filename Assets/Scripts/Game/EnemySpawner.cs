using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public List<GameObject> enemies;
    public List<Transform> spawnPoints;
    [SerializeField] private bool autoSpawn;
    [SerializeField] private float spawnTimer = 30;
    [SerializeField] private float timer;
    void Start()
    {
        if (Instance == null) { Instance = this; }

        timer = spawnTimer;
        SpawnEnemy(3);
    }
    void Update()
    {
        if (autoSpawn)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                timer = spawnTimer;
                SpawnEnemy(Random.Range(1,4));
            }
        }
    }
    public void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (enemies.Count > 0)
            {
                int target = Random.Range(0, enemies.Count);
                if (spawnPoints.Count <= 0)
                {
                    Instantiate(enemies[target], transform);
                }
                else
                {
                    int t2 = Random.Range(0, spawnPoints.Count);
                    Instantiate(enemies[target], spawnPoints[t2].position, Quaternion.identity);
                }
            }
        }
    }
}