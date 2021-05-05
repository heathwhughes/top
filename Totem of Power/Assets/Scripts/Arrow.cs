using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (gameObject.name == "Left Arrow")
        {
            print("clicked " + gameObject.name);
            var myParent = gameObject.transform.parent;
            print("Parent object: " + myParent);
            print("Quads object: " + myParent.Find("Quads"));
            // Figured out how to grab "Quads object". Next, rotate it.
            
        }
        else
        {
            print("Didn't click Left Arrow");
        }
    }
}
