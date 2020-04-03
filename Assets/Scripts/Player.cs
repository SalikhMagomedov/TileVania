using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Collider2D _collider2D;
    
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsClimbing = Animator.StringToHash("IsClimbing");

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Run();
        Climb();
        Jump();
        FlipSprite();
    }

    private void Climb()
    {
        if (!_collider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) return;

        var controlThrow = Input.GetAxis("Vertical");
        var climbVelocity = new Vector2(_rb.velocity.x, controlThrow * climbSpeed);
        _rb.velocity = climbVelocity;
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
        if (!_collider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
            
        if (Input.GetButtonDown("Jump"))
        {
            var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            _rb.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        if (!(Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon)) return;
        
        var localScale = transform.localScale;
        localScale = new Vector3(Mathf.Sign(_rb.velocity.x),
            localScale.y,
            localScale.z);
        transform.localScale = localScale;
    }
}
