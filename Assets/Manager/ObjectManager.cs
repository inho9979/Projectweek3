using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, IStateChangeable
{
    public GameObject wallPrefab;
    public GameObject bonusWallPrefab;
    public GameObject bonusStage;
    public GameObject[] itemPrefab;
    public ParticleSystem powerAura;
    public ParticleSystem healAura;

    private List<GameObject> wallList = new List<GameObject>();
    private List<GameObject> bonusWallList = new List<GameObject>();

    public int startItemCount = 3;
    public int startWallCount = 6;
    private float startPosZ;

    private float timer = 0f;
    private void Awake()
    {
    }

    void Start()
    {
        startPosZ = InGameManager.instance.player.transform.position.z;
    }


    public void ChangeState(InGameManager.InGameState state)
    {
        switch (state)
        {
            case InGameManager.InGameState.Tutorial:
                GameObject.FindWithTag("TutoObject").SetActive(true);
                break;
            case InGameManager.InGameState.Start:
                GameObject.FindWithTag("TutoObject").SetActive(false);
                WallGenerate(startPosZ, 20, 25, startWallCount);
                ItemGenerate(startItemCount);
                foreach(var obj in wallList)
                {
                    obj.GetComponent<Wall>().setMesh();
                }
                break;
            case InGameManager.InGameState.Pause:
                break;
            case InGameManager.InGameState.Bonus:
                BonusGenerate();
                break;
            case InGameManager.InGameState.Clear:
                break;
            case InGameManager.InGameState.GameOver:
                break;
        }
    }
        public void WallGenerate(float startPosZ, int startRange, int endRange, int Count)
    {
        var distanceSum = 0f;
        var walldistance = 0f;
        var wallPos = wallPrefab.transform.position;
        for (int i = 0; i < Count; i++)
        {
            walldistance = Random.Range(startRange, endRange);
            distanceSum += walldistance;
            var locate = startPosZ + distanceSum;
            var tempWall = Instantiate(wallPrefab, new Vector3(wallPos.x, wallPos.y, locate), Quaternion.identity);
            wallList.Add(tempWall);
        }
    }

    public void BonusWallGenerate(float startPosZ, int Count)
    {
        var distanceSum = 0f;
        var walldistance = 8f;
        var wallPos = bonusWallPrefab.transform.position;
        for (int i = 0; i < Count; i++)
        {
            distanceSum += walldistance;
            var locate = startPosZ + distanceSum;

            var tempWall = Instantiate(bonusWallPrefab, new Vector3(wallPos.x, wallPos.y, locate), Quaternion.identity);
            var wallPoint = tempWall.GetComponent<BonusWall>();
            wallPoint.SetPoint((i+1) * 100);
            bonusWallList.Add(tempWall);
        }
    }

    public void ItemGenerate(int ItemCount)
    {
        var count = startItemCount;
        List<Vector3> posList = new List<Vector3>();
        while (count > 0)
        {
            foreach (var obj in wallList)
            {
                var randomPos = Random.Range(-1, 2);
                var itemPos2 = new Vector3(randomPos * 3f, 1f, obj.transform.position.z - 13);
                if (posList.Contains(itemPos2))
                {
                    continue;
                }
                var perCent = Random.Range(0, 100);
                if (perCent < 10)
                {
                    if (perCent > 4)
                    {
                        // 파워아이템
                        var tempItem = Instantiate(itemPrefab[0], itemPos2, Quaternion.identity);
                        var tempAura = Instantiate(powerAura, tempItem.transform.position, Quaternion.identity);
                        tempAura.transform.SetParent(tempItem.transform, true);
                        tempAura.Play();
                        var itemStat = tempItem.GetComponent<ItemAbility>();
                        itemStat.SetAbility(4, false);
                    }
                    else
                    {
                        // 힐 아이템
                        var tempItem = Instantiate(itemPrefab[1], itemPos2, Quaternion.Euler(0f,180f,0f));
                        var tempAura = Instantiate(healAura, tempItem.transform);
                        tempAura.Play();
                        var itemStat = tempItem.GetComponent<ItemAbility>();
                        itemStat.SetAbility(0, true);
                    }
                    count--;
                    posList.Add(itemPos2);
                }
            }
        }
    }

    public void BonusGenerate()
    {
        var stage = Instantiate(bonusStage, bonusStage.transform.position, Quaternion.identity);
        
        var mesh = stage.transform.GetChild(0).GetComponent<MeshRenderer>();
        mesh.material.color = new Color(1f, 0.43f, 0.05f, 1);

        BonusWallGenerate(stage.transform.position.z, 10);
        var bonusCount = InGameManager.instance.score.GetComponent<Score>();
        bonusCount.BonusCount = 10;
    }

    public IEnumerator wallDestroy(GameObject wall)
    {
        yield return new WaitForSeconds(1f);
        Destroy(wall);
    }
    void Update()
    {

    }
}