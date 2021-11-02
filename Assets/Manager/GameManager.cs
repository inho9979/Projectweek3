using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get => instance;
    }

    public float BGMVolume;
    public float EffectVolume;
    public PlayerStatData playerStatInfo;
    public MapStageData mapStageInfo;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            playerStatInfo = gameObject.GetComponent<PlayerStatData>();
            mapStageInfo = gameObject.GetComponent<MapStageData>();
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
