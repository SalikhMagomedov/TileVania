using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Run();
    }

    private void Run()
    {
        var controlThrow = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(controlThrow * runSpeed, rb.velocity.y);
    }
}
