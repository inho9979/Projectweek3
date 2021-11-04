using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int scorePoint = 0;
    private int maxCombo = 1;
    private int curCombo = 0;
    private int coin = 0;
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

    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
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

    public void ScoreUp(int wallHp, int playerAtk)
    {
        var attackPoint = wallHp - (wallHp - playerAtk) * -1;
        if (attackPoint <= 0)
            attackPoint = 1;
        scorePoint += (int)(wallHp + ((attackPoint) / 2 + 10) * (curCombo * 1.15));
        //scorePoint += (stageData.ClearGold * 10)/stageData.WallCount;
    }

    public void SetCoin()
    {
        coin = (stageData.ClearGold * maxCombo);
    }
}
