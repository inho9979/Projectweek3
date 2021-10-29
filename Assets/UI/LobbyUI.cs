using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : GenericUI
{
    void Start()
    {
        
    }

    public override void Open()
    {
        base.Open();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(0);
    }
    public void Option()
    {
        LobbyUImanager.Instance.Open(Windows.Option);
    }
    public void Explain()
    {

    }
}
