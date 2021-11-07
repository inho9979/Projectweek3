using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStageData
{
    // �÷��̾� ������
    public int level;
    public int gold;

    // �������� ������
    public int stageLevel;
    public int stageLimitLevel;

    public int clearStage;
    public int highestScore;
    public int maxCombo;

    // Ʃ�丮�� ���� ����

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
