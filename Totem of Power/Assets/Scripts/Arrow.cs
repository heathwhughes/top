using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Block block;

    private void Start()
    {
        block = gameObject.transform.parent.Find("Block").GetComponent<Block>();

    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Left Arrow")
        {
            block.HandleRotation(true);
        }
        else
        {
            block.HandleRotation(false);
        }
 
    }
        
}