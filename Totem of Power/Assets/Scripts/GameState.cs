using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    [SerializeField] public int chunkCounter;
    [SerializeField] public Block[] blocks;

    void Awake()
    {
        // Set the chunkCounter. Will need to update this value when additional chunks are added
        blocks = FindObjectsOfType<Block>();
        chunkCounter = blocks.Length;
    }

}
