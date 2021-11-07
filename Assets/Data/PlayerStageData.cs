using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStageData
{
    // 플레이어 데이터
    public int level;
    public int gold;

    // 스테이지 데이터
    public int stageLevel;
    public int stageLimitLevel;

    public int clearStage;
    public int highestScore;
    public int maxCombo;

    // 튜토리얼 실행 여부

    public bool isTutorial;

    public PlayerStageData (PlayerStatData playerData, MapStageData stageData)
    {
        level = playerData.CurLevel;
        gold = playerData.Gold;

        stageLevel = stageData.StageLv;
        stageLimitLevel = stageData.LimitStageLv;

        clearStage = GameManager.Instance.clearStageInfo;
        highestScore = GameManager.Instance.highstScoreInfo;
        maxCombo = GameManager.Instance.maxComboInfo;

        isTutorial = GameManager.Instance.tutorialOn;
    }
}
