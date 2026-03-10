using System;
using Settings.Input;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController : MonoBehaviour
{
    public InputReader inputReader;
    private Rigidbody2D rb;

    [Header("Settings")] 
    public float speed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMove;
    }

    private void OnDestroy()
    {
        inputReader.MoveEvent-= OnMove;
    }

    private void OnMove(Vector2 arg0)
    {
        rb.linearVelocity = arg0 * speed;
        Debug.Log(arg0.normalized);
    }
}
