using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;
    private Vector3 distance;
    private Vector3 finishDistance;
    private Vector3 finishRotate;
    private Vector3 baseRotate;

    private Transform statsPos;

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
        }
    }

    void Start()
    {
        distance = new Vector3(0f, 6f, -4.5f);
        baseRotate = new Vector3(30f, 0f, 0f);
        finishDistance = new Vector3(10.6f, 9f, -4.2f);
        finishRotate = new Vector3(32f, -61f, -8f);
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
        transform.position = Vector3.Lerp(transform.position, target.position + finishDistance, Time.deltaTime * 3f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finishRotate), Time.deltaTime * 3f);
    }
}
/*Vector3.Lerp(transform.position, target.position + distance, Time.fixedDeltaTime * 10f);*/