using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D _rb;

    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void Update() => _rb.velocity = new Vector2(IsFacingRight() ? moveSpeed : -moveSpeed, 0f);

    private bool IsFacingRight() => transform.localScale.x > 0;

    private void OnTriggerExit2D(Collider2D other) =>
        transform.localScale = new Vector2(-(Mathf.Sign(_rb.velocity.x)), 1f);
}