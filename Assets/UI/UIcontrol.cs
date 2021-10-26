using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontrol : MonoBehaviour
{

    public GameObject ingameUI;
    public GameObject pauseUI;
    public GameObject gameoverUI;
    public GameObject gameClearUI;
    public GameObject tutorialUI;

    private Text scoreTxt;
    private Text[] playerTxt;
    private Score scoreObj;
    private CharactorStats playerStats;
    public GameObject[] tutoStep;
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

        scoreObj = GameObject.FindWithTag("Score").GetComponent<Score>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<CharactorStats>();

        var obj = ingameUI.transform.GetChild(1);
        scoreTxt = obj.GetComponentInChildren<Text>();

        var pTxt = GameObject.FindWithTag("PlayerStat").GetComponentsInChildren<Text>();
        playerTxt = new Text[pTxt.Length];
        // HP
        playerTxt[0] = pTxt[0];
        // ATK
        playerTxt[1] = pTxt[1];
    }

    void Update()
    {
        scoreTxt.text = $"Score: {scoreObj.ScorePoint}";

        playerTxt[0].text = $"HP: {playerStats.CurrentHp}";
        playerTxt[1].text = $"ATK: {playerStats.TotalPower}";

    }

    public void Play()
    {
        tutorialUI.SetActive(false);
        ingameUI.SetActive(true);
        pauseUI.SetActive(false);
        gameoverUI.SetActive(false);
        gameClearUI.SetActive(false);
    }

    public void PauseButton()
    {
        ingameUI.SetActive(false);
        pauseUI.SetActive(true);
        InGameManager.instance.Pause();
    }

    public void ResumeButton()
    {
        ingameUI.SetActive(true);
        pauseUI.SetActive(false);
        InGameManager.instance.Resume();
    }

    public void ReStartButton()
    {
        InGameManager.instance.ReStart();
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
