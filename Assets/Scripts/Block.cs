using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private GameObject blockSparklesVFX;

    private Level level;
    private GameSession gameSession;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (gameObject.CompareTag("Breakable"))
        {
            level.CountBreakableBlocks();
        }
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Breakable"))
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        gameSession.AddToScore();
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

