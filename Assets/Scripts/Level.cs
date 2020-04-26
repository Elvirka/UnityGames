using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int breakableBlocks;

    private ScenesLoader scenesLoader;

    private void Start()
    {
        scenesLoader = GetComponent<ScenesLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }
    
    public void CountDestroyedBlocks()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            scenesLoader.LoadNextScene();
        }
    }
}
