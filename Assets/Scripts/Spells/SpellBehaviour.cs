using System;
using NUnit.Framework.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpellBehaviour : MonoBehaviour
{
    private SpellStats spellStats;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(GetRandSpawnVel(), ForceMode2D.Impulse);
        rb.gravityScale = spellStats.fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(spellStats.EffectCoroutine(other.transform));
        }
    }
    
    public void ApplyStats(SpellStats stats)
    {
        spellStats = stats;
    }

    private Vector2 GetRandSpawnVel()
    {
        Vector2 spawnDir = Vector2.up;
        spawnDir.x = Random.Range(-1, 1f);
        spawnDir *= Random.Range(1, 2f);
        return spawnDir;
    }
}
