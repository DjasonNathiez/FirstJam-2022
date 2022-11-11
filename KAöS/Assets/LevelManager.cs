using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviourPunCallbacks
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

    void Start()
    {
        photonView.RPC("SetPositions", RpcTarget.AllBufferedViaServer);

        foreach (var player in GameManager.Instance.playersList)
        {
            player.GetComponent<PlayerNetworkSetup>().InitPlayer();
        }
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
        
        GameObject newEnemy = PhotonNetwork.Instantiate(enemySpawner.enemyPrefab.name,
            enemySpawner.enemySpawnPoints[Random.Range(0, enemySpawner.enemySpawnPoints.Length)].position,
            Quaternion.identity);
    }

    [PunRPC]
    private void SetPositions()
    {
        for (int i = 0; i < GameManager.Instance.playersList.Count; i++)
        {
            GameManager.Instance.transform.position = core.transform.position;
            GameManager.Instance.playersList[i].transform.rotation = Quaternion.Euler(0, 0, i * 90);
        }
    }

    public void EnemySpawn(GameObject enemyToSpawn)
    {
        GameObject newEnemy = PhotonNetwork.Instantiate(enemyToSpawn.name,
            enemySpawnPoint[Random.Range(0, enemySpawnPoint.Length)].position, Quaternion.identity);

        newEnemy.GetComponent<EnemyBehaviour>()._levelManager = this;
    }
    
}
