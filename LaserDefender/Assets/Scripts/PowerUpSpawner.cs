using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float minTimeBetweenSpawn = 6f;
    [SerializeField] private float maxTimeBetweenSpawn = 15f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            float TimeBetweenSpawn = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
            float y = Camera.main.orthographicSize + 1;
            float x = Random.Range(-4.5f, 4.5f);
            var powerUp = Instantiate(
                powerUpPrefab,
                new Vector2(x, y),
                Quaternion.identity);
            PowerUp script = (PowerUp) powerUp.gameObject.GetComponent(typeof(PowerUp));
            powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -script.GetSpeed());

            yield return new WaitForSeconds(TimeBetweenSpawn);
        }
    }
}
