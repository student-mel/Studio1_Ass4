using System;
using System.Collections.Generic;
using Settings.Input;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public InputReader inputReader;

    [Header("Projectile Settings")] 
    [SerializeField] private BulletStats bulletStats;
    [SerializeField] private Transform shootOrigin;
    
    [Header("Projectile Pooling")]
    public BulletBehaviour bulletPrefab;
    [SerializeField] private int poolSize = 30;
    [SerializeField] private readonly Queue<GameObject> bulletPool = new Queue<GameObject>();
    
    [Header("Debug")]
    public bool debug;

    private void OnEnable()
    {
        inputReader.ShootEvent += ShootEvent;
        CreatePool();
    }

    private void OnDisable()
    {
        inputReader.ShootEvent -= ShootEvent;
    }
    
    private void ShootEvent()
    {
        SpawnBullet();
    }

    #region Object Pooling

    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            BulletBehaviour bullet = Instantiate(bulletPrefab, transform);
            bullet.AssignBehaviour(this, bulletStats);
            bulletPool.Enqueue(bullet.gameObject);
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = bulletPool.Dequeue();
        bullet.transform.position = shootOrigin.position;
        bullet.SetActive(true);
    }

    public void EnqueueBullet(GameObject bullet)
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

