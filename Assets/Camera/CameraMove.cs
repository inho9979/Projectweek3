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
        finishDistance = new Vector3(12.6f, 11.6f, -2.2f);
        finishRotate = new Vector3(32f, -61f, -8f);
        baseRotate = new Vector3(30f, 0f, 0f);
        target = InGameManager.instance.player.transform;
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
    }

    public void InLoadMove()
    {
        transform.position = (target.position + distance);
        transform.rotation = Quaternion.Euler(baseRotate);
    }

    public void FinishLoadMove()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + finishDistance, Time.deltaTime * 2f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finishRotate), Time.deltaTime * 2f);
    }
}
/*Vector3.Lerp(transform.position, target.position + distance, Time.fixedDeltaTime * 10f);*/