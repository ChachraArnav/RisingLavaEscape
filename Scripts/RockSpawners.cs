using UnityEngine;
using TMPro;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform player;
    public TextMeshProUGUI warningText;
    public float eventInterval = 20f;
    public float showerDuration = 5f;
    public float rockSpawnRate = 0.4f;

    private float eventTimer = 0f;
    private float countdownTimer = 3f;
    private float showerTimer = 0f;
    private float rockTimer = 0f;
    private enum State { Waiting, Countdown, Showering }
    private State state = State.Waiting;

    void Update()
    {
        if (player == null) return;

        switch (state)
        {
            case State.Waiting:
                eventTimer += Time.deltaTime;
                if (warningText) warningText.text = "";
                if (eventTimer >= eventInterval)
                {
                    eventTimer = 0f;
                    countdownTimer = 3f;
                    state = State.Countdown;
                }
                break;

            case State.Countdown:
                countdownTimer -= Time.deltaTime;
                int count = Mathf.CeilToInt(countdownTimer);
                if (warningText)
                    warningText.text = count > 0 ?
                        "Rock Shower in " + count + "!" : "INCOMING!";
                if (countdownTimer <= 0)
                {
                    showerTimer = showerDuration;
                    state = State.Showering;
                }
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayRockShower();
                break;

            case State.Showering:
                showerTimer -= Time.deltaTime;
                rockTimer -= Time.deltaTime;
                if (warningText) warningText.text = "ROCK SHOWER!";

                if (rockTimer <= 0)
                {
                    rockTimer = rockSpawnRate;
                    SpawnRock();
                }

                if (showerTimer <= 0)
                {
                    if (warningText) warningText.text = "";
                    state = State.Waiting;
                }
                break;
        }
    }

    void SpawnRock()
    {
        float x = Random.Range(-3f, 3f);
        float y = player.position.y + 8f;
        Instantiate(rockPrefab, new Vector2(x, y),
                    Quaternion.identity);
    }
}