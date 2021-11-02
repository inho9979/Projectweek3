using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharactorStats : MonoBehaviour
{
    private int curLevel = 1;
    //private int maxLevel = 20;
    //private int gold = 0;
    //private float curExp = 0f;
    //private float lvUpExp = 10;
    private int maxHp = 0;
    private int currentHp = 0;
    private int power = 0;
    private int totalPower = 0;

    private Slider HpBar;

    public int CurLevel
    {
        get => curLevel;
        set => curLevel = value;
    }
    //public int MaxLevel
    //{
    //    get => maxLevel;
    //}

    //public int Gold
    //{
    //    get => gold;
    //    set => gold = value;
    //}
    // 경험치는 value값을 대입으로 받으면 프로퍼티에서 계산하는 방식
    //public float CurExp
    //{
    //    get => curExp;
    //    set
    //    {
    //        if (curLevel >= maxLevel)
    //            return;
    //        var totalExp = curExp + value;
    //        // 경험치가 한방에 많이 추가되면 이렇게 여러번 레벨업하도록
    //        while(totalExp >= lvUpExp)
    //        {
    //            curLevel++;
    //            totalExp -= lvUpExp;
    //            LevelUp();
    //        }
    //        curExp = totalExp;
    //    }
    //}
    //public float LvUpExp
    //{
    //    get => lvUpExp;
    //}

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
        HpBar = GameObject.FindWithTag("PlayerStat").transform.GetChild(2).GetComponent<Slider>();
        Init();
    }

    public void powerInit()
    {
        totalPower = Power;
    }

    public void Init()
    {
        curLevel = GameManager.Instance.playerStatInfo.CurLevel;
        MaxHp = GameManager.Instance.playerStatInfo.MaxHp;
        Power = GameManager.Instance.playerStatInfo.Power;
        //curLevel = 1;
        //MaxHp = 100;
        //Power = 10;
        totalPower = Power;
    }

    public void LevelUp()
    {
        //int.TryParse(data["PlayerHP"].ToString(), out maxHp);
        
        //var data = playerTable.GetPlayerDataLv(curLevel);
        //Power = Int32.Parse(data["PlayerATK"].ToString());
        //MaxHp = Int32.Parse(data["PlayerHP"].ToString());
        //lvUpExp = Int32.Parse(data["LvUpCost"].ToString());

    }

    private void Update()
    {
        float curHp = (float)currentHp / (float)MaxHp;
        HpBar.value = Mathf.Lerp(HpBar.value, curHp, Time.deltaTime * 5f);
    }
}
