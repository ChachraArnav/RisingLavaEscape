using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public void ShowScore(int score, int height)
    {
        if (finalScoreText)
            finalScoreText.text = "Score: " + score + "\n" + height + "m reached";

        int best = PlayerPrefs.GetInt("HighScore", 0);
        if (score > best)
        {
            PlayerPrefs.SetInt("HighScore", score);
            if (highScoreText)
                highScoreText.text = "NEW BEST: " + score + "!";
        }
        else
        {
            if (highScoreText)
                highScoreText.text = "Best: " + best;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}