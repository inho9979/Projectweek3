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

    public PlayerStageData (PlayerStatData playerData, MapStageData stageData)
    {
        level = playerData.CurLevel;
        gold = playerData.Gold;

        stageLevel = stageData.StageLv;
        stageLimitLevel = stageData.LimitStageLv;
    }
}
