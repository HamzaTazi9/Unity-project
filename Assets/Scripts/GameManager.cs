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
    private bool gameStarted = false;
    private bool gameEnded = false;

    void Start()
    {
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

         UpdatePackageCounter();

         UpdateTimerText();
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

        Debug.Log("GAME OVER");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}