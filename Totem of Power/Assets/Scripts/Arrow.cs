using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // This will make it move 90 degrees per second
    [SerializeField] private float speed = 90;
    float yRotation = 0;
    bool rotateLeft = false;
    bool rotateRight = false;
   
    float testSpeed = 0.1f;


    private void Update()
    {
        if (rotateLeft || rotateRight)
        {
            // RotateQuads();    
        }

        // Interpolates rotation between the rotations
        // of from and to.
        // (Choose from and to not to be the same as
        // the object you attach this script to)

        // This works!!
        var Quads = gameObject.transform.parent.Find("Quads");
        print("Quads rotation before: " + Quads.transform.rotation);
        Quads.transform.rotation = Quaternion.Lerp(Quads.transform.rotation, Quaternion.Euler(0f, 90, 0f), Time.deltaTime * testSpeed);
        print("Quads rotation after: " + Quads.transform.rotation);
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Left Arrow")
        {
            rotateLeft = true;
            rotateRight = false;
        }
        else
        {
            rotateRight = true;
            rotateLeft = false;
            print("Didn't click Left Arrow");
        }
    }

    private void RotateQuads()
    {
        if (rotateRight)
        {
            speed *= -1;
        }
        else
        {
            speed = Mathf.Abs(speed);
        }

        var myParent = gameObject.transform.parent;

        var Quads = myParent.Find("Quads");
        print("Quads rotation before update: " + Quads.rotation.eulerAngles);


        yRotation += Time.deltaTime * speed;

        print("Y rotation to be applied: " + yRotation);
        Quaternion newYRotation = Quaternion.Euler(0f, Quads.rotation.eulerAngles.y + yRotation, 0f);
        // Quads.rotation = Quaternion.Euler(0f, Quads.rotation.eulerAngles.y + yRotation, 0f);
        Quads.rotation = Quaternion.Lerp(Quads.rotation, newYRotation, Time.time * speed);
        print("Quads rotation after update: " + Quads.rotation);
    }
}
