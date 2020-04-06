using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSfx;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Camera.main != null) AudioSource.PlayClipAtPoint(coinPickUpSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
