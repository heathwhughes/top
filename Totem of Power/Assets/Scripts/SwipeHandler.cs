using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] public float maxSwipeTime = .85f;
    [SerializeField] public float minSwipeTistance = 100f;

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;

    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;
    private float swipeLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TestForSwipe();
    }

    public void TestForSwipe()
    {
        // add swipable object bool to be defaulted to false and set to true in other scripts
        // add a public method to get and set this bool
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
                print("touch began at time: " + swipeStartTime + " and position: " + startSwipePosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndTime = Time.time;
                endSwipePosition = touch.position;
                print("touch ended at time: " + swipeEndTime + " and position: " + endSwipePosition);

                swipeTime = swipeEndTime - swipeStartTime;
                swipeLength = (endSwipePosition - startSwipePosition).magnitude;
                print("swipe time: " + swipeTime + " and swipe length: " + swipeLength);

                if (swipeTime < maxSwipeTime && swipeLength > minSwipeTistance)
                {
                    print("Valid swipe");
                    SwipeControl();
                }
                else
                {
                    print("Invalid swipe");
                }
            }
        }
    }

    // update this method to return whether it's a left or right swipe so other scripts that call it can act on it
    public void SwipeControl()
    {
        Vector2 distance = endSwipePosition - startSwipePosition;
        float xDistanceAbs = Mathf.Abs(distance.x);
        float yDistanceAbs = Mathf.Abs(distance.y);

        if (xDistanceAbs > yDistanceAbs)
        {
            if (distance.x > 0)
            {
                print("horizontal swipe right");
            }
            else
            {
                print("horizontal swipe left");
            }
            
        }
        else if (yDistanceAbs > xDistanceAbs)
        {
            print("vertical swipe");
        }
    }

}
