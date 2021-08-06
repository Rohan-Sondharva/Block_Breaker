using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // config params
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject blockVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;

    // state vars
    [SerializeField] int timesHits;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHits++;
        int maxHits = hitSprites.Length + 1;
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
            Debug.LogError("Block Sprite is missing from Array : " + gameObject.name);
        }
    }

    public void DestroyBlock()
    {
        PlaySFX();
        FindObjectOfType<GameSession>().AddToScore();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerVFX();
    }

    public void TriggerVFX()
    {
        GameObject sparkles = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }

    public void PlaySFX()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
