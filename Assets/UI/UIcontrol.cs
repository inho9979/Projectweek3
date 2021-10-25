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

    private Text scoreTxt;
    private Score scoreObj;
    private CharactorStats playerStats;
    void Awake()
    {
        ingameUI.SetActive(true);
        pauseUI.SetActive(false);
        gameoverUI.SetActive(false);
        gameClearUI.SetActive(false);

        scoreObj = GameObject.FindWithTag("Score").GetComponent<Score>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<CharactorStats>();

        var obj = ingameUI.transform.GetChild(0);
        scoreTxt = obj.GetComponentInChildren<Text>();
    }

    void Update()
    {
        scoreTxt.text = $"Score :{scoreObj.ScorePoint} \nHp :{playerStats.CurrentHp}\nATK :{playerStats.TotalPower}";
        Debug.Log($"플레이어 체력 : {playerStats.CurrentHp}");
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
