using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuaseUI : MonoBehaviour
{

    public Text score;
    public Text MaxCombo;

    private Score scoreObj;
    void Start()
    {
        scoreObj = InGameManager.instance.score.GetComponent<Score>();
    }

    void Update()
    {
    }
}
