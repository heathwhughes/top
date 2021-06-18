using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public bool IsInView { get; set; }
    public bool IsVisibleLeft { get; set; }
    public bool IsVisibleRight { get; set; }
    public bool IsHiddenLeft { get; set; }
    public bool IsHiddenRight { get; set; }

    private void Start()
    {
        // Set initial values for where each face in in the view for sorting layers
        if (gameObject.name == "Face 1" || gameObject.name == "Face 2")
        {
            IsInView = true;
            IsHiddenLeft = false;
            IsHiddenRight = false;

            if (gameObject.name == "Face 1")
            {
                IsVisibleLeft = true;
                IsVisibleRight = false;
            }
            else
            {
                IsVisibleLeft = false;
                IsVisibleRight = true;
            }
        }
        else
        {
            IsInView = false;
            IsVisibleLeft = false;
            IsVisibleRight = false;

            if (gameObject.name == "Face 3")
            {
                IsHiddenRight = true;
                IsHiddenLeft = false;
            }
            else if (gameObject.name == "Face 4")
            {
                IsHiddenLeft = true;
                IsHiddenRight = false;
            }
            else
            {
                Debug.LogError("Invalid Face object name.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // become the new parent object for enemies when they touch the collider
        other.transform.parent = transform;
    }

}
