using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    public GameObject pausePanel;
    public Slider volumeSlider;

    public TMP_Text packageCounterText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverTimeText;

    public TMP_Text winScoreText;
    public TMP_Text winTimeText;

    public int totalPackages = 3;

    private int deliveredPackages = 0;

    public TMP_Text timerText;
    public float timeLeft = 60f;
    private float startingTime;

    public TMP_Text scoreText;

    public TMP_Text highScoreText;

    public GameObject packageFeedbackText;

    private bool gameStarted = false;
    private bool gameEnded = false;
    private bool isPaused = false;

    private int score = 100;
    public int pointsPerPackage = 100;

    // Zineb : Obstacle Penalty 
    public int obstaclePenalty = 10;
    public int movingObstaclePenalty = 25;
    public TMP_Text collisionFeedbackText;
    // end: Obstacle Penalty 



    private int highScore = 0;

    void Start()
    {
        Time.timeScale = 0f;

        startingTime = timeLeft;

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }

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

        ShowPackageFeedback();

        Debug.Log("Package delivered: " + deliveredPackages + "/" + totalPackages);

        if (deliveredPackages >= totalPackages)
        {
            WinGame();
        }
    }

    void UpdatePackageCounter()
    {
        if (packageCounterText != null)
        {
            packageCounterText.text = "Packages: " + deliveredPackages + "/" + totalPackages;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted && !gameEnded)
        {
            TogglePause();
        }

        if (!gameStarted || gameEnded || isPaused)
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
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
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
            gameOverScoreText.text = "Score: " + score;
        }

        if (gameOverTimeText != null)
        {
            float timeUsed = startingTime - timeLeft;
            int minutes = Mathf.FloorToInt(timeUsed / 60f);
            int seconds = Mathf.FloorToInt(timeUsed % 60f);

            gameOverTimeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
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

    public void WinGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;
        Time.timeScale = 0f;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        if (winScoreText != null)
        {
            winScoreText.text = "Score: " + score;
        }

        if (winTimeText != null)
        {
            float timeUsed = startingTime - timeLeft;
            int minutes = Mathf.FloorToInt(timeUsed / 60f);
            int seconds = Mathf.FloorToInt(timeUsed % 60f);

            winTimeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateHighScoreText();

        Debug.Log("YOU WIN");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowPackageFeedback()
{
    StartCoroutine(ShowFeedbackCoroutine());
}

IEnumerator ShowFeedbackCoroutine()
{
    packageFeedbackText.SetActive(true);

    yield return new WaitForSeconds(1f);

    packageFeedbackText.SetActive(false);
}

// Zineb : Penalty Obstacle 
public void HitObstacle()
{
    score -= obstaclePenalty;

    if (score < 0)
    {
         score = 0;
         UpdateScoreText();
        ShowCollisionFeedback("-10 points");
        GameOver();
        return;
        
    }

    UpdateScoreText();
    ShowCollisionFeedback("-10 points");
    Debug.Log("Obstacle hit! -10 points");
    
}

public void HitMovingObstacle()
{
    score -= movingObstaclePenalty;

    if (score < 0)
    {
        score = 0;
        UpdateScoreText();
        ShowCollisionFeedback("-25 points");
        GameOver();
        return;
       
    }

    UpdateScoreText();
    ShowCollisionFeedback("-25 points");
    Debug.Log("Traffic collision! -25 points");
    
    
}

public void ShowCollisionFeedback(string message)
{
    StopCoroutine(nameof(ShowCollisionFeedbackCoroutine));
    StartCoroutine(ShowCollisionFeedbackCoroutine(message));
}

IEnumerator ShowCollisionFeedbackCoroutine(string message)
{
    collisionFeedbackText.gameObject.SetActive(true);
    collisionFeedbackText.text = message;

    yield return new WaitForSeconds(1f);

    collisionFeedbackText.gameObject.SetActive(false);
}
// end penalty Obstacle 

}

