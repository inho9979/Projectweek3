using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageScrollUI : MonoBehaviour
{

    private bool scrollViewOn = true;
    private Vector2 curScrollPos;
    private Coroutine coRoutine;
    private float height = 1050f;

    private ScrollRect scrollRect;
    public float space = 10f;
    public GameObject buttonPrefab;
    public List<RectTransform> buttonPrefabs = new List<RectTransform>();

    void Start()
    {
        curScrollPos = new Vector2(GetComponent<RectTransform>().sizeDelta.x, height);
        scrollRect = GetComponent<ScrollRect>();


        var limitStage = GameManager.Instance.mapStageInfo.LimitStageLv;
        for (int i = 0; i < limitStage; i++)
        {
            AddStageBtn(i + 1);
        }
    }

    public void AddStageBtn(int stageNum)
    {
        var newBtn = Instantiate(buttonPrefab, scrollRect.content).GetComponent<RectTransform>();
        newBtn.GetComponentInChildren<Text>().text = $"STAGE {stageNum.ToString("D3")}";
        newBtn.GetComponent<StageBtnNum>().stageNum = stageNum;
        buttonPrefabs.Add(newBtn);

        float y = 0f;
        for (int i = 0; i < buttonPrefabs.Count; i++)
        {
            buttonPrefabs[i].anchoredPosition = new Vector2(0f, -y);
            y += buttonPrefabs[i].sizeDelta.y + space;
        }
    }

    public void ToggleScrollView()
    {
        scrollViewOn = !scrollViewOn;

        if(coRoutine != null)
        {
            StopCoroutine(coRoutine);
        }
        coRoutine = StartCoroutine(CoScrollOn(scrollViewOn));
    }


    public IEnumerator CoScrollOn(bool On)
    {
        var rectTr = GetComponent<RectTransform>();
        var duration = 2f;
        var timer = 0f;
        if (On)
        {
            while (timer < duration)
            {
                timer += Time.deltaTime;
                rectTr.sizeDelta = Vector2.Lerp(rectTr.sizeDelta, new Vector2(rectTr.sizeDelta.x, 0f), timer / duration);
                yield return null;
            }
        }
        else
        {
            while (timer < duration)
            {
                timer += Time.deltaTime;
                rectTr.sizeDelta = Vector2.Lerp(rectTr.sizeDelta, curScrollPos, timer / duration);
                yield return null;
            }
        }
    }
}

//if(scrollViewOn)
//{
//    var rectTr = GetComponent<RectTransform>();
//    var timer = 0f;
//    while (timer < 2f)
//    {
//        timer += Time.deltaTime;
//        rectTr.sizeDelta = Vector2.Lerp(rectTr.sizeDelta, new Vector2(rectTr.anchoredPosition.x, 0f), timer / 2f);
//    }
//}
//else
//{
//    var rectTr = GetComponent<RectTransform>();
//    var timer = 0f;
//    while (timer < 2f)
//    {
//        timer += Time.deltaTime;
//        rectTr.sizeDelta = Vector2.Lerp(rectTr.anchoredPosition, curScrollPos, timer / 2f);
//    }
//}