using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 5f;
    float rotationAngle;

    private SwipeHandler swipeHandler;

    private void Start()
    {
        rotationAngle = transform.eulerAngles.y;
        swipeHandler = GetComponent<SwipeHandler>();
    }

    private void Update()
    {
        RotateBlock();

        if (swipeHandler.currentSwipeDirection.Equals("left"))
		{
            this.SetRotationDirection(true);
            swipeHandler.currentSwipeDirection = "none";
		}
        else if (swipeHandler.currentSwipeDirection == "right")
		{
            this.SetRotationDirection(false);
            swipeHandler.currentSwipeDirection = "none";
        }
    }

    public void OnMouseDown()
    {
        swipeHandler.HasCurrentSwipeableObject = true;
    }

    public void SetRotationDirection(bool isLeft)
    {
        if (isLeft)
        {
            rotationAngle += 90;
        }
        else
        {
            rotationAngle -= 90;
        }
    }

    private void RotateBlock()
    {
        transform.rotation = Quaternion.Slerp(
            Quaternion.Euler(transform.eulerAngles),
            Quaternion.Euler(transform.eulerAngles.x, rotationAngle, transform.eulerAngles.z),
            Time.deltaTime * rotationSpeed);
    }

}
