using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtnNum : MonoBehaviour
{
    private Text stageText;
    public int stageNum = 1;
    void Start()
    {
        stageText = GameObject.FindWithTag("StageNum").GetComponent<Text>();
    }

    void Update()
    {
    }

    public void SetStage()
    {
        stageText.text = $"{stageNum}";
        Debug.Log(stageText.name);
        Debug.Log(stageNum);
    }
}
