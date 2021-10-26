using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private CharactorStats playerStat;
    private Score getScore;
    private Animator playerAni;
    private PlayerControl playerCtrl;

    void Start()
    {
        playerStat = gameObject.GetComponent<CharactorStats>();
        playerAni = gameObject.GetComponent<Animator>();
        playerCtrl = gameObject.GetComponent<PlayerControl>();
        getScore = InGameManager.instance.score.GetComponent<Score>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag is "ClearFlag")
        {
            Debug.Log("�浹!");
            InGameManager.instance.GameState = InGameManager.InGameState.Bonus;
        }
        
        if(other.tag is "WallPoint")
        {
            var objStat = other.transform.GetComponent<WallStats>();
            if (objStat != null)
            {
                if (objStat.WallHp <= playerStat.TotalPower)
                {
                    getScore.ScoreUp();
                    var frags = Physics.OverlapSphere(transform.position, 10f);
                    foreach (var obj in frags)
                    {
                        obj.gameObject.SendMessage("Damage", 1000f, SendMessageOptions.DontRequireReceiver);
                    }
                    //Destroy(other.transform.parent.gameObject);
                    var objMgr = InGameManager.instance.objectManager.GetComponent<ObjectManager>();
                    StartCoroutine(objMgr.wallDestroy(other.transform.parent.gameObject));
                }
                else
                {
                    var damage = objStat.WallHp - playerStat.TotalPower;
                    playerStat.CurrentHp -= damage;
                    Debug.Log($"{damage} , {playerStat.CurrentHp}");

                    if (playerStat.CurrentHp <= 0)
                    {
                        InGameManager.instance.GameState = InGameManager.InGameState.GameOver;
                        playerCtrl.State = PlayerControl.MoveState.GameOver;
                    }
                    else
                    {
                        playerCtrl.State = PlayerControl.MoveState.KnockBack;
                        var frags = Physics.OverlapSphere(transform.position, 10f);
                        foreach (var obj in frags)
                        {
                            obj.gameObject.SendMessage("Damage", 2f, SendMessageOptions.DontRequireReceiver);
                        }

                        var objMgr = InGameManager.instance.objectManager.GetComponent<ObjectManager>();
                        StartCoroutine(objMgr.wallDestroy(other.transform.parent.gameObject));
                    }
                }
                playerStat.Init();
            }
        }

        if (other.tag is "HpItem")
        {
            playerStat.CurrentHp = playerStat.MaxHp;
            Destroy(other.gameObject);
        }

        if (other.tag is "PowerItem")
        {
            var itemObj = other.GetComponent<ItemAbility>();
            if (itemObj != null)
            {
                // ��Ż�Ŀ��� set�ϴ� ���� �÷��̾� �������ݷ¿� x �ϴ� ������
                playerStat.TotalPower = itemObj.ItemPower;
                Debug.Log(playerStat.TotalPower);
                Destroy(other.gameObject);
            }
        }

        if(other.tag is "BonusWall")
        {
            playerAni.SetTrigger("Punch");
            getScore.ScoreUp();
            getScore.BonusCount -= 1;
            var frags = Physics.OverlapSphere(transform.position, 5f);
            foreach (var obj in frags)
            {
                obj.gameObject.SendMessage("Damage", 1000f, SendMessageOptions.DontRequireReceiver);
                var rigid = obj.gameObject.GetComponent<Rigidbody>();
                if (rigid != null)
                {
                    rigid.AddForce(new Vector3(0f, 2f, 3f), ForceMode.Impulse);
                }
            }
            //Destroy(other.gameObject);
        }

        if(other.tag is "Wall")
        {
            playerAni.SetTrigger("Punch");
        }
    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag is "Finish")
    //    {
    //        Debug.Log("������");
    //        var rigid = collision.transform.GetComponent<Rigidbody>();
    //        rigid.AddForce(new Vector3(0f, 13f, 20f), ForceMode.Impulse);
    //    }
    //}
}
//var rigid = obj.gameObject.GetComponent<Rigidbody>();
//if (rigid != null)
//{
//    rigid.AddForce(new Vector3(0f, 2f, 3f), ForceMode.Impulse);
//}