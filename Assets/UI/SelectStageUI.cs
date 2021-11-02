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

    // textSet 0: StatTxt, 1: -Text, 2: +Text, 3: StageNumTxt
    // 4: CoinTxt, 5: AtkLvTxt, 6: MoneyTxt

    void Start()
    {
        mapStageData = GameManager.Instance.mapStageInfo;
        Debug.Log(mapStageData);
        textSet = new Text[transform.GetChild(7).childCount];
        textSet = transform.GetChild(7).GetComponentsInChildren<Text>();
        playerStat = GameManager.Instance.playerStatInfo;
    }

    void Update()
    {
        textSet[0].text = $"HP :{playerStat.CurrentHp}\n ATK :{playerStat.Power}" +
            $"\n WallCount: {mapStageData.WallCount} \n WeakHp: {mapStageData.WeakHp}";

        textSet[3].text = mapStageData.StageLv.ToString();
        textSet[4].text = playerStat.LvUpCost.ToString();
        textSet[5].text = playerStat.CurLevel.ToString();
        textSet[6].text = $"{playerStat.Gold}";
    }

    public void ExpUpButton()
    {
        // 20을 추가해서 더해주는동작 프로퍼티!
        //playerStat.CurExp = 20;
        playerStat.LevelUp();

    }

    public void StartStage()
    {
        SceneManager.LoadScene(0);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(1);
    }

    public void PlusButton()
    {
        if(mapStageData.LimitStageLv > mapStageData.StageLv)
        {
            mapStageData.StageLv++;
            mapStageData.SetStageLv(mapStageData.StageLv);
        }
    }

    public void MinusButton()
    {
        if(mapStageData.StageLv > 1)
        {
            mapStageData.StageLv--;
            mapStageData.SetStageLv(mapStageData.StageLv);
        }
    }
}
