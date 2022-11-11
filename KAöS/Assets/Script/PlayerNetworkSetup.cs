using Photon.Pun;
using UnityEngine;

public class PlayerNetworkSetup : MonoBehaviour
{
    private PlayerController _playerController;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _playerController = GetComponent<PlayerController>();
    }

    public void InitPlayer()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            _playerController.enabled = true;
            _playerController.canMove = true;
        }
        else
        {
            _playerController.enabled = false;

        }
    }
}
