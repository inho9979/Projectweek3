using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorStats : MonoBehaviour
{
    //private int level = 1;
    //private int gold = 0;
    //private float exp = 0f;
    private int maxHp = 0;
    private int currentHp = 0;
    private int power = 0;
    private int totalPower = 0;

    public int TotalPower
    {
        get => totalPower;
        set
        {
            totalPower = power * value;
        }
    }

    // maxHp는 첫 생성시 한번만 set - > set 할때 curHP도 같이 초기화
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

    void Start()
    {
        MaxHp = 40;
        Init();
    }

    public void Init()
    {
        Power = 10;
        totalPower = power;
    }

    public void SetStat()
    {

    }
}
