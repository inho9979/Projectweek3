using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScrollUI : MonoBehaviour
{

    private bool scrollViewOn = false;
    private Vector2 curScrollPos;
    private Coroutine coRoutine; 
    void Start()
    {
        curScrollPos = GetComponent<RectTransform>().sizeDelta;
    }

    void Update()
    {
        
    }

    public void ToggleScrollView()
    {
        scrollViewOn = !scrollViewOn;

        if(coRoutine != null)
        {
            StopCoroutine(coRoutine);
        }
        coRoutine = StartCoroutine(CoScrollOn(scrollViewOn));
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
