using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    
    public GameObject[] players = new GameObject[3];
    public int connectedPlayerCount;
    
    public static GameManager Instance;

    private bool isPlaying;
    
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        _uiManager = FindObjectOfType<UIManager>();
        
        _uiManager.mainMenu.SetActive(true);
        _uiManager.hud.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!players[0].activeSelf)
            {
                players[0].SetActive(true);
                connectedPlayerCount += 1;
                
                _uiManager.zPlayerText.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!players[1].activeSelf)
            {
                players[1].SetActive(true);
                connectedPlayerCount += 1;
                
                _uiManager.sPlayerText.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!players[2].activeSelf)
            {
                players[2].SetActive(true);
                connectedPlayerCount += 1;
                
                _uiManager.oPlayerText.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!players[3].activeSelf)
            {
                players[3].SetActive(true);
                connectedPlayerCount += 1;
                
                _uiManager.lPlayerText.SetActive(false);
            }
        }

        if (connectedPlayerCount == 4 && !isPlaying)
        {
            LoadGame();
        }
    }

    void LoadGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            PlayerController playerController = players[i].GetComponent<PlayerController>();

            players[i].GetComponent<ShootController>().enabled = true;
            
            if (i != 0) playerController.neighboorRight = players[i - 1];
            else playerController.neighboorRight = players[players.Length-1];
                
            if (i != players.Length - 1) playerController.neighboorLeft = players[i + 1];
            else playerController.neighboorLeft = players[0];
        }

        _uiManager.mainMenu.SetActive(false);
        _uiManager.hud.SetActive(true);
        
        isPlaying = true;
    }

}
