﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = GetComponent<DamageDealer>();
        damageDealer.Hit();
    }
}
