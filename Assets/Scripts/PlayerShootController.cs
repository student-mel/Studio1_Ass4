using System;
using System.Collections.Generic;
using Settings.Input;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public InputReader inputReader;

    [Header("Projectile Settings")] 
    [SerializeField] private float fireRate = 5;
    [SerializeField] private float bulletSpeed = 2;
    [SerializeField] private Transform shootOrigin;
    
    [Header("Projectile Pooling")]
    public GameObject bulletPrefab;
    [SerializeField] private int poolSize = 30;
    [SerializeField] private Queue<GameObject> bulletPool = new Queue<GameObject>();
    
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
            bulletPool.Enqueue(Instantiate(bulletPrefab));
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

