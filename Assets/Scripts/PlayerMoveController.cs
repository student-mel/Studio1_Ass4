using System;
using Settings.Input;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveController : MonoBehaviour
{
    public InputReader inputReader;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Settings")] 
    public float speed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        //Debug.Log(arg0.normalized);
        animator.SetFloat("AbsMove", rb.linearVelocity.magnitude);
        if (rb.linearVelocity.x > 0)
            spriteRenderer.flipX = false;
        else if (rb.linearVelocity.x < 0)
            spriteRenderer.flipX = true;
    }
}
