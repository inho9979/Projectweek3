using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class CameraMove : MonoBehaviour
{
    private Transform target;
    private Vector3 distance;
    private Vector3 finishDistance2;
    private Vector3 finishRotate2;
    private Vector3 baseRotate;

    private Transform statsPos;

    public GameObject timeLine;
    public GameObject timeLine2;
    private Transform[] virtualCam;

    private Vector3 vir1Distance;
    private Vector3 vir2Distance;
    private Vector3 vir3Distance;
    private Vector3 vir1Rotate;
    private Vector3 vir2Rotate;
    private Vector3 vir3Rotate;
    public enum State
    {
        InLoad,
        FinishLoad,
        Ending,
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
                    GetComponent<CinemachineBrain>().enabled = true;
                    timeLine.SetActive(true);
                    timeLine.GetComponent<PlayableDirector>().playOnAwake = true;
                    timeLine.GetComponent<PlayableDirector>().Play();
                    break;
                case State.Ending:
                    GetComponent<CinemachineBrain>().enabled = true;
                    timeLine2.SetActive(true);
                    timeLine2.GetComponent<PlayableDirector>().playOnAwake = true;
                    timeLine2.GetComponent<PlayableDirector>().Play();
                    break;
            }
        }
    }

    void Start()
    {
        distance = new Vector3(0f, 6f, -4.5f);
        baseRotate = new Vector3(30f, 0f, 0f);
        finishDistance2 = new Vector3(6f, 10f, 23f);
        finishRotate2 = new Vector3(38.5f, -155f, -3.9f);

        vir1Distance = new Vector3(1f, 3.3f, 3.3f);
        vir1Rotate = new Vector3(32f, -166f, -6f);
        vir2Distance = new Vector3(5.8f, 5.5f, 5.9f);
        vir2Rotate = new Vector3(38.5f, -135f, -3.9f);
        vir3Distance = new Vector3(5.6f, 7.3f, 13f);
        vir3Rotate = new Vector3(38.5f, -155f, -3.9f);

        target = InGameManager.instance.player.transform;
        statsPos = GameObject.FindWithTag("PlayerStat").transform;
        GetComponent<CinemachineBrain>().enabled = false;
        timeLine.GetComponent<PlayableDirector>().playOnAwake = false;
        timeLine.SetActive(false);

        timeLine2.GetComponent<PlayableDirector>().playOnAwake = false;
        timeLine2.SetActive(false);

        var Cam = GameObject.FindWithTag("VirCam").transform;
        virtualCam = new Transform[Cam.childCount];
        for (int i = 0; i < Cam.childCount; i++) 
        {
            virtualCam[i] = Cam.GetChild(i);
        }
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
            case State.Ending:
                break;
        }
        statsPos.position = (target.position + new Vector3(0f, 1f, -0.5f));
    }

    public void InLoadMove()
    {
        transform.position = (target.position + distance);
        transform.rotation = Quaternion.Euler(baseRotate);
        VirCamMove();
    }

    public void FinishLoadMove()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + finishDistance2, Time.deltaTime * 3f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finishRotate2), Time.deltaTime * 3f);
        VirCamMove();

    }

    public void VirCamMove()
    {
        virtualCam[0].position = target.position + vir1Distance;
        virtualCam[0].rotation = Quaternion.Euler(vir1Rotate);
        virtualCam[1].position = target.position + vir2Distance;
        virtualCam[1].rotation = Quaternion.Euler(vir2Rotate);
        virtualCam[2].position = target.position + vir3Distance;
        virtualCam[2].rotation = Quaternion.Euler(vir3Rotate);
    }

    public void CineStop()
    {
        GetComponent<CinemachineBrain>().enabled = false;
        timeLine.GetComponent<PlayableDirector>().playOnAwake = false;
        timeLine.SetActive(false);
        InGameManager.instance.playerFinishTrigger = true;
    }
    public void CineStop2()
    {
        //GetComponent<CinemachineBrain>().enabled = false;
        timeLine2.GetComponent<PlayableDirector>().playOnAwake = false;
        timeLine2.SetActive(false);

        InGameManager.instance.GameState = InGameManager.InGameState.Clear;
    }
}

//public IEnumerator CoFinish()
//{
//    StartCoroutine(CoCameraPos2(5f));
//    yield return new WaitForSeconds(1f);
//}

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

//public IEnumerator CoCameraPos2(float duration)
//{
//    Debug.Log(isFinish);
//    while (timer < 2f)
//    {
//        timer += Time.deltaTime;
//        transform.position = Vector3.Lerp(transform.position, target.position + finishDistance, Time.deltaTime * 3f);
//        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finishRotate), Time.deltaTime * 3f);
//        yield return null;
//    }
//    isFinish = true;
//    Debug.Log(isFinish);
//}
/*Vector3.Lerp(transform.position, target.position + distance, Time.fixedDeltaTime * 10f);*/


//        timer += Time.deltaTime;
////if (timer < 1f)
////{
//    var cameraPos = Vector3.up;
//    transform.RotateAround(target.position, cameraPos, 180f * Time.deltaTime);
//    transform.position = target.position + new Vector3(0f, 3f, 5f);
////}
///
//finishDistance = new Vector3(10.6f, 9f, -4.2f);
//finishRotate = new Vector3(32f, -61f, -8f);
