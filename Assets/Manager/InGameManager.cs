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
    private Touch touchFuc;

    private int tutoStep = 0;
    private float startPosZ;

    public enum InGameState
    {
        Tutorial,
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
                    break;
                case InGameState.Play:
                    uiCtrlComponent.Play();
                    objGenerateComponent.WallGenerate(startPosZ, 20, 25, 6);
                    objGenerateComponent.ItemGenerate(3);
                    Resume();
                    break;
                case InGameState.Pause:
                    Pause();
                    break;
                case InGameState.Bonus:
                    Bonus();
                    break;
                case InGameState.Clear:
                    GameClear();
                    break;
                case InGameState.GameOver:
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
        uiCtrlComponent = ui.GetComponent<UIcontrol>();
        touchFuc = GameObject.FindWithTag("Touch").GetComponent<Touch>();
        GameState = InGameState.Tutorial;
        startPosZ = player.transform.position.z;
    }

    void Update()
    {
        BonusStateCheck();
        if(gameState == InGameState.Tutorial)
            Tutorial();
    }

    public void Tutorial()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("��ġ��");
            tutoStep++;
            if(tutoStep > 2)
            {
                GameState = InGameState.Play;
                GameObject.FindWithTag("TutoObject").SetActive(false);
                uiCtrlComponent.ingameUI.transform.GetChild(0).gameObject.SetActive(true);
                return;
            }
            uiCtrlComponent.tutoStep[tutoStep-1].SetActive(false);
            uiCtrlComponent.tutoStep[tutoStep].SetActive(true);
        }
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    // ��������.. ���� ����
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    // ��������.. ���� ����
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    // Ŭ����ɽ� �ʿ��� ��� ���۵� ����
    public void Bonus()
    {
        playerCtrlComponent.StageClear();
        objGenerateComponent.BonusGenerate();
        scoreComponent.BonusCount = 10;/*(scoreComponent.ScorePoint) / 10;*/
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
        uiCtrlComponent.GameClearUI();
    }

    public void BonusStateCheck()
    {
        if (scoreComponent.BonusCount <= 0)
            playerCtrlComponent.IsBonus = false;
    }
}
