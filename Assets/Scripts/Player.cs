using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        var controlThrow = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(controlThrow * runSpeed, rb.velocity.y);
        animator.SetBool("IsRunning", Mathf.Abs(controlThrow) > Mathf.Epsilon);
    }

    private void FlipSprite()
    {
        if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x),
                                               transform.localScale.y,
                                               transform.localScale.z);
        }
    }
}
