using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public GameObject core;

    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoint;
    public int numberToSpawn;
    private bool enemyInstantiate;
    public EnemySpawner[] enemySpawners;

    public float timer;
    public float frequencyLimiter;

    public float spawnLatency;
    
    [Serializable] public class EnemySpawner
    {
        [Range(1,60)] public float frequency;
        public GameObject enemyPrefab;
        public int enemyMinSpawnCount;
        public int enemyMaxSpawnCount;
        public Transform[] enemySpawnPoints;
    }
    
    private void Awake()
    {
        core = FindObjectOfType<CoreManager>().gameObject;
    }

    private void Update()
    {
        if (timer <= frequencyLimiter)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        
        Spawner();
    }

    void Spawner()
    {
        foreach (var enemySpawner in enemySpawners)
        {
            if (Math.Abs(timer % enemySpawner.frequency) < 0.005)
            {
                int countToSpawn = Random.Range(enemySpawner.enemyMinSpawnCount, enemySpawner.enemyMaxSpawnCount);

                for (int i = 0; i < countToSpawn; i++)
                {
                    StartCoroutine(ReturnTheGod(enemySpawner));
                }
            }
        }
    }

    IEnumerator ReturnTheGod(EnemySpawner enemySpawner)
    {
        yield return new WaitForSeconds(spawnLatency);

        int point = Random.Range(0, enemySpawner.enemySpawnPoints.Length);
        Vector3 spawnPoint = new Vector3(enemySpawner.enemySpawnPoints[point].position.x + Random.Range(0,5), enemySpawner.enemySpawnPoints[point].position.x + Random.Range(0,5), 0);
        
        GameObject newEnemy = Instantiate(enemySpawner.enemyPrefab,
            enemySpawner.enemySpawnPoints[Random.Range(0, enemySpawner.enemySpawnPoints.Length)].position,
            Quaternion.identity);
    }
}
