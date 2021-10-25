using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAbility : MonoBehaviour
{
    private int itemPower = 0;
    private bool isHeal = false;

    public int ItemPower
    {
        get => itemPower;
        set
        {
            itemPower = value;
        }
    }
    public bool IsHeal
    {
        get => isHeal;
        set
        {
            isHeal = value;
        }
    }

    public void SetAbility(int power, bool isHeal)
    {
        ItemPower = power;
        IsHeal = isHeal;
    }


    void Start()
    {
    }
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
