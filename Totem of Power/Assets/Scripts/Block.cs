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
    [SerializeField] public List<Enemy> enemiesLeaving = new List<Enemy>();
    [SerializeField] private GameObject chunkBelow;
    GameState gameState;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        rotationAngle = transform.eulerAngles.y;
        swipeHandler = GetComponent<SwipeHandler>();
        faces = GetComponentsInChildren<Face>();
        IsRotating = false;
        if (gameObject.transform.parent.name != "Chunk 0")
        {
            chunkBelow = GameObject.Find("Chunk " + (gameState.chunkCounter - 2));
        }
    }

    private void Update()
    {
        RotateBlock();

        // Detect swipe direction
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

        // Set value for IsRotating
        float roundedAngle = (float)Math.Round(transform.eulerAngles.y * 1000f) / 1000f;
        float angleDifference = Mathf.Abs(roundedAngle) % 45;
        print("angle: " + transform.eulerAngles.y + "; " + "rounded angle: " + roundedAngle + "; " + "actual difference: " + angleDifference);
        if (angleDifference == 0)
        {
            IsRotating = false;
        }
        else
        {
            IsRotating = true;
        }

        UpdateEnemiesAtBoundary();
    }

    public void AddToLeavingList(Enemy enemy)
    {
        enemiesLeaving.Add(enemy);
    }

    public void RemoveFromLeavingList(Enemy enemy)
    {
        enemiesLeaving.Remove(enemy);
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

    private void UpdateEnemiesAtBoundary()
    {
        if (IsRotating)
        {
            // set moving=false for each enemy leaving this block
            foreach (Enemy enemy in enemiesLeaving)
            {
                enemy.IsMoving = false;
            }

            if (chunkBelow != null)
            {
                // set moving=false for each enemy leaving the block in the chunk below
                foreach (Enemy enemy in chunkBelow.GetComponentInChildren<Block>().enemiesLeaving)
                {
                    enemy.IsMoving = false;
                }
            }  

        }
        else if (!IsRotating)
        {
            // clear list of enemies entering or leaving
            foreach (Enemy enemy in GetComponentsInChildren<Enemy>())
            {
                enemy.IsMoving = true;
            }
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
            // Set the values to keep track of where in the view each face is after rotation
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