using UnityEngine;

public class LavaRise : MonoBehaviour
{
    public float baseSpeed = 1.5f;
    public float maxSpeed = 4f;
    public float speedIncreasePerUnit = 0.02f;
    public Transform player;

    private bool slowed;
    private float slowTimer;
    private bool started = false;

    void Update()
    {
        if (player == null) return;

        if (!started) return;

        float height = Mathf.Max(0, player.position.y);
        float speed = Mathf.Min(
            baseSpeed + height * speedIncreasePerUnit, maxSpeed);

        if (slowed)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0) slowed = false;
            speed *= 0.3f;
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Only snap to camera bottom if lava falls too far behind
        Camera cam = Camera.main;
        float camBottom = cam.transform.position.y - cam.orthographicSize;
        float maxBehind = camBottom - 1f;

        if (transform.position.y < maxBehind)
            transform.position = new Vector2(
                transform.position.x, maxBehind);
    }

    public void StartRising()
    {
        started = true;
    }

    public void ApplySlow(float duration)
    {
        slowed = true;
        slowTimer = duration;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.TryGameOver();
    }
}