using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int blocksCount;

    private ScenesLoader scenesLoader;

    private void Start()
    {
        scenesLoader = GetComponent<ScenesLoader>();
    }

    public void CountBlocks()
    {
        blocksCount++;
    }
    
    public void CountDestroyedBlocks()
    {
        blocksCount--;
        if (blocksCount <= 0)
        {
            scenesLoader.LoadNextScene();
        }
    }
}
