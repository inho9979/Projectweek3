using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerStatData : MonoBehaviour
{
    private int curLevel = 1;
    private int maxLevel = 300;
    private int gold = 300;
    //private float curExp = 0f;
    private int lvUpCost = 10;
    private int maxHp = 0;
    private int currentHp = 0;
    private int power = 0;
    private int totalPower = 0;

    private PlayerData playerTable;

    public int CurLevel
    {
        get => curLevel;
        set => curLevel = value;
    }

    public int MaxLevel
    {
        get => maxLevel;
    }

    public int Gold
    {
        get => gold;
        set => gold = value;
    }

    public int LvUpCost
    {
        get => lvUpCost;
        set
        {
            lvUpCost = value;
        }
    }

    public int MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            currentHp = maxHp;
        }
    }
    public int CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = value;
        }
    }
    public int Power
    {
        get => power;
        set
        {
            power = value;
        }
    }
    public int TotalPower
    {
        get => totalPower;
        set
        {
            totalPower = power * value;
        }
    }

    private void Awake()
    {
    }

    void Start()
    {
        playerTable = GameManager.Instance.GetComponent<PlayerData>();
        Init();
    }

    public void Init()
    {
        var data = playerTable.GetPlayerDataLv(curLevel);
        Power = Int32.Parse(data["PlayerATK"].ToString());
        MaxHp = Int32.Parse(data["PlayerHP"].ToString());
        LvUpCost = Int32.Parse(data["CostGoldLvUp"].ToString());
    }

    public void LevelUp()
    {
        if(gold < lvUpCost || curLevel >= maxLevel)
        {
            return;
        }

        curLevel++;
        gold -= lvUpCost;
        var data = playerTable.GetPlayerDataLv(curLevel);
        Power = Int32.Parse(data["PlayerATK"].ToString());
        MaxHp = Int32.Parse(data["PlayerHP"].ToString());
        LvUpCost = Int32.Parse(data["CostGoldLvUp"].ToString());

    }
    public void SetStat(Dictionary<string, object> playerData)
    {
    }

    private void Update()
    {
    }
}


// ����ġ�� value���� �������� ������ ������Ƽ���� ����ϴ� ���
//public float CurExp
//{
//    get => curExp;
//    set
//    {
//        if (curLevel >= maxLevel)
//            return;
//        var totalExp = curExp + value;
//        // ����ġ�� �ѹ濡 ���� �߰��Ǹ� �̷��� ������ �������ϵ���
//        while(totalExp >= lvUpExp)
//        {
//            curLevel++;
//            totalExp -= lvUpExp;
//            LevelUp();
//        }
//        curExp = totalExp;
//    }
//}