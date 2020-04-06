using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per seconds")]
    [SerializeField] private float scrollRate = .2f;

    private void Update()
    {
        transform.Translate(new Vector2(0f, scrollRate * Time.deltaTime));
    }
}
