using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorStats : MonoBehaviour
{
    //private int level = 1;
    //private int gold = 0;
    //private float exp = 0f;
    private int maxHp = 0;
    private int currentHp = 0;
    private int power = 0;
    private int totalPower = 0;

    private Slider HpBar;

    public int TotalPower
    {
        get => totalPower;
        set
        {
            totalPower = power * value;
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

    void Start()
    {
        HpBar = GameObject.FindWithTag("PlayerStat").transform.GetChild(2).GetComponent<Slider>();
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

    private void Update()
    {
        float curHp = (float)currentHp / (float)MaxHp;
       HpBar.value = Mathf.Lerp(HpBar.value, curHp, Time.deltaTime * 5f);
    }
}
