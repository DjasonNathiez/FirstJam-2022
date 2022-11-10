using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerCount;
    
    
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        
        Instance = this;
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
