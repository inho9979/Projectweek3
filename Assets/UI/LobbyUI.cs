using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : GenericUI
{
    public AudioClip clip;

    void Start()
    {
        
    }

    public override void Open()
    {
        base.Open();
    }

    public void GameStart()
    {
        SoundManager.Instance.SFXPlay("GameStartBtn", clip);
        LobbyUImanager.Instance.Open(Windows.Option);
        SceneManager.LoadScene(2);
    }
    public void Option()
    {
        SoundManager.Instance.SFXPlay("OptionBtn", clip);
        LobbyUImanager.Instance.Open(Windows.Option);
    }
    public void Rule()
    {
        SoundManager.Instance.SFXPlay("RuleBtn", clip);
    }
}
