using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    new public GameObject camera;
    public GameObject player;
    public GameObject ui;
    public GameObject objectManager;
    public GameObject score;
    public GameObject timeLine;

    private PlayerControl playerCtrlComponent;
    private ObjectManager objGenerateComponent;
    private Score scoreComponent;
    private InGameUImanager uiCtrlComponent;
    private Touch touchFuc;

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

    private void Start()
    {
        timeLine.GetComponent<PlayableDirector>().playOnAwake = false;
        timeLine.SetActive(false);
    }

    void Update()
    {
    }

    public void GamePlay()
    {
        GameState = InGameState.Start;
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
        timeLine.SetActive(true);
        timeLine.GetComponent<PlayableDirector>().playOnAwake = true;
    }
    // ���� ������ �ʿ��� ��� ���۵� ����
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        //Pause();
        uiCtrlComponent.GameOverUI();
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
