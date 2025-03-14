﻿using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    [SerializeField] private Vector2 deathKick = new Vector2(25f, 25f);

    private Rigidbody2D _rb;
    private Animator _animator;
    private CapsuleCollider2D _collider2D;
    private BoxCollider2D _feet;
    private float _startingGravity;
    private bool _isAlive = true;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsClimbing = Animator.StringToHash("IsClimbing");
    private static readonly int Dying = Animator.StringToHash("Dying");
    private GameSession _gameSession;

    private void Awake()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _feet = GetComponent<BoxCollider2D>();
        _collider2D = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _startingGravity = _rb.gravityScale;
    }

    private void Update()
    {
        if (!_isAlive) return;
        Run();
        Climb();
        Jump();
        FlipSprite();
        Die();
    }

    private void Die()
    {
        if (!_collider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) return;
        _animator.SetTrigger(Dying);
        _rb.velocity = deathKick;
        _isAlive = false;
        _gameSession.ProcessPlayerDeath();
    }

    private void Climb()
    {
        if (!_feet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _animator.SetBool(IsClimbing, false);
            _rb.gravityScale = _startingGravity;
            return;
        }

        var controlThrow = Input.GetAxis("Vertical");
        var climbVelocity = new Vector2(_rb.velocity.x, controlThrow * climbSpeed);
        _rb.velocity = climbVelocity;
        _rb.gravityScale = 0f;
        _animator.SetBool(IsClimbing, Mathf.Abs(controlThrow) > Mathf.Epsilon);
    }

    private void Run()
    {
        var controlThrow = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(controlThrow * runSpeed, _rb.velocity.y);
        _animator.SetBool(IsRunning, Mathf.Abs(controlThrow) > Mathf.Epsilon);
    }

    private void Jump()
    {
        if (!_feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (!Input.GetButtonDown("Jump")) return;
        var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
        _rb.velocity += jumpVelocityToAdd;
    }

    private void FlipSprite()
    {
        if (!(Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon)) return;
        var localScale = transform.localScale;
        localScale = new Vector3(Mathf.Sign(_rb.velocity.x), localScale.y, localScale.z);
        transform.localScale = localScale;
    }
}