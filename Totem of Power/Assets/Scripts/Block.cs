using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public bool IsRotating { get; set; }
    [SerializeField] float rotationSpeed = 5f;
    float rotationAngle;

    private SwipeHandler swipeHandler;
    private Face[] faces;
    private readonly float floatVariationTolerance = .000001f;

    private void Start()
    {
        rotationAngle = transform.eulerAngles.y;
        swipeHandler = GetComponent<SwipeHandler>();
        faces = GetComponentsInChildren<Face>();
        IsRotating = false;
    }

    private void Update()
    {
        RotateBlock();

        if (swipeHandler.currentSwipeDirection.Equals("left"))
		{
            this.HandleRotation(true);
            swipeHandler.currentSwipeDirection = "none";
		}
        else if (swipeHandler.currentSwipeDirection == "right")
		{
            this.HandleRotation(false);
            swipeHandler.currentSwipeDirection = "none";
        }

        // Update value for IsRotating
        float allowedAngleDifference = Mathf.Abs(transform.eulerAngles.y * floatVariationTolerance);
        float roundedAngle = (float)Math.Round(transform.eulerAngles.y * 10000f) / 10000f;
        float actualDifference = Mathf.Abs(roundedAngle) % 45;
        if (actualDifference <= allowedAngleDifference)
        {
            IsRotating = false;
        }
        else
        {
            IsRotating = true;
        }
        
    }

    public void OnMouseDown()
    {
        swipeHandler.HasCurrentSwipeableObject = true;
    }

    public void HandleRotation(bool isLeft)
    {
        if (isLeft)
        {
            rotationAngle += 90;
            HandleSortingLayerForFaces(true);
        }
        else
        {
            rotationAngle -= 90;
            HandleSortingLayerForFaces(false);
        }  
    }

    private void RotateBlock()
    {
        transform.rotation = Quaternion.Slerp(
            Quaternion.Euler(transform.eulerAngles),
            Quaternion.Euler(transform.eulerAngles.x, rotationAngle, transform.eulerAngles.z),
            Time.deltaTime * rotationSpeed);
    }

    private void HandleSortingLayerForFaces(bool isLeftRotation)
    {
        
        foreach (Face face in faces)
        {
            // Determine set the values to keep track of where in the view each face is after rotation
            if (face.IsInView)
            {
                if (face.IsVisibleLeft && !face.IsVisibleRight && isLeftRotation)
                {
                    face.IsInView = false;
                    face.IsVisibleLeft = false;
                    face.IsHiddenLeft = true;
                    print("Face that should be hidden left: " + face);
                    print("IsInView that should be false: " + face.IsInView);
                    print("IsVisibleLeft that should be false: " + face.IsVisibleLeft);
                    print("IsHiddenLeft that should be true: " + face.IsHiddenLeft);
                }
                else if (!face.IsVisibleLeft && face.IsVisibleRight && isLeftRotation)
                {
                    face.IsVisibleLeft = true;
                    face.IsVisibleRight = false;
                }
                else if (!face.IsVisibleLeft && face.IsVisibleRight  && !isLeftRotation)
                {
                    face.IsInView = false;
                    face.IsVisibleRight = false;
                    face.IsHiddenRight = true;
                }
                else if (face.IsVisibleLeft && !face.IsVisibleRight && !isLeftRotation)
                {
                    face.IsVisibleLeft = false;
                    face.IsVisibleRight = true;
                }
                else if (face.IsVisibleLeft && face.IsVisibleRight)
                {
                    Debug.LogError("face.IsVisibleLeft and face.IsVisibleRight should not both be true.");
                }
            }
            else if (!face.IsInView)
            {
                if (face.IsHiddenLeft && !face.IsHiddenRight && isLeftRotation)
                {
                    face.IsHiddenLeft = false;
                    face.IsHiddenRight = true;
                }
                else if (!face.IsHiddenLeft && face.IsHiddenRight && isLeftRotation)
                {
                    face.IsInView = true;
                    face.IsVisibleRight = true;
                    face.IsHiddenRight = false;
                }
                else if (!face.IsHiddenLeft && face.IsHiddenRight && !isLeftRotation)
                {
                    face.IsHiddenRight = false;
                    face.IsHiddenLeft = true;
                }
                else if (face.IsHiddenLeft && !face.IsHiddenRight && !isLeftRotation)
                {
                    face.IsInView = true;
                    face.IsHiddenLeft = false;
                    face.IsVisibleLeft = true;
                }
                else if (face.IsHiddenLeft && face.IsHiddenRight)
                {
                    Debug.LogError("face.IsHiddenLeft and face.IsHiddenRight should not both be true.");
                }
            }

        }

    }

}