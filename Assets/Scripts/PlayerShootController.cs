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
    [SerializeField] private Transform shootOrigin;
    
    [Header("Projectile Pooling")]
    public BulletBehaviour bulletPrefab;
    [SerializeField] private int poolSize = 30;
    [SerializeField] private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private bool isShooting = false;
    
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
        CreatePool();
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
        StopAllCoroutines();
    }

    IEnumerator Shoot()
    {
        if (isShooting) yield break;
        isShooting = true;
        
        while (isShooting)
        {
            SpawnBullet();
            yield return new WaitForSeconds(1/bulletStats.fireRate);
        }
    }

    #region Object Pooling

    private void CreatePool()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            BulletBehaviour bullet = Instantiate(bulletPrefab, transform);
            bullet.AssignBehaviour(this, bulletStats);
            bulletPool.Enqueue(bullet.gameObject);
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

