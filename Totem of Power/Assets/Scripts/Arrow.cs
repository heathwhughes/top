using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // This will make it move 90 degrees per second
    [SerializeField] float rotateSpeed = 5f;
    bool rotateLeft = false;
    bool rotateRight = false;
    float rotationAngle;
    Transform Quad;

    private void Start()
    {
        Quad = gameObject.transform.parent.Find("Quads");
        rotationAngle = Quad.eulerAngles.y;
    }

    private void Update()
    {
        if (rotateLeft || rotateRight)
        {
            RotateQuads();    
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Left Arrow")
        {
            rotateLeft = true;
            rotateRight = false;
            rotationAngle += 90;
            print("rotationAngle: " + rotationAngle);
        }
        else
        {
            rotateRight = true;
            rotateLeft = false;
            rotationAngle -= 90;
            print("rotationAngle: " + rotationAngle);
        }
    }

    private void RotateQuads()
    {
        Quad.transform.rotation = Quaternion.Slerp(
            Quaternion.Euler(Quad.transform.eulerAngles),
            Quaternion.Euler(0f, rotationAngle, 0f),
            Time.deltaTime * rotateSpeed);
    }
}