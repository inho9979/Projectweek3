using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    public Text coinText;
    public Text baseGold;
    public Text maxComboText;

    private Score scoreObj;
    void Start()
    {
        scoreObj = InGameManager.instance.score.GetComponent<Score>();
    }

    void Update()
    {
        coinText.text = $"{scoreObj.Coin}";
        baseGold.text = $"BaseGold: {GameManager.Instance.mapStageInfo.ClearGold}";
        maxComboText.text = $"Combo Bonus: x{scoreObj.MaxCombo}";
    }
}
