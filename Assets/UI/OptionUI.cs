using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : GenericUI
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public override void Open()
    {
        base.Open();
    }

    public void Exit()
    {
        LobbyUImanager.Instance.Open(Windows.Start);
    }
}
