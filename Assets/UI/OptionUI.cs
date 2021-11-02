using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : GenericUI
{

    private Slider[] volumeSlider;
    void Start()
    {
        volumeSlider = new Slider[2];
        volumeSlider = gameObject.GetComponentsInChildren<Slider>();
    }

    void Update()
    {
        
    }
    public override void Open()
    {
        base.Open();
    }

    public void Exit()
    {
        LobbyUImanager.Instance.Open(Windows.Start);
    }

    public void BGMvolumeChange()
    {
        GameManager.Instance.BGMVolume = volumeSlider[0].value;
    }

    public void EffectVolumeChange()
    {
        GameManager.Instance.BGMVolume = volumeSlider[1].value;
    }
}
