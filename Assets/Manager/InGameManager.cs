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
    private InGameUImanager uiCtrlComponent;
    private Touch touchFuc;

    public bool playerFinishTrigger = false;

    public enum InGameState
    {
        Tutorial,
        Start,
        Play,
        Pause,
        Bonus,
        Clear,
        GameOver
    }
    private InGameState gameState;
    public InGameState GameState
    {
        get => gameState;
        set
        {
            gameState = value;
            switch (gameState)
            {
                case InGameState.Tutorial:
                    uiCtrlComponent.ChangeState(gameState);
                    playerCtrlComponent.ChangeState(gameState);
                    objGenerateComponent.ChangeState(gameState);
                    break;
                case InGameState.Start:
                    uiCtrlComponent.ChangeState(gameState);
                    playerCtrlComponent.ChangeState(gameState);
                    objGenerateComponent.ChangeState(gameState);
                    break;
                case InGameState.Play:
                    uiCtrlComponent.ChangeState(gameState);
                    playerCtrlComponent.ChangeState(gameState);
                    Resume();
                    break;
                case InGameState.Pause:
                    uiCtrlComponent.ChangeState(gameState);
                    playerCtrlComponent.ChangeState(gameState);
                    objGenerateComponent.ChangeState(gameState);
                    Pause();
                    break;
                case InGameState.Bonus:
                    uiCtrlComponent.ChangeState(gameState);
                    playerCtrlComponent.ChangeState(gameState);
                    objGenerateComponent.ChangeState(gameState);
                    Bonus();
                    break;
                case InGameState.Clear:
                    uiCtrlComponent.ChangeState(gameState);
                    playerCtrlComponent.ChangeState(gameState);
                    objGenerateComponent.ChangeState(gameState);
                    break;
                case InGameState.GameOver:
                    uiCtrlComponent.ChangeState(gameState);
                    playerCtrlComponent.ChangeState(gameState);
                    objGenerateComponent.ChangeState(gameState);
                    StartCoroutine(GameOver());
                    break;
            }
        }
    }

    void Awake()
    {
        instance = this;
        playerCtrlComponent = player.GetComponent<PlayerControl>();
        objGenerateComponent = objectManager.GetComponent<ObjectManager>();
        scoreComponent = score.GetComponent<Score>();
        uiCtrlComponent = ui.GetComponent<InGameUImanager>();
        touchFuc = GameObject.FindWithTag("Touch").GetComponent<Touch>();
        GameState = InGameState.Tutorial;
    }

    void Update()
    {
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Bonus()
    {
    }
    // 게임 오버에 필요한 모든 동작들 실행
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        uiCtrlComponent.GameOverUI();
        //Pause();
    }

    public void GameClear()
    {
    }

    //public void BonusStateCheck()
    //{
    //    if (scoreComponent.BonusCount <= 0)
    //        playerCtrlComponent.IsBonusRun = false;
    //}
}
