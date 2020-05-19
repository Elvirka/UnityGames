using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float firingPeriod = 0.3f;
    [SerializeField] private int health = 2000;
    
    [Header("Damage Component")]
    [SerializeField] private RectTransform healthChangeText;
    [SerializeField] private float healthChangeTextDuration = 1f;
    
    [Header("Laser")]
    [SerializeField] private float laserSpeed = 20f;
    [SerializeField] private GameObject laserPrefab;
    
    [Header("Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField][Range(0,1)] private float deathSoundVolume = 0.1f;
    [SerializeField][Range(0,1)] private float shootSoundVolume = 0.05f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private IEnumerator coroutine;
    private AudioSource fireAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        fireAudioSource = GetComponent<AudioSource>();
        SetUpMoveBoundaries();
        InitDamageText();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    public int GetHealth()
    {
        return health;
    }

    private void InitDamageText()
    {
        healthChangeText.gameObject.SetActive(false);
    }

    private IEnumerator ShowHealthChangeText(int healthChange, bool positive)
    {
        var textMesh = healthChangeText.GetComponentInChildren<TextMeshProUGUI>(true);
        if (positive)
        {
            textMesh.color = Color.green;
            textMesh.text = "+" + healthChange;
        }
        else
        {
            textMesh.color = Color.red;
            textMesh.text = "-" + healthChange;
        }
        healthChangeText.position = new Vector2(transform.position.x + 1, transform.position.y + 0.5f);
        healthChangeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(healthChangeTextDuration);
        healthChangeText.gameObject.SetActive(false);
        
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        var renderer = GetComponent<SpriteRenderer>();
        float halfPlayerWidth = renderer.bounds.size.x / 2;
        float halfPlayerHeight = renderer.bounds.size.y / 2;
        
        Camera gameCamera = Camera.main;
        Debug.Log($"{halfPlayerWidth}, {halfPlayerHeight}");
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfPlayerWidth;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfPlayerWidth;
        
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + halfPlayerHeight;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - halfPlayerHeight;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (coroutine is null)
            {
                coroutine = FireContinuously();
            }
            StartCoroutine(coroutine);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(coroutine);
        }
    }

    private void MakeOneShot()
    {
        GameObject laser = Instantiate(
            laserPrefab,
            transform.position,
            Quaternion.identity
        );
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
        PlayFireSFX();
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            MakeOneShot();
            yield return new WaitForSeconds(firingPeriod);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
        if (damageDealer)
        {
            ProcessHit(damageDealer);
        }
        else if(powerUp)
        {
            health += powerUp.GetPowerQuantity();
            StartCoroutine(ShowHealthChangeText(
                powerUp.GetPowerQuantity(), true)
            );
            powerUp.Hit();
        }
        
    }
    
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        StartCoroutine(ShowHealthChangeText(
            damageDealer.GetDamage(), false)
        );
        if (health <= 0f)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject, 0.05f);
        FindObjectOfType<Level>().LoadGameOver();
        PlayDestroySFX();
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
