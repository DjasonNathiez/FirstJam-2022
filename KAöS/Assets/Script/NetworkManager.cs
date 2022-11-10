using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Interface")] 
    public TextMeshProUGUI serverStatue;
    public Button connectToServerBtn;

    public GameObject connectScreenGo;
    public GameObject mainMenuGo;

    public TextMeshProUGUI roomInputFieldInfo;
    public TextMeshProUGUI roomGeneratedText;

    public string generateCode;

    public int countToJoinRoom = 4;
    private RoomOptions _roomOptions;
    private TypedLobby _typedLobby;

    private void Awake()
    {
        connectScreenGo.SetActive(true);

        _roomOptions = new RoomOptions
        {
            IsOpen = true,
            IsVisible = true,
            EmptyRoomTtl = 0,
            MaxPlayers = 4
        };

        _typedLobby = new TypedLobby("Hub", LobbyType.Default);
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            serverStatue.text = "Waiting for player : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + countToJoinRoom + " in room " + PhotonNetwork.CurrentRoom.Name + " with current lobby is " + PhotonNetwork.CurrentLobby;

            if (PhotonNetwork.CurrentRoom.PlayerCount == countToJoinRoom)
            {
                PhotonNetwork.LoadLevel(1);
            }
            
        }
    }

    private void SetServerStatueText(string text)
    {
        serverStatue.text = text;
    }
    
    public void ConnectLocalClient()
    {
        SetServerStatueText("Connecting...");
        connectToServerBtn.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        SetServerStatueText("Connected");
        base.OnConnectedToMaster();
        
        connectScreenGo.SetActive(false);
        mainMenuGo.SetActive(true);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInputFieldInfo.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError(returnCode + " ; " + message );
        base.OnJoinRoomFailed(returnCode, message);
        PhotonNetwork.CreateRoom(roomInputFieldInfo.text);
    }


    public void CreateRoom()
    {
        string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        char[] generated = new char[4];

        for (int i = 0; i < generated.Length; i++)
        {
            generated[i] = st[Random.Range(0, st.Length)];
            Debug.Log(generated[i]);
        }

        for (int i = 0; i < generated.Length; i++)
        {
            generateCode = generateCode.Insert(i, generated[i].ToString());
        }

        roomGeneratedText.text = "Room code : " + generateCode;
        
        PhotonNetwork.CreateRoom(generateCode); 
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }

}
