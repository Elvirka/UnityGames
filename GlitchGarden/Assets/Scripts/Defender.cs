﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] private int starCost = 100;
    
    public int GetStarCost()
    {
        return starCost;
    }
    
    public void AddStars(int amount)
    {
        StarsDisplay starsDisplay = FindObjectOfType<StarsDisplay>();
        starsDisplay.AddStars(amount);
    }
}
