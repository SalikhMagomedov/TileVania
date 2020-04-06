using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSfx;
    [SerializeField] private int amount = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameSession>().AddToScore(amount);
        if (Camera.main != null) AudioSource.PlayClipAtPoint(coinPickUpSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}