using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{

    public Text scoreText;
    public Text comboText;
    public Text comboCountText;
    public Text stageText;
    private Score scoreObj;
    private Color curColor;

    private Coroutine coRoutine;
    void Start()
    {
        scoreObj = InGameManager.instance.score.GetComponent<Score>();
        comboText.enabled = false;
        comboCountText.enabled = false;
        curColor = comboText.color;
        //scoreText = GetComponentInChildren<Text>();
        //comboText = transform.GetChild(4)
    }

    void Update()
    {
        scoreText.text = $"{scoreObj.ScorePoint}";
        comboCountText.text = $"X{scoreObj.CurCombo}";
        stageText.text = $"STAGE: {GameManager.Instance.mapStageInfo.StageLv}";
    }

    public void ComboHit()
    {
        if(scoreObj.CurCombo > 1)
        {
            Debug.Log(scoreObj.CurCombo);
            comboText.enabled = true;
            comboCountText.enabled = true;
            if(coRoutine != null)
            {
                StopCoroutine(coRoutine);
            }
            coRoutine = StartCoroutine(CoFade(1f));
        }
    }

    IEnumerator CoFade(float duration)
    {
        var timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            var clearColor = new Color(comboText.color.r, comboText.color.g, comboText.color.b, 0.2f);
            comboText.color = Color.Lerp(comboText.color, clearColor, Time.deltaTime * 2f);
            comboCountText.color = Color.Lerp(comboCountText.color, clearColor, Time.deltaTime * 2f);
            yield return null;
        }
        comboCountText.enabled = false;
        comboText.enabled = false;
        comboText.color = curColor;
        comboCountText.color = curColor;
    }
}
