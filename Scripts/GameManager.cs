using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI heightText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI powerUpText;
    public GameObject deathEffect;
   
    private float startY;
    private int coins;
    private float maxHeight;
    private bool isGameOver;
    private bool hasShield;

    void Awake() { Instance = this; }

    void Start()
    {
        startY = player.position.y;
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameMusic();
    }

    void Update()
    {
        if (isGameOver) return;
        float height = Mathf.Max(0, player.position.y - startY);
        if (height > maxHeight) maxHeight = height;
        int score = Mathf.FloorToInt(maxHeight) + coins * 5;
        if (scoreText) scoreText.text = "Score: " + score;
        if (heightText) heightText.text = Mathf.FloorToInt(maxHeight) + "m";
        if (coinsText) coinsText.text = "Coins: " + coins;
    }

    public void AddCoin() { coins++; }

    public void ActivateShield(float duration)
    {
        hasShield = true;
        Invoke(nameof(RemoveShield), duration);
    }

    void RemoveShield() { hasShield = false; }

    public void TryGameOver()
    {
        if (hasShield) { RemoveShield(); return; }
        GameOver();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameOver();
        if (gameOverPanel) gameOverPanel.SetActive(true);
        var screen = gameOverPanel.GetComponent<GameOverScreen>();
        if (screen) screen.ShowScore(
            Mathf.FloorToInt(maxHeight) + coins * 5,
            Mathf.FloorToInt(maxHeight));
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb) rb.simulated = false;
        Time.timeScale = 0f;
        if (deathEffect)
            Instantiate(deathEffect, player.position,
                        Quaternion.identity);
    }
    public void ShowPowerUpText(string message, float duration)
    {
        if (powerUpText)
        {
            StopCoroutine("PowerUpTextTimer");
            StartCoroutine(PowerUpTextTimer(message, duration));
        }
    }

    System.Collections.IEnumerator PowerUpTextTimer(
        string message, float duration)
    {
        float timer = duration;
        while (timer > 0)
        {
            powerUpText.text = message + ": " +
                               Mathf.CeilToInt(timer) + "s";
            timer -= Time.deltaTime;
            yield return null;
        }
        powerUpText.text = "";
    }
}