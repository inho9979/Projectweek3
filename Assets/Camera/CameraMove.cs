using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class CameraMove : MonoBehaviour
{
    private Transform target;
    private Vector3 distance;
    private Vector3 finishDistance;
    private Vector3 finishDistance2;
    private Vector3 finishRotate;
    private Vector3 finishRotate2;
    private Vector3 baseRotate;

    private Transform statsPos;
    private float timer = 0f;

    private bool isFinish = false;
    //private PlayerControl playerComponent;
    public enum State
    {
        InLoad,
        FinishLoad,
    }

    private State cameraState;
    public State CameraState
    {
        get => cameraState;
        set
        {
            cameraState = value;
            switch (cameraState)
            {
                case State.InLoad:
                    break;
                case State.FinishLoad:
                    StartCoroutine(CoCameraPos2(2f));
                    break;
            }
        }
    }

    void Start()
    {
        distance = new Vector3(0f, 6f, -4.5f);
        baseRotate = new Vector3(30f, 0f, 0f);
        finishDistance = new Vector3(10.6f, 9f, -4.2f);
        finishDistance2 = new Vector3(7.5f, 12f, 18f);

        finishRotate = new Vector3(32f, -61f, -8f);
        finishRotate2 = new Vector3(32f, -145f, -7.5f);
        target = InGameManager.instance.player.transform;

        statsPos = GameObject.FindWithTag("PlayerStat").transform;

    }

    void LateUpdate()
    {
        switch (cameraState)
        {
            case State.InLoad:
                InLoadMove();
                break;
            case State.FinishLoad:
                FinishLoadMove();
                break;
            default:
                break;
        }

        statsPos.position = (target.position + new Vector3(0f, 1f, -0.5f));
    }

    public void InLoadMove()
    {
        transform.position = (target.position + distance);
        transform.rotation = Quaternion.Euler(baseRotate);
    }

    public void FinishLoadMove()
    {
        if (isFinish)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + finishDistance2, Time.deltaTime * 3f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finishRotate2), Time.deltaTime * 3f);
        }
    }

    public IEnumerator CoFinish()
    {
        StartCoroutine(CoCameraPos2(5f));
        yield return new WaitForSeconds(1f);
        //StartCoroutine(CoCameraPos1(10f));
    }

    //public IEnumerator CoCameraPos1(float duration)
    //{
    //    Debug.Log(isFinish);

    //    var timer = 0f;
    //    while(timer < duration)
    //    {
    //        timer += Time.deltaTime;
    //        transform.position = Vector3.Lerp(transform.position, target.position + finishDistance, timer/duration);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finishRotate), timer / duration);
    //        yield return null;
    //    }
    //}

    public IEnumerator CoCameraPos2(float duration)
    {
        Debug.Log(isFinish);
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position + finishDistance, Time.deltaTime * 3f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finishRotate), Time.deltaTime * 3f);
            yield return null;
        }
        isFinish = true;
        Debug.Log(isFinish);
    }
}
/*Vector3.Lerp(transform.position, target.position + distance, Time.fixedDeltaTime * 10f);*/


//        timer += Time.deltaTime;
////if (timer < 1f)
////{
//    var cameraPos = Vector3.up;
//    transform.RotateAround(target.position, cameraPos, 180f * Time.deltaTime);
//    transform.position = target.position + new Vector3(0f, 3f, 5f);
////}