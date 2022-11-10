using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [Header("Input")] 
    [SerializeField] private KeyCode leftInput;
    [SerializeField] private KeyCode rightInput;
    
    
    
    private PhotonView view;

// Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        
        
        if(!view.IsMine)return;
        
        
    }

    // Update is called once per frame
    void Update()
    {


        //if(!view.IsMine) return;

        Debug.Log("view");

        if (Input.GetKey(leftInput)) transform.Rotate(Vector3.forward);
        if (Input.GetKey(rightInput)) transform.Rotate(Vector3.back);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
