using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SelectStageUI : MonoBehaviour
{

    private PlayerStatData playerStat;
    private MapStageData mapStageData;
    private Text[] textSet;

    public AudioClip buttonClick;
    // textSet 0: StatTxt, 1: -Text, 2: +Text, 3: StageNumTxt
    // 4: CoinTxt, 5: AtkLvTxt, 6: MoneyTxt

    void Start()
    {
        mapStageData = GameManager.Instance.mapStageInfo;
        Debug.Log(mapStageData);
        textSet = new Text[transform.GetChild(9).childCount];
        textSet = transform.GetChild(9).GetComponentsInChildren<Text>();
        playerStat = GameManager.Instance.playerStatInfo;

        textSet[3].text = $"{GameManager.Instance.mapStageInfo.StageLv}";
    }

    void Update()
    {
        textSet[0].text = $"  LV. {playerStat.CurLevel}";
        textSet[1].text = $"{playerStat.MaxHp}";
        textSet[2].text = $"{playerStat.Power}";
        textSet[4].text = playerStat.LvUpCost.ToString();
        textSet[5].text = $"{playerStat.Gold}";
    }

    public void ExpUpButton()
    {
        SoundManager.Instance.SFXPlay("ExpUp", buttonClick);
        playerStat.LevelUp();
    }

    public void StartStage()
    {
        SoundManager.Instance.SFXPlay("ExpUp", buttonClick);
        mapStageData.StageLv = int.Parse(textSet[3].text);
        mapStageData.SetStageLv(mapStageData.StageLv);
        SceneManager.LoadScene(2);
    }

    public void BackButton()
    {
        SoundManager.Instance.SFXPlay("ExpUp", buttonClick);
        SceneManager.LoadScene(0);
    }

    //public void PlusButton()
    //{
    //    SoundManager.Instance.SFXPlay("ExpUp", buttonClick);
    //    if (mapStageData.LimitStageLv > mapStageData.StageLv)
    //    {
    //        mapStageData.StageLv++;
    //        mapStageData.SetStageLv(mapStageData.StageLv);
    //    }
    //}

    //public void MinusButton()
    //{
    //    SoundManager.Instance.SFXPlay("ExpUp", buttonClick);
    //    if (mapStageData.StageLv > 1)
    //    {
    //        mapStageData.StageLv--;
    //        mapStageData.SetStageLv(mapStageData.StageLv);
    //    }
    //}

    public void StageBtn()
    {

    }
}
