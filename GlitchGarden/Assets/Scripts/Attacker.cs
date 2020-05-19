using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    void Update()
    {
       transform.Translate(Vector2.left * speed * Time.deltaTime); 
    }

    private void SetMovementSpeed(float speed)
    {
        this.speed = speed;
    }
}