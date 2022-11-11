using UnityEngine;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        foreach (var player in GameManager.Instance.playersList)
        {
            player.GetComponent<PlayerNetworkSetup>().InitPlayer();
        }
    }

}
