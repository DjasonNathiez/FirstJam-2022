using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
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
        DontDestroyOnLoad(gameObject);
        
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

            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && SceneManager.GetActiveScene().name != "GLOBAL_Level")
            {
                PhotonNetwork.LoadLevel("GLOBAL_Level");
                mainMenuGo.SetActive(false);
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
        InstantiatePlayer();
    }
    
    [SerializeField] private GameObject playerPrefab;
    private int joinedPlayer;

    public void InstantiatePlayer()
    {
        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,0,0), Quaternion.Euler(0,0,joinedPlayer * 90));

        newPlayer.name = "player_" + newPlayer.GetComponent<PhotonView>().ViewID;
        
        if (newPlayer.GetComponent<PhotonView>().IsMine)
        {
            GameManager.Instance.localPlayer = newPlayer;
        }
        
        GameManager.Instance.LocalAddPlayerID();
    }
}
