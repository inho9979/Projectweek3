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

    public AudioClip crushSound;
    public AudioClip attackSound;
    public AudioClip dontCrushSound;
    public AudioClip knockBackSound;
    public AudioClip getPower;
    public AudioClip getHeal;
    public AudioClip gameOver;
    public AudioClip finishCrush;
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
            playerCtrl.isBigger = false;
            InGameManager.instance.GameState = InGameManager.InGameState.Bonus;
        }
        
        if(other.tag is "WallPoint")
        {
            var objStat = other.transform.GetComponent<WallStats>();
            if (objStat != null)
            {
                if (objStat.WallHp <= playerStat.TotalPower)
                {
                    getScore.ScoreUp(objStat.WallHp, playerStat.TotalPower);
                    getScore.CurCombo++;
                    ingameUI.ComboHit();
                    SoundManager.Instance.SFXPlay("WallCrush", crushSound);
                    SoundManager.Instance.SFXPlay("Attack", attackSound);

                    //Debug.Log($"curCom: {getScore.CurCombo}, maxCom: {getScore.MaxCombo}");

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
                    getScore.CurCombo = 0;
                    SoundManager.Instance.SFXPlay("DontCrush", dontCrushSound);
                    SoundManager.Instance.SFXPlay("KnockBack", knockBackSound);

                    if (playerStat.CurrentHp <= 0)
                    {
                        SoundManager.Instance.SFXPlay("GameOver", gameOver);
                        playerStat.CurrentHp = 0;
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
            SoundManager.Instance.SFXPlay("HealItem", getHeal);
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
                SoundManager.Instance.SFXPlay("PowerItem", getPower);
                Destroy(other.gameObject);
            }
        }

        if(other.tag is "BigItem")
        {
            playerCtrl.State = PlayerControl.MoveState.Big;
            StartCoroutine(ReturnSmall());
        }

        if(other.tag is "BonusWall")
        {
            SoundManager.Instance.SFXPlay("FinishCrush", finishCrush);
            playerAni.SetTrigger("Punch");
            getScore.BonusCount -= 1;
            var frags = Physics.OverlapSphere(transform.position, 5f);
            foreach (var obj in frags)
            {
                obj.gameObject.SendMessage("Damage", 1000f, SendMessageOptions.DontRequireReceiver);
            }

            var objMgr = InGameManager.instance.objectManager.GetComponent<ObjectManager>();
            StartCoroutine(objMgr.BonusWallDestroy(other.transform.gameObject));
        }

        if(other.tag is "Wall")
        {
            playerAni.SetTrigger("Punch");
        }
    }

    IEnumerator ReturnSmall()
    {
        yield return new WaitForSeconds(7f);
        playerCtrl.isBigger = false;
        Debug.Log(playerCtrl.isBigger);
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