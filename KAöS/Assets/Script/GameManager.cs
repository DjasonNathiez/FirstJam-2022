using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    public List<PlayerController> playersList = new List<PlayerController>();
    
    
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void AddPlayer(PlayerController newPlayer)
    {
        playersList.Add(newPlayer);

        if (playersList.Count == 4)
        {
            for (int i = 0; i < playersList.Count; i++)
            {
                if (i != 0) playersList[i].neighboorRight = playersList[i - 1];
                else playersList[i].neighboorRight = playersList[playersList.Count-1];
                
                if (i != playersList.Count - 1) playersList[i].neighboorLeft = playersList[i + 1];
                else playersList[i].neighboorLeft = playersList[0];
            }
        Debug.Log("Neighboor Set");
        }
        
    }
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
