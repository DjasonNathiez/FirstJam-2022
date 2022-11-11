using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] players = new GameObject[3];
    public int connectedPlayerCount;
    
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(!players[0].activeSelf) players[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(!players[1].activeSelf) players[1].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if(!players[2].activeSelf) players[2].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if(!players[3].activeSelf) players[3].SetActive(true);
            
        }
    }

    public void AddPlayerID()
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
