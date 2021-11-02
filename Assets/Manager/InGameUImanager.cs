using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InGameUImanager : MonoBehaviour, IStateChangeable
{

    public GameObject ingameUI;
    public GameObject pauseUI;
    public GameObject gameoverUI;
    public GameObject gameClearUI;
    public GameObject tutorialUI;

    private Text scoreTxt;
    private Text[] playerTxtUI;
    private Score scoreObj;
    private CharactorStats playerStats;
    public GameObject[] tutoStep;

    private int tutoStepNum = 0;
    private bool isTutorial = false;
    public bool IsTutorial
    {
        get => isTutorial;
        set
        {
            isTutorial = value;
        }
    }
    void Awake()
    {
        ingameUI.SetActive(true);
        tutorialUI.SetActive(true);
        pauseUI.SetActive(false);
        gameoverUI.SetActive(false);
        gameClearUI.SetActive(false);

        tutoStep = new GameObject[tutorialUI.transform.childCount - 1];
        for (int i = 0; i < tutoStep.Length; i++) 
        {
            tutoStep[i] = tutorialUI.transform.GetChild(i).gameObject;
        }
        tutoStep[0].SetActive(true);

        scoreObj = InGameManager.instance.score.GetComponent<Score>();
        playerStats = InGameManager.instance.player.GetComponent<CharactorStats>();

        var obj = ingameUI.transform.GetChild(1);
        scoreTxt = obj.GetComponentInChildren<Text>();

        var playerStatUI = GameObject.FindWithTag("PlayerStat").GetComponentsInChildren<Text>();
        playerTxtUI = new Text[playerStatUI.Length];
        // HP
        playerTxtUI[0] = playerStatUI[0];
        // ATK
        playerTxtUI[1] = playerStatUI[1];
    }

    public void ChangeState(InGameManager.InGameState state)
    {
        switch (state)
        {
            case InGameManager.InGameState.Tutorial:
                isTutorial = true;
                break;
            case InGameManager.InGameState.Start:
                Play();
                ingameUI.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case InGameManager.InGameState.Play:
                Play();
                break;
            case InGameManager.InGameState.Pause:
                ingameUI.SetActive(false);
                pauseUI.SetActive(true);
                break;
            case InGameManager.InGameState.Bonus:
                break;
            case InGameManager.InGameState.Clear:
                GameClearUI();
                break;
            case InGameManager.InGameState.GameOver:
                GameOverUI();
                break;
        }
    }
    void Update()
    {
        //scoreTxt.text = $"Score: {scoreObj.ScorePoint}";
        playerTxtUI[0].text = $"HP: {playerStats.CurrentHp}";
        playerTxtUI[1].text = $"ATK: {playerStats.TotalPower}";

        if (isTutorial)
        {
            Tutorial();
        }

    }

    public void Tutorial()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("��ġ��");
            tutoStepNum++;
            if (tutoStepNum > 2)
            {
                ingameUI.transform.GetChild(0).gameObject.SetActive(true);
                isTutorial = false;
                tutorialUI.SetActive(false);
                return;
            }
            tutoStep[tutoStepNum - 1].SetActive(false);
            tutoStep[tutoStepNum].SetActive(true);
        }
    }

    public void Play()
    {
        ingameUI.SetActive(true);
        pauseUI.SetActive(false);
        gameoverUI.SetActive(false);
        gameClearUI.SetActive(false);
    }

    public void PauseButton()
    {
        InGameManager.instance.GameState = InGameManager.InGameState.Pause;
    }

    public void ResumeButton()
    {
        InGameManager.instance.GameState = InGameManager.InGameState.Play;
    }

    public void ReStartButton()
    {
        InGameManager.instance.ReStart();
    }

    public void SelectStageButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void LobbyButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void GameOverUI()
    {
        ingameUI.SetActive(false);
        pauseUI.SetActive(false);
        gameoverUI.SetActive(true);
    }

    public void GameClearUI()
    {
        ingameUI.SetActive(false);
        gameClearUI.SetActive(true);
    }

}