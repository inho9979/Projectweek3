using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum WeakPoint
    {
        right,
        center,
        left
    }

    private CollisionPoints[] blockPoint;
    private WeakPoint weak;

    void Awake()
    {
        blockPoint = new CollisionPoints[3];
        weak = (WeakPoint)Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            blockPoint[i] = transform.GetChild(i).GetComponent<CollisionPoints>();
            blockPoint[i].transform.tag = "WallPoint";
        }
    }

    // Awake타이밍에서는 자식오브젝트 생성이 안되잇기때문에 start()에서 호출
    private void Start()
    {
        SetStats(30, 1);
    }

    public void SetStats(int maxHp, int minHp)
    {
        for (int i = 0; i < 3; i++)
        {
            var stats = blockPoint[i].GetComponent<WallStats>();
            if (i != (int)weak)
            {
                stats.WallHp = maxHp;
            }
            else
            {
                stats.WallHp = minHp;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
        }
    }
    void Update()
    {

    }

}
