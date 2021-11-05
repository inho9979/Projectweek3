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
    private RectTransform statUI;
    private GameObject playerPOS;

    private Score scoreObj;
    private CharactorStats playerStats;
    public GameObject[] tutoStep;

    private int tutoStepNum = 0;
    private bool isTutorial = false;

    public AudioClip startClearBtn;
    public AudioClip etcButton;
    public AudioClip clearSound;

    private InGameManager.InGameState curState;
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
        statUI = GameObject.FindWithTag("PlayerStat").GetComponent<RectTransform>();
        playerPOS = GameObject.FindWithTag("Respawn");
        playerTxtUI = new Text[playerStatUI.Length];
        // HP
        playerTxtUI[0] = playerStatUI[0];
        // ATK
        playerTxtUI[1] = playerStatUI[1];
    }

    public void ChangeState(InGameManager.InGameState state)
    {
        curState = state;
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
                playerTxtUI[0].enabled = false;
                playerTxtUI[1].enabled = false;
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

        playerTxtUI[0].text = $"HP: {playerStats.CurrentHp}";
        playerTxtUI[1].text = $"ATK: {playerStats.TotalPower}";
        statUI.anchoredPosition = new Vector3(statUI.position.x, 10f);
        if (isTutorial)
        {
            Tutorial();
        }

    }

    public void Tutorial()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ÅÍÄ¡ÇÔ");
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

    public void StartButton()
    {
        SoundManager.Instance.SFXPlay("StartBtn", startClearBtn);
        InGameManager.instance.GameState = InGameManager.InGameState.Start;
    }

    public void PauseButton()
    {
        ingameUI.GetComponent<IngameUI>().comboCountText.enabled = false;
        ingameUI.GetComponent<IngameUI>().comboText.enabled = false;
        SoundManager.Instance.SFXPlay("PauseBtn", etcButton);
        InGameManager.instance.GameState = InGameManager.InGameState.Pause;
    }

    public void ResumeButton()
    {
        SoundManager.Instance.SFXPlay("PauseBtn", etcButton);
        InGameManager.instance.GameState = InGameManager.InGameState.Play;
    }

    public void ReStartButton()
    {
        SoundManager.Instance.SFXPlay("PauseBtn", etcButton);
        InGameManager.instance.ReStart();

    }

    public void SelectStageButton()
    {
        SoundManager.Instance.SFXPlay("ClearBtn", startClearBtn);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LobbyButton()
    {
        SoundManager.Instance.SFXPlay("ClearBtn", startClearBtn);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void NextStageButton()
    {
        SoundManager.Instance.SFXPlay("ClearBtn", startClearBtn);
        if (GameManager.Instance.mapStageInfo.LimitStageLv > GameManager.Instance.mapStageInfo.StageLv)
        {
            GameManager.Instance.mapStageInfo.StageLv++;
            GameManager.Instance.mapStageInfo.SetStageLv(GameManager.Instance.mapStageInfo.StageLv);
        }
        SceneManager.LoadScene(2);
    }

    public void GameOverUI()
    {
        ingameUI.SetActive(false);
        pauseUI.SetActive(false);

        gameoverUI.SetActive(true);
    }

    public void GameClearUI()
    {
        SoundManager.Instance.SFXPlay("Clear", clearSound);
        ingameUI.SetActive(false);
        gameClearUI.SetActive(true);
    }


}