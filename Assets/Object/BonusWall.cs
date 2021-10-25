using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusWall : MonoBehaviour
{
    private int point;
    public int Point
    {
        get => point;
        set
        {
            point = value;
        }
    }

    private Text text;
    void Start()
    {
    }

    public void SetPoint(int pointt)
    {
        Point = pointt;
        var rectTr = transform.GetChild(0).GetComponent<RectTransform>();
        text = rectTr.GetComponentInChildren<Text>();
        text.text = $"{point}";
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
