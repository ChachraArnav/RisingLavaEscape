using UnityEngine;

public class FallingRock : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.TryGameOver();

        if (other.CompareTag("Lava"))
            Destroy(gameObject);
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        float playerY = GameManager.Instance.player.position.y;
        float rockY = transform.position.y;
        float diff = playerY - rockY;

        if (diff > 15f)
            Destroy(gameObject);
    }
}