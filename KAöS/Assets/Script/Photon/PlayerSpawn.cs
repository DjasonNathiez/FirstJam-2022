using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform core;
    private bool first = true;

    private PlayerController player;

    private void Start()
    {
        PhotonView newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, core.position, Quaternion.Euler(0,0,GameManager.Instance.playersList.Count*90)).GetComponent<PhotonView>();
        GameManager.Instance.AddPlayerToList(newPlayer);
        
    }
}
