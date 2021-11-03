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
    //public int sceneNum = -1;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            playerStatInfo = gameObject.GetComponent<PlayerStatData>();
            mapStageInfo = gameObject.GetComponent<MapStageData>();
            instance = this;
            BGMVolume = 0.4f;
            EffectVolume = 0.4f;
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

    private void Update()
    {
    }
}


