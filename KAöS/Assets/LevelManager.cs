using Photon.Pun;
using UnityEngine;

public class LevelManager : MonoBehaviourPunCallbacks
{
    public GameObject core;
    void Start()
    {
        photonView.RPC("SetPositions", RpcTarget.AllBufferedViaServer);
        
        foreach (var player in GameManager.Instance.playersList)
        {
            player.GetComponent<PlayerNetworkSetup>().InitPlayer();
        }
    }

    [PunRPC]
    private void SetPositions()
    {
        for (int i = 0; i < GameManager.Instance.playersList.Count; i++)
        {
            GameManager.Instance.transform.position = core.transform.position;
            GameManager.Instance.playersList[i].transform.rotation = Quaternion.Euler(0, 0, i * 90);
        }
    }
    
}
