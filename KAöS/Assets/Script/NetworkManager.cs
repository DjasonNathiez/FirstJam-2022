using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    
    [Header("Interface")] 
    public TextMeshProUGUI serverStatue;
    public Button connectToServerBtn;

    public GameObject connectScreenGo;
    public GameObject mainMenuGo;

    public TextMeshProUGUI roomInputFieldInfo;

    public byte countToJoinRoom = 4;
    private RoomOptions _roomOptions;
    private TypedLobby _typedLobby;

    private void Awake()
    {
        connectScreenGo.SetActive(true);

        _roomOptions = new RoomOptions
        {
            PlayerTtl = 1000,
            EmptyRoomTtl = 0,
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = countToJoinRoom
        };
    }
    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            serverStatue.text = "Waiting for player : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + " in room " + PhotonNetwork.CurrentRoom.Name + " with current lobby is " + PhotonNetwork.CurrentLobby;

            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
        else
        {
            serverStatue.text = "Not in a room";
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
        base.OnConnectedToMaster();
        SetServerStatueText("Connected...");
        connectScreenGo.SetActive(false);
        mainMenuGo.SetActive(true);
    }

    public void JoinRoom()
    {
        string room = roomInputFieldInfo.text;
        PhotonNetwork.JoinOrCreateRoom(room, _roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }
}
