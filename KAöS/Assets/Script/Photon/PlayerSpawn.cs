using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform core;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, core.position, Quaternion.Euler(0,0,GameManager.Instance.playerCount*90));
        GameManager.Instance.playerCount++;
    }
}
