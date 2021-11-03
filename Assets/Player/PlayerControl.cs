using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const float playerRunSpeed = 10f;
    public const float playerWalkSpeed = 3f;
    public const float playerBackSpeed = 2f;
}

public class PlayerControl : MonoBehaviour, IStateChangeable
{

    private Rigidbody playerRigid;
    private Animator playerAni;
    private Touch touchFuc;
    private PlayerEffect playerEffect;

    private float moveToX = 3f;
    private bool isMove;

    // 트리거
    private bool isClear;
    public bool IsClear
    {
        get => isClear;
        set
        {
            isClear = value;
        }
    }
    private bool isBonusRun;
    public bool IsBonusRun
    {
        get => isBonusRun;
        set
        {
            isBonusRun = value;
        }
    }

    private float moveSpeed = 0f;
    private float timer = 0f;

    private Vector3 curPos;
    public enum MoveDirection
    {
        front,
        right,
        left,
    }
    private MoveDirection moveDirect;

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
    private MoveState state;
    private MoveState beforeState;
    public MoveState State
    {
        get
        {
            return state;
        }
        set
        {
            beforeState = state;
            state = value;
            switch (state)
            {
                case MoveState.Idle:
                    moveSpeed = 0f;
                    break;
                case MoveState.Run:
                    moveSpeed = Constants.playerRunSpeed;
                    break;
                case MoveState.SideRun:
                    moveSpeed = Constants.playerRunSpeed;
                    curPos = transform.position;
                    break;
                case MoveState.KnockBack:
                    playerAni.SetTrigger("Knock");
                    moveSpeed = Constants.playerBackSpeed;
                    break;
                case MoveState.ClearMove:
                    playerEffect.FinishAuraEffect();
                    var camera = InGameManager.instance.camera.GetComponent<CameraMove>();
                    camera.CameraState = CameraMove.State.FinishLoad;
                    moveSpeed = Constants.playerWalkSpeed;
                    break;
                case MoveState.End:
                    moveSpeed = 0f;
                    break;
                case MoveState.GameOver:
                    playerAni.SetTrigger("Down");
                    moveSpeed = Constants.playerBackSpeed;
                    break;
            }
        }
    }
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerAni = GetComponent<Animator>();
        playerEffect = GetComponent<PlayerEffect>();
        State = MoveState.Idle;
        isClear = false;
        isBonusRun = false;
        transform.position = new Vector3(0f, 0f, -100f);
        touchFuc = GameObject.FindWithTag("Touch").GetComponent<Touch>();
    }

    public void ChangeState(InGameManager.InGameState state)
    {
        switch (state)
        {
            case InGameManager.InGameState.Tutorial:
                State = MoveState.Idle;
                break;
            case InGameManager.InGameState.Start:
                State = MoveState.Run;
                break;
            case InGameManager.InGameState.Play:
                State = beforeState;
                break;
            case InGameManager.InGameState.Pause:
                State = MoveState.Idle;
                break;
            case InGameManager.InGameState.Bonus:
                State =  MoveState.ClearMove;
                break;
            case InGameManager.InGameState.Clear:
                break;
            case InGameManager.InGameState.GameOver:
                State = MoveState.GameOver;
                break;
        }
    }
    void FixedUpdate()
    {

        playerAni.SetFloat("Speed", moveSpeed);
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
    private void PlayerIdle()
    {
    }
    private void PlayerRun()
    {
        var moveDir = transform.forward;
        playerRigid.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        //var Swipevec = touchFuc.Swipe();
        //if (Swipevec != Vector2.zero)
        //{
        //    Debug.Log("스와이프");
        //    moveDirect = Swipevec == Vector2.right ? MoveDirection.right : MoveDirection.left;
        //    State = MoveState.SideRun;
        //}
        if (Input.GetMouseButton(0) && isMove == false)
        {
            Debug.Log("버튼클릭");
            var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            moveDirect = pos.x > 0.5f ? MoveDirection.right : MoveDirection.left;
            State = MoveState.SideRun;
        }
    }

    private void PlayerSideRun()
    {
        // 좌측, 우측끝 예외처리
        if ((transform.position.x >= moveToX && moveDirect == MoveDirection.right)
            || (transform.position.x <= -moveToX && moveDirect == MoveDirection.left))
        {
            State = MoveState.Run;
            playerRigid.MoveRotation(Quaternion.identity);
            isMove = false;
            return;
        }
        // 라인 한곳 이동후 상태 전환 , curPos는 스와이프 이동 시작한 Pos
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
            // 이코드쓰면 작동 잘 안됨
            transform.rotation = Quaternion.identity;
            state = MoveState.Run;
            moveSpeed = Constants.playerRunSpeed;
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
            SoundManager.Instance.BgSoundPlay(SoundManager.Instance.finishBgm);
            isBonusRun = true;
        }
    }
    private void PlayerFinishRun()
    {
        var dir = transform.forward;

        if (InGameManager.instance.playerFinishTrigger == false)
            return;
        if (InGameManager.instance.score.GetComponent<Score>().BonusCount <= 0)
            isBonusRun = false;

        if(isBonusRun)
        {
            // 마지막 달리기는 더 빠르게
            moveSpeed = 16f;
            playerRigid.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            var endPos = GameObject.FindWithTag("BonusEnd").transform.position;
            var distance = Vector3.Distance(transform.position, endPos);
            var enddir = (endPos - transform.position).normalized;
            playerEffect.FinishAuraEffectOff();

            if (distance >= 2f)
            {
                
                moveSpeed = 5f;
                playerRigid.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                moveSpeed = 0f;
                var camera = InGameManager.instance.camera.GetComponent<CameraMove>();
                camera.CameraState = CameraMove.State.Ending;

                State = MoveState.Idle;
                //InGameManager.instance.GameState = InGameManager.InGameState.Clear;
            }
        }
    }
    private void PlayerGameOver()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {

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

// 안쓰는 코드
//IEnumerator MoveToX(int dir)
//{
//    Debug.Log("X축이동 시작");
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
//    Debug.Log("X축이동 종료");
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
//moveSpeed = Constants.playerSpeed;
//timer = 0f;
//isMove = false;

//if (Input.GetMouseButtonDown(0) && !isClear)
//{
//    State = MoveState.Run;
//}
//if(touchFuc.Tap() && !isClear)
//{
//    State = MoveState.Run;
//}

//if (isClear)
//{
//    State = MoveState.ClearMove;
//}

// 클리어 트리거 -> 게임매니저에서 시키는 동작
//public void StageClear()
//{
//    State = MoveState.Idle;
//    isClear = true;
//}