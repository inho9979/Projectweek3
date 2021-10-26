using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionPoints : MonoBehaviour
{
    private WallStats objStat;
    private Text text;

    void Awake()
    {
        objStat = GetComponent<WallStats>();
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        setText();
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void setText()
    {
        //var textObj = Instantiate(text, transform);
        //var textMesh = textObj.GetComponent<TextMesh>();
        text.text = objStat.WallHp.ToString();
    }
}
