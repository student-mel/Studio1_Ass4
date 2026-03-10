using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    private const string ShootableTag = "Shootable";
    private PlayerShootController shooter;
    private BulletStats stats;

    private Rigidbody2D rb;
    private Vector2 shootDir;
    private Coroutine lifetimeRoutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        lifetimeRoutine = StartCoroutine(DisableAfterLifetime());
    }

    public void AssignBehaviour(PlayerShootController _shooter, BulletStats _stats)
    {
        shooter = _shooter;
        stats =  _stats;
    }

    private void Update()
    {
        rb.linearVelocity = shootDir * stats.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag(ShootableTag))
        {
            gameObject.SetActive(false);
        }*/
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        shooter.EnqueueBullet(this);
    }

    public void SetDir(Vector2 dir)
    {
        shootDir = dir;
    }

    private IEnumerator DisableAfterLifetime()
    {
        yield return new WaitForSeconds(stats.lifetime);
        gameObject.SetActive(false);
    }
}
