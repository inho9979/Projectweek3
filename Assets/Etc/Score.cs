using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int scorePoint = 0;
    private int maxCombo = 1;
    private int curCombo = 0;
    private int getCoin = 0;
    private int bonusCount;

    private MapStageData stageData;

    public int MaxCombo
    {
        get => maxCombo;
    }

    public int CurCombo
    {
        get => curCombo;
        set
        {
            curCombo = value;
        }
    }

    public int GetCoin
    {
        get => getCoin;
        set
        {
            getCoin = value;
        }
    }

    public int BonusCount
    {
        get => bonusCount;
        set
        {
            bonusCount = value;
        }
    }

    public int ScorePoint
    {
        get => scorePoint;
        set
        {
            scorePoint = value;
        }
    }

    void Start()
    {
        scorePoint = 0;
        stageData = GameManager.Instance.mapStageInfo;
    }

    void Update()
    {
        if (maxCombo <= curCombo)
            maxCombo = curCombo;
    }

    public void ScoreUp()
    {
        scorePoint += (stageData.ClearGold * 10)/stageData.WallCount;
        getCoin += (stageData.ClearGold) / stageData.WallCount;
    }
}
