using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    List<Dictionary<string, object>> player_data;

    void Awake()
    {
        player_data = new List<Dictionary<string, object>>();
        player_data = CSVReader.Read("PlayerLvDesign");
        //for(int i=0; i<player_data.Count; i++)
        //{
        //    print(player_data[i]["PlayerATK"].ToString());
        //}
    }

    void Update()
    {
        
    }

    public Dictionary<string, object> GetPlayerDataLv(int level)
    {
        return player_data[level - 1];
    }
}
