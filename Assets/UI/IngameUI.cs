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
    private Color clearColor;

    private Coroutine coRoutine;
    void Start()
    {
        scoreObj = InGameManager.instance.score.GetComponent<Score>();
        comboText.enabled = false;
        comboCountText.enabled = false;
        curColor = new Color(1f, 217f / 255f, 0f, 1f);
        clearColor = new Color(1f, 217f / 255f, 0f, 0f);
        comboText.color = clearColor;
        comboCountText.color = clearColor;
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
            timer += Time.unscaledDeltaTime;
            
            //comboText.color = Color.Lerp(comboText.color, clearColor, Time.deltaTime * 2f);
            //comboCountText.color = Color.Lerp(comboCountText.color, clearColor, Time.deltaTime * 2f);
            comboText.color = Color.Lerp(comboText.color, curColor, Time.unscaledDeltaTime * 3f);
            comboCountText.color = Color.Lerp(comboCountText.color, curColor, Time.unscaledDeltaTime * 3f);
            yield return null;
        }
        comboCountText.enabled = false;
        comboText.enabled = false;
        comboText.color = clearColor;
        comboCountText.color = clearColor;
    }
}
