using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySound : MonoBehaviour
{
    private AudioSource lobbyBgm;
    void Start()
    {
        lobbyBgm = GetComponent<AudioSource>();
    }
    void Update()
    {
        lobbyBgm.volume = GameManager.Instance.BGMVolume;
    }
}
