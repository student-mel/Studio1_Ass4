using System;
using System.Collections;
using System.Collections.Generic;
using Settings.Input;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public InputReader inputReader;

    [Header("Projectile Settings")] 
    [SerializeField] private BulletStats bulletStats;
    [SerializeField] public Transform shootOrigin;
    private Vector2 shootDir;
    
    [Header("Projectile Pooling")]
    public BulletBehaviour bulletPrefab;
    [SerializeField] private int poolSize = 30;
    private Queue<BulletBehaviour> bulletPool = new Queue<BulletBehaviour>();
    private bool isShooting = false;
    private float nextFireTime = 0f;
    
    [Header("Debug")]
    public bool debug;

    void Awake()
    {
        // unparent from player
        transform.parent =  null;
    }

    private void OnEnable()
    {
        inputReader.ShootStartedEvent += ShootEvent;
        inputReader.ShootStoppedEvent += ShootStopEvent;
        inputReader.MoveEvent += MoveEvent;
        CreatePool();
        
        shootDir = Vector2.up;;
    }

    private void OnDisable()
    {
        inputReader.ShootStartedEvent -= ShootEvent;
        inputReader.ShootStoppedEvent -= ShootStopEvent;
        DestroyPool();
    }
    
    private void ShootEvent()
    {
        StartCoroutine(Shoot());
    }
    
    private void ShootStopEvent()
    {
        isShooting = false;
    }
    
    private void MoveEvent(Vector2 arg0)
    {
        shootDir = arg0.x switch
        {
            < 0 => new Vector2(1, 1).normalized,
            > 0 => new Vector2(-1, 1).normalized,
            _ => Vector2.up
        };
    }

    IEnumerator Shoot()
    {
        if (isShooting) yield break;
        isShooting = true;
        
        while (isShooting)
        {
            if (Time.time >= nextFireTime)
            {
                SpawnBullet();
                nextFireTime = Time.time + (1f / bulletStats.fireRate);
            }

            yield return null;
        }
    }

    #region Object Pooling

    private void CreatePool()
    {
        bulletPool = new Queue<BulletBehaviour>();
        for (int i = 0; i < poolSize; i++)
        {
            BulletBehaviour bullet = Instantiate(bulletPrefab, transform);
            bullet.AssignBehaviour(this, bulletStats);
            bulletPool.Enqueue(bullet);
        }
    }

    private void DestroyPool()
    {
        for (int i = poolSize - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void SpawnBullet()
    {
        BulletBehaviour bullet = bulletPool.Dequeue();
        bullet.SetDir(shootDir);
        bullet.transform.position = shootOrigin.position;
        bullet.gameObject.SetActive(true);
    }

    public void EnqueueBullet(BulletBehaviour bullet)
    {
        bulletPool.Enqueue(bullet);
    }
    #endregion
  
    private void DebugMessage(string message)
    {
        if (debug)
            Debug.Log(message);
    }
}

