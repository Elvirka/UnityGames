using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject projectile;

    private AttackerSpawner myLineSpawner;
    private Vector2 projectilePos;
    private Animator animator;

    void Start()
    {
        projectilePos = gun.transform.position;
        SetLineSpawner();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAttackerOnLine())
        {
            Debug.Log("Attacker here!!!");
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            Debug.Log("Nobody here))");
            animator.SetBool("IsAttacking", false);
        }
    }

    public void Fire()
    {
        Instantiate(projectile, projectilePos, transform.rotation);
    }

    private void SetLineSpawner()
    {
        var spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (var spawner in spawners)
        {
            bool isOnOneLine = Math.Abs(spawner.transform.position.y - transform.position.y)
                               < Mathf.Epsilon;
            if (isOnOneLine)
            {
                myLineSpawner = spawner;
            }
        }
    }

    private bool IsAttackerOnLine()
    {
        return myLineSpawner.transform.childCount > 0;
    }

}
