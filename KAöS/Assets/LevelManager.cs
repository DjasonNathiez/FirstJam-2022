using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public GameObject enemyPrefab;
    //public Transform[] enemySpawnPoint;
    //public int numberToSpawn;
    //private bool enemyInstantiate;
    //public EnemySpawner[] enemySpawners;

    private float timer;
    //public float frequencyLimiter;

    public float spawnLatency;
    public bool spawning;
    
   /* [Serializable] public class EnemySpawner
    {
        [Range(1,60)] public float frequency;
        public GameObject enemyPrefab;/*
        public int enemyMinSpawnCount;
        public int enemyMaxSpawnCount;
        public Transform[] enemySpawnPoints;
    }*/

   private void Awake()
   {
       Instance = this;
   }


   private void Update()
    {
        if (timer <= spawnLatency)
        {
           if(spawning) timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            SpawnEnemy();
        }
        
        //Spawner();
    }
    
    void SpawnEnemy()
    {
        float x = 0;
        float y = 0;

        int rdm = Random.Range(0, 4);

        switch (rdm)
        {
            case 0:
                x = Random.Range(1.1f, 1.3f);
                y = Random.Range(-.3f, 1.2f);
                break;
            case 1:
                x = Random.Range(-.3f, -.1f);
                y = Random.Range(-.3f, 1.2f);
                break;
            case 2:
                y = Random.Range(1.1f, 1.3f);
                x = Random.Range(-.3f, 1.2f);
                break;
            case 3:
                y = Random.Range(-.3f, -.1f);
                x = Random.Range(-.3f, 1.2f);
                break;
        }

        Vector2 pos = Camera.main.ViewportToWorldPoint(new Vector3(x,y));
        Instantiate(enemyPrefab, pos, Quaternion.identity, transform);
    }

    /*void Spawner()
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
    }*/
    
}
