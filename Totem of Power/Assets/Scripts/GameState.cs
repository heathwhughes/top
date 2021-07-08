using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] public int chunkCounter;
    [SerializeField] public Block[] blocks;

    // Start is called before the first frame update
    void Start()
    {
        blocks = FindObjectsOfType<Block>();
        chunkCounter = blocks.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
