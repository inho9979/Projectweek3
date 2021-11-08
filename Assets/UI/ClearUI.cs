using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    public Text coinText;
    public Text score;
    public Text maxComboText;
    public Text rankText;

    private Score scoreObj;
    void Start()
    {
        scoreObj = InGameManager.instance.score.GetComponent<Score>();
    }

    void Update()
    {
        coinText.text = $"{scoreObj.Coin}";
        score.text = $"Score: {scoreObj.ScorePoint}";
        maxComboText.text = $"Best Combo: x{scoreObj.MaxCombo}";
        if(scoreObj.MaxCombo > 20)
        {
            rankText.text = "PerFect!";
            rankText.color = new Color(0f, 176 / 255f, 240 / 255f, 1f);
        }
        else if(scoreObj.MaxCombo > 15)
        {
            rankText.text = "Master";
            rankText.color = new Color(112 / 255f, 48 / 255f, 160 / 255f, 1f);
        }
        else if(scoreObj.MaxCombo > 10)
        {
            rankText.text = "Expert";
            rankText.color = new Color(189 / 255f, 215 / 255f, 238 / 255f, 1f);
        }
        else if(scoreObj.MaxCombo > 5)
        {
            rankText.text = "Beginner";
            rankText.color = new Color(194 / 255f, 144 / 255f, 44 / 255f, 1f);
        }
        else
        {
            rankText.text = "Not Bad";
            rankText.color = new Color(229 / 255f, 131 / 255f, 229 / 255f, 1f);
        }
    }
}
