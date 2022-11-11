using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public GameObject localPlayer;
    
    public List<GameObject> playersList = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    public void LocalAddPlayerID()
    {
        photonView.RPC("AddPlayerID", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC] public void AddPlayerID()
    {
        playersList.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        
        for (int i = 0; i < playersList.Count; i++)
        {
            PlayerController playerController = playersList[i].GetComponent<PlayerController>();
            
            if (i != 0) playerController.neighboorRight = playersList[i - 1].gameObject;
            else playerController.neighboorRight = playersList[playersList.Count-1].gameObject;
                
            if (i != playersList.Count - 1) playerController.neighboorLeft = playersList[i + 1].gameObject;
            else playerController.neighboorLeft = playersList[0].gameObject;
        }
    }
}
