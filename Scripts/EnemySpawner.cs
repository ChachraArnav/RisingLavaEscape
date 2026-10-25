using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnInterval = 5f;
    public float spawnAbovePlayer = 6f;

    private float timer;

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-2f, 2f);
        float y = player.position.y + spawnAbovePlayer;
        Instantiate(enemyPrefab, new Vector2(x, y),
                    Quaternion.identity);
    }
}