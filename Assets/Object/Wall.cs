using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private MeshRenderer[] mesh = new MeshRenderer[3];

    private int weakHp;
    private int normalHp;

    void Awake()
    {
        weakHp = GameManager.Instance.mapStageInfo.WeakHp;
        normalHp = GameManager.Instance.mapStageInfo.NormalHp;

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

        //mesh[(int)weak].materials[0].color = Color.green;
        //for(int i=0; i<3; i++)
        //{
        //    if (i == (int)weak) continue;
        //    mesh[i].materials[0].color = new Color(51f / 255f, 85f / 255f, 1f, 1f);
        //}
        var wallTxtColor = new Text[3];

        wallTxtColor[0] = blockPoint[0].transform.GetComponentInChildren<Text>();
        wallTxtColor[1] = blockPoint[1].transform.GetComponentInChildren<Text>();
        wallTxtColor[2] = blockPoint[2].transform.GetComponentInChildren<Text>();

        mesh[(int)weak].materials[0].color = Color.white;
        wallTxtColor[(int)weak].color = Color.black;
        for (int i = 0; i < 3; i++)
        {
            if (i == (int)weak) continue;
            mesh[i].materials[0].color = Color.black;
            wallTxtColor[i].color = Color.white;
        }
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
            else
            {
                stats.WallHp = normalHp;
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
