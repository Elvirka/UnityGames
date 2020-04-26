using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;

    private Level level;
    private GameSession gameSession;
    private int maxHits;
    private int timesHits;

    private void Start()
    {
        CountBreakableBlocks();
        gameSession = FindObjectOfType<GameSession>();
        maxHits = hitSprites.Length + 1;
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (gameObject.CompareTag("Breakable"))
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHits++;
        if (timesHits >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHits - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];   
        }
        else
        {
            Debug.LogError($"Block {name} sprite is missing from array");
        }
        
    }

    private void DestroyBlock()
    {
        gameSession.AddToScore(timesHits);
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.CountDestroyedBlocks();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, 0.1f);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}

