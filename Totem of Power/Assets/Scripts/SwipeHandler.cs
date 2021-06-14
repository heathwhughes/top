using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is intended to be placed as a component on an object that we intend to use swipe controls with
public class SwipeHandler : MonoBehaviour
{
    [SerializeField] public float maxSwipeTime = .85f;
    [SerializeField] public float minSwipeTistance = 100f;
    // To be set by the script for the swipeable object
    public bool HasCurrentSwipeableObject { get; set; }
    public string currentSwipeDirection { get; set; }

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;

    private Vector2 startSwipePosition;
    private Vector2 endSwipePosition;
    private float swipeLength;

    // Start is called before the first frame update
    void Start()
    {
        this.HasCurrentSwipeableObject = false;
        this.currentSwipeDirection = "none";
    }

    // Update is called once per frame
    void Update()
    {
        TestForSwipe();
    }

    public void TestForSwipe()
    {
        if (Input.touchCount > 0 && this.HasCurrentSwipeableObject)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
                // print("touch began at time: " + swipeStartTime + " and position: " + startSwipePosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndTime = Time.time;
                endSwipePosition = touch.position;
                // print("touch ended at time: " + swipeEndTime + " and position: " + endSwipePosition);

                swipeTime = swipeEndTime - swipeStartTime;
                swipeLength = (endSwipePosition - startSwipePosition).magnitude;
                // print("swipe time: " + swipeTime + " and swipe length: " + swipeLength);

                if (swipeTime < maxSwipeTime && swipeLength > minSwipeTistance)
                {
                    // print("Valid swipe");
                    this.HasCurrentSwipeableObject = false;
                    SwipeControl();
                }
                else
                {
                    this.HasCurrentSwipeableObject = false;
                    // print("Invalid swipe");
                }
            }
        }
    }

    // This method returns whether it's a left or right swipe so other scripts that call it can act upon that info
    public void SwipeControl()
    {
        Vector2 distance = endSwipePosition - startSwipePosition;
        float xDistanceAbs = Mathf.Abs(distance.x);
        float yDistanceAbs = Mathf.Abs(distance.y);

        if (xDistanceAbs > yDistanceAbs)
        {
            if (distance.x > 0)
            {
                this.currentSwipeDirection = "right";
                // print("current swipe direction: " + currentSwipeDirection);
            }
            else
            {
                this.currentSwipeDirection = "left";
                // print("current swipe direction: " + currentSwipeDirection);
            }
            
        }
        else if (yDistanceAbs > xDistanceAbs)
        {
            this.currentSwipeDirection = "up/down";
            // print("current swipe direction: " + currentSwipeDirection);
        }
    }

}