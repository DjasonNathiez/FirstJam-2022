using System.Collections.Generic;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
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

    public void AddPlayerToList(PhotonView newPlayer)
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBufferedViaServer, newPlayer); 
    }
    
    [PunRPC] public void AddPlayer(PhotonView newPlayer)
    {
        playersList.Add(newPlayer.GetComponent<PlayerController>());

        if (playersList.Count == 4)
        {
            for (int i = 0; i < playersList.Count; i++)
            {
                if (i != 0) playersList[i].neighboorRight = playersList[i - 1];
                else playersList[i].neighboorRight = playersList[playersList.Count-1];
                
                if (i != playersList.Count - 1) playersList[i].neighboorLeft = playersList[i + 1];
                else playersList[i].neighboorLeft = playersList[0];
            } 
        }
        
    }
}
