using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    //private float tapTimer = 0.5f;
    private float firstTouchTime = 0f;

    private float swipeDistanceInch = 0.3f;
    private float swipeDistancePixels;
    private float fingerId = int.MinValue;

    private Vector2 firstTouchPos;
    //float preDis = 0f;

    private void Awake()
    {
        swipeDistancePixels = Screen.dpi * swipeDistanceInch;
    }

    void Update()
    {
        
    }

    public bool Tap()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                return true;
        }
        return false;
    }

    public Vector2 Swipe()
    {
        Vector2 vec = Vector2.zero;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //if (fingerId == int.MinValue)
                    //{
                        Debug.Log("첫 터치");
                        firstTouchTime = Time.time;
                        firstTouchPos = touch.position;
                        fingerId = touch.fingerId;
                    //}
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    Debug.Log("끝 터치");
                    //if (fingerId == touch.fingerId)
                    //{
                        var endPos = touch.position;
                        var movePos = endPos - firstTouchPos;
                        if (Mathf.Abs(movePos.y) > swipeDistancePixels
                            || Mathf.Abs(movePos.x) > swipeDistancePixels
                            /*&& firstTouchTime + tapTimer < Time.time*/)
                        {

                            if (Mathf.Abs(movePos.x) > Mathf.Abs(movePos.y))
                            {
                                vec = (movePos.x < 0) ? Vector2.left : Vector2.right;
                                Debug.Log(vec);
                            }
                            //else
                            //{
                            //    vec = (movePos.y < 0) ? Vector2.down : Vector2.up;
                            //    Debug.Log(vec);
                            //}
                        }

                    //}
                    fingerId = int.MinValue;
                    break;
            }
        }
        return vec;
    }
}
