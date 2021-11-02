using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private CharactorStats playerStat;
    private Score getScore;
    private Animator playerAni;
    private PlayerControl playerCtrl;
    private PlayerEffect playerEffect;
    private IngameUI ingameUI;
    void Start()
    {
        playerStat = gameObject.GetComponent<CharactorStats>();
        playerAni = gameObject.GetComponent<Animator>();
        playerCtrl = gameObject.GetComponent<PlayerControl>();
        getScore = InGameManager.instance.score.GetComponent<Score>();
        playerEffect = gameObject.GetComponent<PlayerEffect>();

        var UImgr = InGameManager.instance.ui.GetComponent<InGameUImanager>();
        ingameUI = UImgr.ingameUI.GetComponent<IngameUI>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag is "ClearFlag")
        {
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
                    getScore.CurCombo++;
                    ingameUI.ComboHit();
                    Debug.Log($"{getScore.CurCombo}, {getScore.MaxCombo}");

                    var frags = Physics.OverlapSphere(transform.position, 10f);
                    foreach (var obj in frags)
                    {
                        obj.gameObject.SendMessage("Damage", 1000f, SendMessageOptions.DontRequireReceiver);
                    }
                    var objMgr = InGameManager.instance.objectManager.GetComponent<ObjectManager>();
                    other.transform.parent.GetComponent<Wall>().DestroyMesh();
                    StartCoroutine(objMgr.wallDestroy(other.transform.parent.gameObject));
                    playerEffect.AttackEffect();
                }
                else
                {
                    var damage = objStat.WallHp - playerStat.TotalPower;
                    playerStat.CurrentHp -= damage;
                    Debug.Log($"{damage} , {playerStat.CurrentHp}");
                    getScore.CurCombo = 0;

                    if (playerStat.CurrentHp <= 0)
                    {
                        InGameManager.instance.GameState = InGameManager.InGameState.GameOver;
                    }
                    else
                    {
                        playerCtrl.State = PlayerControl.MoveState.KnockBack;
                        var frags = Physics.OverlapSphere(transform.position, 10f);
                        foreach (var obj in frags)
                        {
                            obj.gameObject.SendMessage("Damage", 1f, SendMessageOptions.DontRequireReceiver);
                        }

                        var objMgr = InGameManager.instance.objectManager.GetComponent<ObjectManager>();
                        other.transform.parent.GetComponent<Wall>().DestroyMesh();
                        StartCoroutine(objMgr.wallDestroy(other.transform.parent.gameObject));
                    }

                    playerEffect.KnockBackEffect();
                }
                // 파워 아이템으로 올라간것 다시 초기화
                playerStat.powerInit();
            }
        }

        if (other.tag is "HpItem")
        {
            playerStat.CurrentHp = playerStat.MaxHp;
            playerEffect.HealEffect();
            Destroy(other.gameObject);
        }

        if (other.tag is "PowerItem")
        {
            var itemObj = other.GetComponent<ItemAbility>();
            if (itemObj != null)
            {
                // 토탈파워에 set하는 값은 플레이어 기존공격력에 x 하는 배율값
                playerStat.TotalPower = itemObj.ItemPower;
                playerEffect.PowerEffect();
                Destroy(other.gameObject);
            }
        }

        if(other.tag is "BonusWall")
        {
            playerAni.SetTrigger("Punch");
            getScore.BonusCount -= 1;
            var frags = Physics.OverlapSphere(transform.position, 5f);
            foreach (var obj in frags)
            {
                obj.gameObject.SendMessage("Damage", 1000f, SendMessageOptions.DontRequireReceiver);
            }
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
    //        Debug.Log("날리기");
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