using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;

public TMP_Text packageCounterText;
    public TMP_Text gameOverScoreText;

    public int totalPackages = 3;

    private int deliveredPackages = 0;

    public TMP_Text timerText;
public float timeLeft = 60f;

public TMP_Text scoreText;

public TMP_Text highScoreText;
    private bool gameStarted = false;
    private bool gameEnded = false;

    private int score = 0;
public int pointsPerPackage = 100;

private int highScore = 0;

    void Start()
    {
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

         UpdatePackageCounter();

         UpdateTimerText();

         UpdateScoreText();

         highScore = PlayerPrefs.GetInt("HighScore", 0);
UpdateHighScoreText();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gameStarted = true;
        Time.timeScale = 1f;
    }

    public void DeliverPackage()
    {
         if (!gameStarted || gameEnded)
    {
        return;
    }

    deliveredPackages++;

    score += pointsPerPackage;
UpdateScoreText();

    UpdatePackageCounter();

    Debug.Log("Package delivered: " + deliveredPackages + "/" + totalPackages);
    }

    void UpdatePackageCounter()
{
    packageCounterText.text = "Packages: " + deliveredPackages + "/" + totalPackages;
}

void Update()
{
    if (!gameStarted || gameEnded)
    {
        return;
    }

    timeLeft -= Time.deltaTime;

    if (timeLeft <= 0)
    {
        timeLeft = 0;
        GameOver();
    }

    UpdateTimerText();
}

void UpdateTimerText()
{
    timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
}

void UpdateScoreText()
{
    scoreText.text = "Score: " + score;
}

void UpdateHighScoreText()
{
    highScoreText.text = "High Score: " + highScore;
}

    public void GameOver()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;
        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);

        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = "Packages delivered: " + deliveredPackages + "/" + totalPackages;
        }

        if (score > highScore)
{
    highScore = score;
    PlayerPrefs.SetInt("HighScore", highScore);
    PlayerPrefs.Save();
}

UpdateHighScoreText();

        Debug.Log("GAME OVER");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}