using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    new public GameObject camera;
    public GameObject player;
    public GameObject ui;
    public GameObject objectManager;
    public GameObject score;

    private PlayerControl playerCtrlComponent;
    private ObjectManager objGenerateComponent;
    private Score scoreComponent;
    private UIcontrol uiCtrlComponent;


    void Awake()
    {
        instance = this;
        playerCtrlComponent = player.GetComponent<PlayerControl>();
        objGenerateComponent = objectManager.GetComponent<ObjectManager>();
        scoreComponent = score.GetComponent<Score>();
        uiCtrlComponent = ui.GetComponent<UIcontrol>();
    }

    void Update()
    {
        BonusStateCheck();
    }

    public void Init()
    {

    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    // 마찬가지.. 아직 미정
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    // 마찬가지.. 아직 미정
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    // 클리어될시 필요한 모든 동작들 실행
    public void StageClear()
    {
        playerCtrlComponent.StageClear();
        objGenerateComponent.BonusGenerate();
        scoreComponent.BonusCount = 10;/*(scoreComponent.ScorePoint) / 10;*/

    }
    // 게임 오버에 필요한 모든 동작들 실행
    public void GameOver()
    {
        Pause();
        uiCtrlComponent.GameOverUI();
    }

    public void GameClear()
    {
        uiCtrlComponent.GameClearUI();
    }

    public void BonusStateCheck()
    {
        if (scoreComponent.BonusCount <= 0)
            playerCtrlComponent.IsBonus = false;
    }
}
