using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform core;
    private bool first = true;

    private void Start()
    {
        //PhotonNetwork.Instantiate(playerPrefab.name, core.position, Quaternion.Euler(0,0,GameManager.Instance.playerCount*90));
        //GameManager.Instance.playerCount++;
        for (int i = 0; i < 4; i++)
        {
            PlayerController newPlayer = Instantiate(playerPrefab, core.position, Quaternion.Euler(0,0,GameManager.Instance.playersList.Count*90)).GetComponent<PlayerController>();
            if (first) newPlayer.canMove = true;
            first = false;
            GameManager.Instance.AddPlayer(newPlayer);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerController newPlayer = Instantiate(playerPrefab, core.position, Quaternion.Euler(0,0,GameManager.Instance.playersList.Count*90)).GetComponent<PlayerController>();
            if (first) newPlayer.canMove = true;
            first = false;
            GameManager.Instance.AddPlayer(newPlayer);

        }
    }
}
