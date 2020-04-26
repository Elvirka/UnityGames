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
        level.CountBreakableBlocks();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, 0.1f);
        Destroy(gameObject);
        level.CountDestroyedBlocks();
        gameSession.AddToScore();
    }

    private void TriggerSparklesVFX()
    {
        
    }
}

