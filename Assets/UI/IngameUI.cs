using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{

    public Text scoreText;
    public Text comboText;
    public Text comboCountText;
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
            coRoutine = StartCoroutine(CoFade(2f));
        }
    }

    IEnumerator CoFade(float duration)
    {
        var timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            var clearColor = new Color(comboText.color.r, comboText.color.g, comboText.color.b, 0f);
            comboText.color = Color.Lerp(comboText.color, clearColor, timer / duration);
            comboCountText.color = Color.Lerp(comboCountText.color, clearColor, timer / duration);
            yield return null;
        }
        comboCountText.enabled = false;
        comboText.enabled = false;
        comboText.color = curColor;
        comboCountText.color = curColor;
    }
}
