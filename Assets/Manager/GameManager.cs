using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public InGameManager inGameManager;
    public LobbyUImanager lobbyManager;

    public static GameManager Instance
    {
        get => instance;
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
