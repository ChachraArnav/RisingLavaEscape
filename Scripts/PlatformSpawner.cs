using UnityEngine;
using System.Collections.Generic;


public class PlatformSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject enemyPrefab;
    public GameObject platformPrefab;
    public GameObject powerUpShield;
    public GameObject powerUpJump;
    public GameObject powerUpTimeSlow;
    public Transform player;
    public float minYGap = 1.6f;
    public float maxYGap = 2.2f;
    public float spawnAheadDistance = 8f;
    public float despawnBehindDistance = 12f;

    private float lastSpawnY = -3f;
    private float lastSpawnX = 0f;
    private int direction = 1;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float camHalfW;

    void Start()
    {
        camHalfW = Camera.main.orthographicSize * Camera.main.aspect;
        for (int i = 0; i < 8; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player == null) return;

        while (lastSpawnY < player.position.y + spawnAheadDistance)
        {
            SpawnPlatform();
        }

        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i] == null)
            {
                activePlatforms.RemoveAt(i);
                continue;
            }

            if (activePlatforms[i].transform.position.y < player.position.y - despawnBehindDistance)
            {
                Destroy(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        float xOffset = Random.Range(1.5f, 3.5f);
        float x = Mathf.Clamp(lastSpawnX + direction * xOffset,
                               -camHalfW + 1.2f, camHalfW - 1.2f);
        float y = lastSpawnY + Random.Range(minYGap, maxYGap);
        GameObject p = Instantiate(platformPrefab,
                        new Vector2(x, y), Quaternion.identity);
        activePlatforms.Add(p);
        lastSpawnX = x;
        lastSpawnY = y;
        direction *= -1;


        float spawnRoll = Random.value;

        if (spawnRoll < 0.40f)
        {
            // 35% chance - spawn coin
            Instantiate(coinPrefab,
                new Vector2(x, y + 0.5f),
                Quaternion.identity);
        }
        else if (spawnRoll < 0.50f)
        {
            // 10% chance - spawn power-up
            int rand = Random.Range(0, 3);
            GameObject pu = rand == 0 ? powerUpShield :
                            rand == 1 ? powerUpJump : powerUpTimeSlow;
            Instantiate(pu,
                new Vector2(x, y + 0.6f),
                Quaternion.identity);
        }
        else if (lastSpawnY > 10f && spawnRoll < 0.55f)
        {
            // 10% chance - spawn enemy (only after climbing)
            Instantiate(enemyPrefab,
                new Vector2(x, y + 0.6f),
                Quaternion.identity);
        }
        
    }
}