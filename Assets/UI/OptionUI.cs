using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : GenericUI
{
    public AudioClip clip;
    private Slider[] volumeSlider;

    public Text[] recordTexts;
    void Start()
    {
        volumeSlider = new Slider[2];
        volumeSlider = gameObject.GetComponentsInChildren<Slider>();

        volumeSlider[0].value = GameManager.Instance.BGMVolume;
        volumeSlider[1].value = GameManager.Instance.EffectVolume;
    }

    void Update()
    {
        recordTexts[0].text = $"highest score: {GameManager.Instance.highstScoreInfo}";
        recordTexts[1].text = $"Clear Stage: {GameManager.Instance.clearStageInfo}";
        recordTexts[2].text = $"Max combo: {GameManager.Instance.maxComboInfo}";
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
