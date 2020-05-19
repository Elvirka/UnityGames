using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] private Attacker attackerPrefab;
    [SerializeField] private float minTimeBetweenSpawn = 1f;
    [SerializeField] private float maxTimeBetweenSpawn = 5f;

    [SerializeField] private bool spawn = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(WaitForNewAttacker());
    }

    private IEnumerator WaitForNewAttacker()
    {
        while (spawn)
        {
            float timeBetweenSpawn = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
            yield return new WaitForSeconds(timeBetweenSpawn);
            
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        Attacker newAttacker = Instantiate(attackerPrefab, transform.position, transform.rotation);
        newAttacker.transform.parent = transform;
    }
}


