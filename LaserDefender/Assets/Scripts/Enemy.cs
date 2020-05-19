using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 300;
    [SerializeField] private float explosionDuration = 10f;
    [SerializeField] private int scoreValue = 150;
    
    
    [Header("Laser")]
    [SerializeField] private float laserSpeed = 8f;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;

    [Header("Prefabs")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject laserPrefab;
    
    [Header("Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField][Range(0f,1f)] private float deathSoundVolume = 0.1f;
    [SerializeField][Range(0f,1f)] private float shootSoundVolume = 0.05f;

    private float timeBetweenShots;
    private AudioSource fireAudioSource;
    private GameSession gameSession;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = GetRandomShotCounter();
        fireAudioSource = GetComponent<AudioSource>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        timeBetweenShots -= Time.deltaTime;
        if (timeBetweenShots <= 0)
        {
            Fire();
            timeBetweenShots = GetRandomShotCounter();
        }
    }

    private float GetRandomShotCounter()
    {
        return Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            laserPrefab,
            transform.position,
            Quaternion.identity
        );
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        PlayFireSFX();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.AddToScore(scoreValue);
        PlatyDestroyVFX();
        PlayDestroySFX();
        Destroy(gameObject, 0.05f);
    }

    private void PlatyDestroyVFX()
    {
        GameObject explosion = Instantiate(
            explosionPrefab,
            transform.position,
            transform.rotation
        ); 
        Destroy(explosion, explosionDuration);
    }
    
    private void PlayDestroySFX()
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
    
    private void PlayFireSFX()
    {
        fireAudioSource.PlayOneShot(shootSound, shootSoundVolume);
    }
}
