using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    
    public List<Transform> GetWaypoints()
    {
        //GameObject path0 = PrefabUtility.LoadPrefabContents("Assets/Paths/Path(0).prefab");
        //waypoints = path0.GetComponentsInChildren<Transform>(true)
        //    .Where(c => !c.CompareTag("Path")).ToList();
        List<Transform> waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    
    public float GetSpawnRandomFactor() { return Random.Range(-0.3f, 0.3f); }
    
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    
    public float GetMoveSpeed() { return moveSpeed; }
}

