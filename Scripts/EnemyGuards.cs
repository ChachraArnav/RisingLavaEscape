using UnityEngine;

public class EnemyGuard : MonoBehaviour
{
    public float speed = 1.5f;
    public float patrolDistance = 1.5f;

    private Vector2 startPos;
    private int direction = 1;
    private SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Patrol left and right on platform
        transform.Translate(Vector2.right * direction *
                            speed * Time.deltaTime);

        // Flip sprite based on direction
        if (sr != null) sr.flipX = direction == -1;

        // Reverse direction at patrol limits
        if (transform.position.x > startPos.x + patrolDistance)
            direction = -1;
        else if (transform.position.x < startPos.x - patrolDistance)
            direction = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.TryGameOver();
    }
}