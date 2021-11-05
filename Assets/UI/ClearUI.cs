using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    public Text coinText;
    public Text score;
    public Text maxComboText;

    private Score scoreObj;
    void Start()
    {
        scoreObj = InGameManager.instance.score.GetComponent<Score>();
    }

    void Update()
    {
        coinText.text = $"{scoreObj.Coin}";
        score.text = $"Score: {scoreObj.ScorePoint}";
        maxComboText.text = $"Combo Bonus: x{scoreObj.MaxCombo}";
    }
}
