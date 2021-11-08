using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, IStateChangeable
{
    public GameObject wallPrefab;
    public GameObject bonusWallPrefab;
    public GameObject[] bonusStage;
    public GameObject tutoWallPrefab;
    public GameObject[] itemPrefab;
    public ParticleSystem powerAura;
    public ParticleSystem healAura;

    public GameObject backGround;
    public GameObject floorObj;
    public GameObject endTrigger;
    public GameObject virCam;

    private GameObject tutoWall;
    public List<GameObject> wallList = new List<GameObject>();
    public List<GameObject> bonusWallList = new List<GameObject>();

    private float startPosZ;
    private float timer = 0f;

    private int wallDistance;

    private int wallCount;
    private int[] ATKItemCount = new int[2];
    private int[] HealItemCount = new int[2];
    private int clearGold;

    private void Awake()
    {
    }

    void Start()
    {
        startPosZ = InGameManager.instance.player.transform.position.z;

        wallCount = GameManager.Instance.mapStageInfo.WallCount;

        ATKItemCount[0] = GameManager.Instance.mapStageInfo.atkPackCount[0];
        ATKItemCount[1] = GameManager.Instance.mapStageInfo.atkPackCount[1];
        HealItemCount[0] = GameManager.Instance.mapStageInfo.healPackCount[0];
        HealItemCount[1] = GameManager.Instance.mapStageInfo.healPackCount[1];
        clearGold = GameManager.Instance.mapStageInfo.ClearGold;

        wallDistance = 20;

        virCam = GameObject.FindWithTag("VirCam1");
        StageInit();
    }

    public void StageInit()
    {
        var backGDistance = 55f;

        var floor = Instantiate(floorObj);
        var background = Instantiate(backGround);
        var trigger = Instantiate(endTrigger);

        var bgPos = background.transform.position;

        if (wallCount == 10)
        {
            for (int i = 0; i < 3; i++)
            {
                var createPos = new Vector3(bgPos.x, bgPos.y, bgPos.z + backGDistance * (i + 1));
                Instantiate(background, createPos, Quaternion.identity);
            }
        }
        else if (wallCount == 15)
        {
            for (int i = 0; i < 5; i++)
            {
                var createPos = new Vector3(bgPos.x, bgPos.y, bgPos.z + backGDistance * (i + 1));
                Instantiate(background, createPos, Quaternion.identity);
            }

            floor.transform.localScale = new Vector3(floor.transform.localScale.x, floor.transform.localScale.y, floor.transform.localScale.z + 15);
            trigger.transform.position = new Vector3(trigger.transform.position.x, trigger.transform.position.y, trigger.transform.position.z + 76);
            virCam.transform.position = new Vector3(virCam.transform.position.x, virCam.transform.position.y, virCam.transform.position.z + 76);
        }
        else if (wallCount == 20)
        {
            for (int i = 0; i < 7; i++)
            {
                var createPos = new Vector3(bgPos.x, bgPos.y, bgPos.z + backGDistance * (i + 1));
                Instantiate(background, createPos, Quaternion.identity);
            }

            floor.transform.localScale = new Vector3(floor.transform.localScale.x, floor.transform.localScale.y, floor.transform.localScale.z + 35);
            trigger.transform.position = new Vector3(trigger.transform.position.x, trigger.transform.position.y, trigger.transform.position.z + 201);
            virCam.transform.position = new Vector3(virCam.transform.position.x, virCam.transform.position.y, virCam.transform.position.z + 201);
        }
        else if (wallCount == 25)
        {
            for (int i = 0; i < 9; i++)
            {
                var createPos = new Vector3(bgPos.x, bgPos.y, bgPos.z + backGDistance * (i + 1));
                Instantiate(background, createPos, Quaternion.identity);
            }

            floor.transform.localScale = new Vector3(floor.transform.localScale.x, floor.transform.localScale.y, floor.transform.localScale.z + 55);
            trigger.transform.position = new Vector3(trigger.transform.position.x, trigger.transform.position.y, trigger.transform.position.z + 316);
            virCam.transform.position = new Vector3(virCam.transform.position.x, virCam.transform.position.y, virCam.transform.position.z + 316);
        }
    }


    public void ChangeState(InGameManager.InGameState state)
    {
        switch (state)
        {
            case InGameManager.InGameState.Tutorial:
                tutoWall = Instantiate(tutoWallPrefab);
                break;
            case InGameManager.InGameState.Start:
                Destroy(tutoWall);
                GameObject.FindWithTag("TutoObject").SetActive(false);
                WallGenerate(startPosZ, wallDistance, wallCount);
                ItemGenerate(ATKItemCount, HealItemCount);
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
    public void WallGenerate(float startPosZ, int wallDistance, int Count)
    {
        var distanceSum = 0f;
        var wallPos = wallPrefab.transform.position;
        for (int i = 0; i < Count; i++)
        {
            distanceSum += wallDistance;
            var locate = startPosZ + distanceSum;
            var tempWall = Instantiate(wallPrefab, new Vector3(wallPos.x, wallPos.y, locate), Quaternion.identity);
            wallList.Add(tempWall);
        }
    }


    public void ItemGenerate(int[] powerItem, int[] healItem)
    {
        List<Vector3> posList = new List<Vector3>();

        var powerItemCount = Random.Range(powerItem[0], powerItem[1] + 1);
        var healItemCount = Random.Range(healItem[0], healItem[1] + 1);

        while(powerItemCount > 0)
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
                    // 파워아이템
                    var tempItem = Instantiate(itemPrefab[0], itemPos2, Quaternion.identity);
                    var tempAura = Instantiate(powerAura, tempItem.transform.position, Quaternion.identity);
                    tempAura.transform.SetParent(tempItem.transform, true);
                    tempAura.Play();
                    var itemStat = tempItem.GetComponent<ItemAbility>();
                    itemStat.SetAbility(4, false);

                    powerItemCount--;
                    posList.Add(itemPos2);
                }
                if (powerItemCount < 1)
                    break;
            }
        }
        while (healItemCount > 0)
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
                    // 힐 아이템
                    var tempItem = Instantiate(itemPrefab[1], itemPos2, Quaternion.Euler(0f, 180f, 0f));
                    var tempAura = Instantiate(healAura, tempItem.transform);
                    tempAura.Play();
                    var itemStat = tempItem.GetComponent<ItemAbility>();
                    itemStat.SetAbility(0, true);

                    healItemCount--;
                    posList.Add(itemPos2);
                }
                if (healItemCount < 1)
                    break;
            }
        }
        int bigItemCount = 1;
        while(bigItemCount > 0)
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
                    // 빅 아이템
                    var tempItem = Instantiate(itemPrefab[2], itemPos2, Quaternion.identity);
                    //var tempAura = Instantiate(healAura, tempItem.transform);
                    //tempAura.Play();
                    bigItemCount--;
                    posList.Add(itemPos2);
                }

                if (bigItemCount < 1)
                    break;
            }
        }
    }

    public void BonusGenerate()
    {
        if (wallCount == 10)
        {
            var stage = Instantiate(bonusStage[0], bonusStage[0].transform.position, Quaternion.identity);
            var mesh = stage.transform.GetChild(0).GetComponent<MeshRenderer>();
            mesh.material.color = new Color(1f, 0.43f, 0.05f, 1);

            var bonusWalls = stage.GetComponentsInChildren<BonusWall>();
            for (int i = 0; i < bonusWalls.Length; i++)
            {
                bonusWalls[i].SetPoint(i + 1);
                bonusWallList.Add(bonusWalls[i].gameObject);
                var wallMesh = bonusWalls[i].transform.GetChild(4).GetComponent<MeshRenderer>();
                switch (i % 4)
                {
                    case 0:
                        wallMesh.material.color = new Color(1f, (50f / 255f), 0f, 1f);
                        break;
                    case 1:
                        wallMesh.material.color = Color.yellow;
                        break;
                    case 2:
                        wallMesh.material.color = Color.green;
                        break;
                    case 3:
                        wallMesh.material.color = Color.blue;
                        break;
                }
            }
        }
        else if(wallCount == 15)
        {
            var stage = Instantiate(bonusStage[1], bonusStage[1].transform.position, Quaternion.identity);
            var mesh = stage.transform.GetChild(0).GetComponent<MeshRenderer>();
            mesh.material.color = new Color(1f, 0.43f, 0.05f, 1);

            var bonusWalls = stage.GetComponentsInChildren<BonusWall>();
            for (int i = 0; i < bonusWalls.Length; i++)
            {
                bonusWalls[i].SetPoint(i + 1);
                bonusWallList.Add(bonusWalls[i].gameObject);
                var wallMesh = bonusWalls[i].transform.GetChild(4).GetComponent<MeshRenderer>();
                switch (i % 4)
                {
                    case 0:
                        wallMesh.material.color = new Color(1f, (50f / 255f), 0f, 1f);
                        break;
                    case 1:
                        wallMesh.material.color = Color.yellow;
                        break;
                    case 2:
                        wallMesh.material.color = Color.green;
                        break;
                    case 3:
                        wallMesh.material.color = Color.blue;
                        break;
                }
            }
        }
        else if (wallCount == 20)
        {
            var stage = Instantiate(bonusStage[2], bonusStage[2].transform.position, Quaternion.identity);
            var mesh = stage.transform.GetChild(0).GetComponent<MeshRenderer>();
            mesh.material.color = new Color(1f, 0.43f, 0.05f, 1);

            var bonusWalls = stage.GetComponentsInChildren<BonusWall>();
            for (int i = 0; i < bonusWalls.Length; i++)
            {
                bonusWalls[i].SetPoint(i + 1);
                bonusWallList.Add(bonusWalls[i].gameObject);
                var wallMesh = bonusWalls[i].transform.GetChild(4).GetComponent<MeshRenderer>();
                switch (i % 4)
                {
                    case 0:
                        wallMesh.material.color = new Color(1f, (50f / 255f), 0f, 1f);
                        break;
                    case 1:
                        wallMesh.material.color = Color.yellow;
                        break;
                    case 2:
                        wallMesh.material.color = Color.green;
                        break;
                    case 3:
                        wallMesh.material.color = Color.blue;
                        break;
                }
            }
        }
        else if (wallCount == 25)
        {
            var stage = Instantiate(bonusStage[3], bonusStage[3].transform.position, Quaternion.identity);
            var mesh = stage.transform.GetChild(0).GetComponent<MeshRenderer>();
            mesh.material.color = new Color(1f, 0.43f, 0.05f, 1);

            var bonusWalls = stage.GetComponentsInChildren<BonusWall>();
            for (int i = 0; i < bonusWalls.Length; i++)
            {
                bonusWalls[i].SetPoint(i + 1);
                bonusWallList.Add(bonusWalls[i].gameObject);
                var wallMesh = bonusWalls[i].transform.GetChild(4).GetComponent<MeshRenderer>();
                switch (i % 4)
                {
                    case 0:
                        wallMesh.material.color = new Color(1f, (50f / 255f), 0f, 1f);
                        break;
                    case 1:
                        wallMesh.material.color = Color.yellow;
                        break;
                    case 2:
                        wallMesh.material.color = Color.green;
                        break;
                    case 3:
                        wallMesh.material.color = Color.blue;
                        break;
                }
            }
        }
        //BonusWallGenerate(stage.transform.position.z, 10);
        var scoreObj = InGameManager.instance.score.GetComponent<Score>();
        scoreObj.BonusCount = scoreObj.MaxCombo;
    }

    public IEnumerator wallDestroy(GameObject wall)
    {
        yield return new WaitForSeconds(1f);
        Destroy(wall);
    }
    public IEnumerator BonusWallDestroy(GameObject wall)
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(wall);
    }
    void Update()
    {

    }
}


//case 4: wallMesh.material.color = new Color(1f, (50f / 255f), 0f, 1f);
//    break;
//case 5: wallMesh.material.color = Color.yellow;
//    break;
//case 6: wallMesh.material.color = Color.green;
//    break;
//case 7: wallMesh.material.color = Color.blue;
//    break;
//case 8: wallMesh.material.color = new Color(1f, (50f / 255f), 0f, 1f);
//    break;
//case 9: wallMesh.material.color = Color.yellow;
//break;


//public void BonusWallGenerate(float startPosZ, int Count)
//{
//    var distanceSum = 0f;
//    var walldistance = 8f;
//    var wallPos = bonusWallPrefab.transform.position;
//    for (int i = 0; i < Count; i++)
//    {
//        distanceSum += walldistance;
//        var locate = startPosZ + distanceSum;

//        var tempWall = Instantiate(bonusWallPrefab, new Vector3(wallPos.x, wallPos.y, locate), Quaternion.identity);
//        var wallPoint = tempWall.GetComponent<BonusWall>();
//        wallPoint.SetPoint((i+1) * 100);
//        bonusWallList.Add(tempWall);
//    }
//}

//while (count > 0)
//{
//    foreach (var obj in wallList)
//    {
//        var randomPos = Random.Range(-1, 2);
//        var itemPos2 = new Vector3(randomPos * 3f, 1f, obj.transform.position.z - 13);
//        if (posList.Contains(itemPos2))
//        {
//            continue;
//        }
//        var perCent = Random.Range(0, 100);
//        if (perCent < 10)
//        {
//            if (perCent > 4)
//            {
//                // 파워아이템
//                var tempItem = Instantiate(itemPrefab[0], itemPos2, Quaternion.identity);
//                var tempAura = Instantiate(powerAura, tempItem.transform.position, Quaternion.identity);
//                tempAura.transform.SetParent(tempItem.transform, true);
//                tempAura.Play();
//                var itemStat = tempItem.GetComponent<ItemAbility>();
//                itemStat.SetAbility(4, false);
//            }
//            else
//            {
//                // 힐 아이템
//                var tempItem = Instantiate(itemPrefab[1], itemPos2, Quaternion.Euler(0f,180f,0f));
//                var tempAura = Instantiate(healAura, tempItem.transform);
//                tempAura.Play();
//                var itemStat = tempItem.GetComponent<ItemAbility>();
//                itemStat.SetAbility(0, true);
//            }
//            count--;
//            posList.Add(itemPos2);
//        }
//    }
//}