using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : GenericUI
{
    public AudioClip clip;
    private Slider[] volumeSlider;
    void Start()
    {
        volumeSlider = new Slider[2];
        volumeSlider = gameObject.GetComponentsInChildren<Slider>();

        volumeSlider[0].value = GameManager.Instance.BGMVolume;
        volumeSlider[1].value = GameManager.Instance.EffectVolume;
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
        SoundManager.Instance.SFXPlay("OptionExit", clip);
        LobbyUImanager.Instance.Open(Windows.Start);
    }

    public void BGMvolumeChange()
    {
        GameManager.Instance.BGMVolume = volumeSlider[0].value;
    }

    public void EffectVolumeChange()
    {
        GameManager.Instance.EffectVolume = volumeSlider[1].value;
    }
}
