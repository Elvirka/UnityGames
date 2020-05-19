using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private GameObject deathVFX;

    public void Die()
    {
        Destroy(gameObject, 0.1f);
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathVFX();
            Die();
        }
    }

    private void DeathVFX()
    {
        if (!deathVFX) return;
        GameObject vfx =Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(vfx, 1f);
    }
}
