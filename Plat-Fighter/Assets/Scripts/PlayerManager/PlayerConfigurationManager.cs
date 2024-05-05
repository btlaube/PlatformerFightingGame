using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]
    private int MaxPlayers = 2;

    public static PlayerConfigurationManager Instance { get; private set; }

    private PlayerInputManager inputManager;

    void OnEnable()
    {
        

        // Subscribe to player left event
        // inputManager.playerLeft += OnPlayerLeft;
    }

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        // Subscribe to player joined event
        Debug.Log(inputManager.playerJoinedEvent);
        Debug.Log("fart");

        if(Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
        
    }

    public void HandlePlayerJoin()
    {
        // Debug.Log("player joined " + pi.playerIndex);
        // pi.GameObject.transform.SetParent(transform);

        // if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        // {
        //     playerConfigs.Add(new PlayerConfiguration(pi));
        // }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].playerMaterial = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        // PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public PlayerInput Input { get; private set; }
    public int PlayerIndex { get; private set; }
    public bool isReady { get; set; }
    public Material playerMaterial {get; set;}
}