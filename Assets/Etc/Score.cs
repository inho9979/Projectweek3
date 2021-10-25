using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int scorePoint;

    private int bonusCount;

    public int BonusCount
    {
        get => bonusCount;
        set
        {
            bonusCount = value;
        }
    }

    public int ScorePoint
    {
        get => scorePoint;
        set
        {
            scorePoint = value;
        }
    }

    void Start()
    {
        scorePoint = 0;
    }

    void Update()
    {
        
    }

    public void ScoreUp()
    {
        scorePoint += 10;
    }
}
