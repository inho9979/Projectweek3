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

    private MeshRenderer[] mesh = new MeshRenderer[3];

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
    }

    public void DestroyMesh()
    {
        mesh[0].enabled = false;
        mesh[1].enabled = false;
        mesh[2].enabled = false;
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
