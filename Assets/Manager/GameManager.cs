using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int maxComboInfo;
    public int highstScoreInfo;
    public int clearStageInfo;

    //public int sceneNum = -1;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            playerStatInfo = gameObject.GetComponent<PlayerStatData>();
            mapStageInfo = gameObject.GetComponent<MapStageData>();
            instance = this;
            BGMVolume = 0f;
            EffectVolume = 0f;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        //if (SceneManager.GetActiveScene().name == "GameScene")
        //{
        //    sceneNum = 2;
        //}
        //else if (SceneManager.GetActiveScene().name == "StageSelectScene")
        //{
        //    sceneNum = 1;
        //}
        //else if (SceneManager.GetActiveScene().name == "LobbyScene")
        //{
        //    sceneNum = 0;
        //}
    }

    public void DataSave()
    {
        //SaveSystem.SaveData(playerStatInfo, mapStageInfo);
    }

    public void DataLoad()
    {
        //var data = SaveSystem.LoadData();
        //playerStatInfo.CurLevel = data.level;
        //playerStatInfo.Gold = data.gold;

        //mapStageInfo.StageLv = data.stageLevel;
        //mapStageInfo.LimitStageLv = data.stageLimitLevel;
    }

    private void Update()
    {
    }
}


