using System;
using Settings.Input;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public InputReader inputReader;

    [Header("Shoot Settings")] 
    public GameObject bulletPrefab;
    [SerializeField] private float fireRate = 5, projectileSpeed = 2;
    [SerializeField] private Transform shootOrigin;
    
    [Header("Debug")]
    public bool debug;
    
    private void OnEnable()
    {
        inputReader.ShootEvent += ShootEvent;
    }

    private void OnDisable()
    {
        inputReader.ShootEvent -= ShootEvent;
    }
    
    private void ShootEvent()
    {
        Instantiate(bulletPrefab, shootOrigin.position, Quaternion.identity, shootOrigin);
    }

    private void DebugMessage(string message)
    {
        if (debug)
            Debug.Log(message);
    }
}

