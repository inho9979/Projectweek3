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
    public Material material;
    private CollisionPoints[] blockPoint;
    private WeakPoint weak;
    private WeakPoint normal;
    private MeshRenderer[] mesh = new MeshRenderer[3];

    private int weakHp;
    private int normalHp;

    void Awake()
    {
        weakHp = GameManager.Instance.mapStageInfo.WeakHp;
        normalHp = GameManager.Instance.mapStageInfo.NormalHp;

        blockPoint = new CollisionPoints[3];
        weak = (WeakPoint)Random.Range(0, 3);
        if (weak == WeakPoint.center)
            normal = (WeakPoint)(2 * Random.Range(0, 2));
        else
            normal = WeakPoint.center;

        for (int i = 0; i < 3; i++)
        {
            blockPoint[i] = transform.GetChild(i).GetComponent<CollisionPoints>();
            blockPoint[i].transform.tag = "WallPoint";
        }
    }

    // Awake타이밍에서는 자식오브젝트 생성이 안되잇기때문에 start()에서 호출
    private void Start()
    {
        SetStats();
    }

    public void setMesh()
    {
        Transform[] meshrender = new Transform[3];
        meshrender[0] = transform.Find("Original Mesh3");
        meshrender[1] = transform.Find("Original Mesh2");
        meshrender[2] = transform.Find("Original Mesh");

        for (int i = 0; i < meshrender.Length; i++)
        {
            mesh[i] = meshrender[i].GetComponent<MeshRenderer>();
        }

        mesh[(int)weak].materials[0].color = Color.green;
        mesh[(int)normal].materials[0].color = Color.blue;
    }

    public void DestroyMesh()
    {
        mesh[0].enabled = false;
        mesh[1].enabled = false;
        mesh[2].enabled = false;
    }

    public void SetStats()
    {
        for (int i = 0; i < 3; i++)
        {
            var stats = blockPoint[i].GetComponent<WallStats>();
            if (i == (int)weak)
            {
                stats.WallHp = weakHp;
            }
            else if (i == (int)normal)
            {
                stats.WallHp = normalHp;
            }
            else
            {
                stats.WallHp = (int)(normalHp * 2.5);
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
