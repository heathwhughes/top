using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaving : MonoBehaviour
{
    private Block block;

    private void Start()
    {
        block = GetComponentInParent<Block>();
    }

    private void OnTriggerExit(Collider other)
    {
        // add other to the block's leaving list. They should be removed when they get a new parent
        block.AddToLeavingList(other.GetComponentInParent<Enemy>());
        print("enemies leaving block - " + block + " : " + block.enemiesLeaving);
    }

}
