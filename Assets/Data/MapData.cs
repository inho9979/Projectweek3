using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    List<Dictionary<string, object>> map_data;
    void Awake()
    {
        map_data = new List<Dictionary<string, object>>();
        map_data = CSVReader.Read("StageLvDesign");
    }

    void Update()
    {
        
    }

    public Dictionary<string, object> GetMapDataLv(int level)
    {
        return map_data[level - 1];
    }
}
