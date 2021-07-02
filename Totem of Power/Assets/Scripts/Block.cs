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
        // print("starting rotationAngle: " + rotationAngle);
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

        float allowedAngleDifference = Mathf.Abs(transform.eulerAngles.y * floatVariationTolerance);
        float roundedAngle = (float)Math.Round(transform.eulerAngles.y * 10000f) / 10000f;
        float actualDifference = Mathf.Abs(roundedAngle) % 45;
        // print("actual difference: " + actualDifference + " . Allowed difference: " + allowedAngleDifference);
        if (actualDifference <= allowedAngleDifference)
        {
            IsRotating = false;
        }
        else
        {
            IsRotating = true;
        }
        // print("IsRotating: " + IsRotating + ". " + "transform.eulerAngles.y = " + transform.eulerAngles.y + " and rotationAngle = " + rotationAngle);

        
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