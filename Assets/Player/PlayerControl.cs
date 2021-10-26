using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody playerRigid;
    private Animator playerAni;
    private Touch touchFuc;

    private float moveToX = 3f;
    private bool isMove;

    // Ʈ����
    private bool isClear;
    public bool IsClear
    {
        get => isClear;
        set
        {
            isClear = value;
        }
    }
    //private bool isStart;
    //public bool IsStart
    //{
    //    get => isStart;
    //    set
    //    {
    //        isStart = value;
    //    }
    //}
    private bool isBonus;
    public bool IsBonus
    {
        get => isBonus;
        set
        {
            isBonus = value;
        }
    }

    private float moveSpeed = 0f;
    private float timer = 0f;

    private Vector3 curPos;
    public enum MoveState
    {
        Idle,
        Run,
        SideRun,
        KnockBack,
        ClearMove,
        End,
        GameOver,
    }
    public enum MoveDirection
    {
        front,
        right,
        left,
    }

    private MoveDirection moveDirect;

    private MoveState state;
    public MoveState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            switch (state)
            {
                case MoveState.Idle:
                    moveSpeed = 0f;
                    break;
                case MoveState.Run:
                    moveSpeed = 10f;
                    break;
                case MoveState.SideRun:
                    moveSpeed = 10f;
                    curPos = transform.position;
                    break;
                case MoveState.KnockBack:
                    playerAni.SetTrigger("Knock");
                    moveSpeed = 2f;
                    break;
                case MoveState.ClearMove:
                    moveSpeed = 3f;
                    break;
                case MoveState.End:
                    moveSpeed = 0f;
                    break;
                case MoveState.GameOver:
                    playerAni.SetTrigger("Down");
                    moveSpeed = 2f;
                    break;
            }
        }
    }
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerAni = GetComponent<Animator>();
        State = MoveState.Idle;
        isClear = false;
        isBonus = false;
        transform.position = new Vector3(0f, 0f, -100f);
        touchFuc = GameObject.FindWithTag("Touch").GetComponent<Touch>();

    }


    void FixedUpdate()
    {
        if(InGameManager.instance.GameState != InGameManager.InGameState.Tutorial)
        {
            playerAni.SetFloat("Speed", moveSpeed);
            Debug.Log(moveSpeed);
            switch (State)
            {
                case MoveState.Idle:
                    PlayerIdle();
                    break;
                case MoveState.Run:
                    PlayerRun();
                    break;
                case MoveState.SideRun:
                    PlayerSideRun();
                    break;
                case MoveState.KnockBack:
                    PlayerKnockBack();
                    break;
                case MoveState.ClearMove:
                    PlayerClearWalk();
                    break;
                case MoveState.End:
                    PlayerFinishRun();
                    break;
                case MoveState.GameOver:
                    PlayerGameOver();
                    break;
            }
        }
    }
    // Ŭ���� Ʈ���� -> ���ӸŴ������� ��Ű�� ����
    public void StageClear()
    {
        State = MoveState.Idle;
        isClear = true;
    }
    public void PlayerStart()
    {
        State = MoveState.Run;
        InGameManager.instance.ui.GetComponent<UIcontrol>().ingameUI.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void PlayerIdle()
    {
        //if (Input.GetMouseButtonDown(0) && !isClear)
        //{
        //    State = MoveState.Run;
        //}

        //if(touchFuc.Tap() && !isClear)
        //{
        //    State = MoveState.Run;
        //}

        if (isClear)
        {
            State = MoveState.ClearMove;
        }
    }
    private void PlayerRun()
    {
        var moveDir = transform.forward;
        playerRigid.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);

        //var Swipevec = touchFuc.Swipe();
        //if (Swipevec != Vector2.zero)
        //{
        //    Debug.Log("��������");
        //    moveDirect = Swipevec == Vector2.right ? MoveDirection.right : MoveDirection.left;
        //    State = MoveState.SideRun;

        //}
        if (Input.GetMouseButton(0) && isMove == false)
        {
            Debug.Log("��ưŬ��");
            var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            moveDirect = pos.x > 0.5f ? MoveDirection.right : MoveDirection.left;
            State = MoveState.SideRun;
        }
    }

    private void PlayerSideRun()
    {
        // ����, ������ ����ó��
        if ((transform.position.x >= moveToX && moveDirect == MoveDirection.right)
            || (transform.position.x <= -moveToX && moveDirect == MoveDirection.left))
        {
            State = MoveState.Run;
            playerRigid.MoveRotation(Quaternion.identity);
            isMove = false;
            return;
        }
        // ���� �Ѱ� �̵��� ���� ��ȯ , curPos�� �������� �̵� ������ Pos
        if ((transform.position.x <= curPos.x - moveToX && moveDirect == MoveDirection.left)
            || (transform.position.x >= curPos.x + moveToX && moveDirect == MoveDirection.right))
        {
            State = MoveState.Run;
            playerRigid.MoveRotation(Quaternion.identity);
            isMove = false;
            return;
        }
        if (moveDirect == MoveDirection.left)
        {
            isMove = true;
            playerRigid.MoveRotation(Quaternion.Euler(0f, -35f, 0f));
            playerRigid.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }
        else if (moveDirect == MoveDirection.right)
        {
            isMove = true;
            playerRigid.MoveRotation(Quaternion.Euler(0f, 35f, 0f));
            playerRigid.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void PlayerKnockBack()
    {
        //if(playerAni.GetCurrentAnimatorStateInfo(0).IsName("KnockBack"))
        //{
        //}
        // else
        //{
        //}
        timer += Time.deltaTime;
        var moveDir = -transform.forward;
        playerRigid.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        if (timer > 1f)
        {
            if (Mathf.Abs(transform.position.x) < 1.5)
            {
                transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            }
            else
            {
               transform.position = transform.position.x > 0 ? new Vector3(3f, transform.position.y, transform.position.z)
                    : new Vector3(-3f, transform.position.y, transform.position.z);
            }
            //playerRigid.MoveRotation(Quaternion.identity);
            // ���ڵ徲�� �۵� �� �ȵ�
            transform.rotation = Quaternion.identity;
            state = MoveState.Run;
            moveSpeed = 10f;
            timer = 0f;
            isMove = false;
        }
    }

    private void PlayerClearWalk()
    {
        var bonusPos = GameObject.FindWithTag("Bonus").transform.position;
        var distance = Vector3.Distance(transform.position, bonusPos);
        var dir = (bonusPos - transform.position).normalized;

        playerRigid.MoveRotation(Quaternion.identity);
        playerRigid.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);

        if(distance <= 3f)
        {
            State = MoveState.End;
            isBonus = true;
        }
    }
    private void PlayerFinishRun()
    {
        var dir = transform.forward;
        timer += Time.deltaTime;
        // 2�� ���
        if (timer <= 2f)
            return;

        var camera = InGameManager.instance.camera.GetComponent<CameraMove>();
        camera.CameraState = CameraMove.State.FinishLoad;

        moveSpeed = 15f;
        if(isBonus)
        {
            playerRigid.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            var endPos = GameObject.FindWithTag("BonusEnd").transform.position;
            var distance = Vector3.Distance(transform.position, endPos);
            var enddir = (endPos - transform.position).normalized;
            
            if (distance >= 2f)
            {
                moveSpeed = 5f;
                playerRigid.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                moveSpeed = 0f;
                InGameManager.instance.GameClear();
            }
        }
    }
    private void PlayerGameOver()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            //if (Mathf.Abs(transform.position.x) < 1.5)
            //{
            //    transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            //}
            //else
            //{
            //    transform.position = transform.position.x > 0 ? new Vector3(3f, transform.position.y, transform.position.z)
            //         : new Vector3(-3f, transform.position.y, transform.position.z);
            //}
            //transform.rotation = Quaternion.identity;
            //state = MoveState.Run;
            //moveSpeed = 10f;
            //timer = 0f;
            //isMove = false;
            moveSpeed = 0f;
            transform.position = curPos + new Vector3(0f, 0.12f, 0f);
        }
        else
        {
            var moveDir = -transform.forward;
            playerRigid.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            curPos = transform.position;
        }
    }
}

// �Ⱦ��� �ڵ�
//IEnumerator MoveToX(int dir)
//{
//    Debug.Log("X���̵� ����");
//    float curPer = 0f;
//    var startPos = transform.position;
//    var endX = transform.position.x + dir * moveToX;
//    var endPos = new Vector3(endX, startPos.y, startPos.z);

//    isMove = true;

//    while(curPer < 1f)
//    {
//        curPer += Time.fixedDeltaTime;
//        var moveVec = Vector3.Lerp(startPos, endPos, curPer);

//        playerRigid.MovePosition(moveVec);
//        //playerRigid.MovePosition(transform.position + transform.forward * (moveSpeed / 2) * Time.fixedDeltaTime);
//        transform.rotation = Quaternion.Euler(new Vector3(0f, dir * 35f, 0f));
//        yield return null;
//    }

//    isMove = false;
//    State = MoveState.Run;
//    transform.rotation = Quaternion.identity;
//    curCoroutine = null;
//    Debug.Log("X���̵� ����");
//}

//Swipevec = touchFuc.Swipe();
//if (Swipevec != Vector2.zero)
//{
//    moveState = MoveState.SideRun;
//}


//public void Init()
//{
//    State = MoveState.Idle;
//    isClear = false;
//    transform.position = new Vector3(0f, 0f, -100f);
//}