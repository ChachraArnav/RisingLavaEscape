using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMenuMusic();

        if (highScoreText)
            highScoreText.text = "Best: " +
                                 PlayerPrefs.GetInt("HighScore", 0);
    }

    public void StartGame()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameMusic();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}