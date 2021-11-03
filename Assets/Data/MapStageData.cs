using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapStageData : MonoBehaviour
{
    private int stageLv = 1;
    private int maxStageLv = 300;
    private int limitStageLv = 3;

    private int wallCount;
    private int normalHp;
    private int weakHp;
    public int[] atkPackCount = new int[2];
    public int[] healPackCount = new int[2];
    private int clearGold;

    private MapData mapTable;
    public int StageLv
    {
        get => stageLv;
        set
        {
            stageLv = value;
        }
    }
    public int MaxStageLv
    {
        get => maxStageLv;
    }

    public int LimitStageLv
    {
        get => limitStageLv;
        set
        {
            limitStageLv = value;
        }
    }

    public int WallCount
    {
        get => wallCount;
        set => wallCount = value;
    }
    public int NormalHp
    {
        get => normalHp;
        set => normalHp = value;
    }
    public int WeakHp
    {
        get => weakHp;
        set => weakHp = value;
    }
    public int ClearGold
    {
        get => clearGold;
        set => clearGold = value;
    }

    void Start()
    {
        mapTable = GameManager.Instance.GetComponent<MapData>();
        maxStageLv = 300;
        Init();
    }

    void Init()
    {
        var data = mapTable.GetMapDataLv(stageLv);
        WallCount = Int32.Parse(data["Totalwalls"].ToString());
        NormalHp = Int32.Parse(data["NormalWallHP"].ToString());
        WeakHp = Int32.Parse(data["WeakWallHP"].ToString());
        atkPackCount[0] = Int32.Parse(data["MinATKPAC"].ToString());
        atkPackCount[1] = Int32.Parse(data["MaxATKPAC"].ToString());
        healPackCount[0] = Int32.Parse(data["MinHealPAC"].ToString());
        healPackCount[1] = Int32.Parse(data["MaxHealPAC"].ToString());
        clearGold = Int32.Parse(data["ClearBasicGold"].ToString());
    }

    public void SetStageLv(int level)
    {
        stageLv = level;

        var data = mapTable.GetMapDataLv(stageLv);
        WallCount = Int32.Parse(data["Totalwalls"].ToString());
        NormalHp = Int32.Parse(data["NormalWallHP"].ToString());
        WeakHp = Int32.Parse(data["WeakWallHP"].ToString());
        atkPackCount[0] = Int32.Parse(data["MinATKPAC"].ToString());
        atkPackCount[1] = Int32.Parse(data["MaxATKPAC"].ToString());
        healPackCount[0] = Int32.Parse(data["MinHealPAC"].ToString());
        healPackCount[1] = Int32.Parse(data["MaxHealPAC"].ToString());
        clearGold = Int32.Parse(data["ClearBasicGold"].ToString());
    }

    void Update()
    {
        
    }
}
