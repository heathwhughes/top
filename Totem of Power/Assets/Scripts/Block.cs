using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 5f;
    float rotationAngle;

    public SwipeHandler swipeHandler;

    private void Start()
    {
        rotationAngle = transform.eulerAngles.y;
        swipeHandler = FindObjectOfType<SwipeHandler>();
    }

    private void Update()
    {
        RotateBlock();

    }

    public void OnMouseDown()
    {
        swipeHandler.TestForSwipe();
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
