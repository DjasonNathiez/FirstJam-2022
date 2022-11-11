using System;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform core;
    private bool first = true;

    private PlayerController player;
    
    public void InstantiatePlayer()
    {
        PhotonView newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, core.position, Quaternion.Euler(0,0,GameManager.Instance.playersList.Count*90)).GetComponent<PhotonView>();
    }

    public void Start()
    {
        GameManager.Instance.LocalAddPlayerID();
    }
}
