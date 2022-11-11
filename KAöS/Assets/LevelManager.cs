using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPunCallbacks
{
    public GameObject core;

    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoint;
    public int numberToSpawn;

    private bool enemyInstantiate;
    
    void Start()
    {
        photonView.RPC("SetPositions", RpcTarget.AllBufferedViaServer);
        
        foreach (var player in GameManager.Instance.playersList)
        {
            player.GetComponent<PlayerNetworkSetup>().InitPlayer();
        }
        
        for (int i = 0; i < numberToSpawn; i++)
        {
            EnemySpawn();
        }
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

    public void EnemySpawn()
    {
        GameObject newEnemy = PhotonNetwork.Instantiate(enemyPrefab.name,
            enemySpawnPoint[Random.Range(0, enemySpawnPoint.Length)].position, Quaternion.identity);

        newEnemy.GetComponent<EnemyBehaviour>()._levelManager = this;
    }
    
}
