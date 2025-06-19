using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
{
    public int targetWasteScore = 15;               // Score needed to win
    public float timeLimit = 60f;                   // Time allowed for level
    public GameObject levelCompleteUI;              // Panel shown on win
    public GameObject levelFailedUI;                // Panel shown on fail
    public GameObject objectivePanel;               // Panel shown before start
    public GameObject gameUI;                       // Main in-game UI panel (new)
    public Text timerText;                          // Text to show timer

    private float timer;
    private bool levelEnded = false;
    private bool gameStarted = false;

    void Start()
    {
        timer = timeLimit;
        Time.timeScale = 1f;

        if (objectivePanel != null)
            objectivePanel.SetActive(true); // Show objectives

        if (gameUI != null)
            gameUI.SetActive(false); // Hide game UI initially

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    void Update()
    {
        if (!gameStarted || levelEnded) return;

        // Timer countdown
        timer -= Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();

        // Win check
        if (ScoreManager.instance.score >= targetWasteScore)
        {
            levelEnded = true;
            Time.timeScale = 0f;

            if (levelCompleteUI != null)
                levelCompleteUI.SetActive(true);

            if (gameUI != null)
                gameUI.SetActive(false); // Hide game UI on completion
        }
        // Fail check
        else if (timer <= 0)
        {
            levelEnded = true;
            Time.timeScale = 0f;

            if (levelFailedUI != null)
                levelFailedUI.SetActive(true);

            if (gameUI != null)
                gameUI.SetActive(false); // Hide game UI on failure
        }
    }

    // Call this from Start Button
    public void StartGame()
    {
        gameStarted = true;

        if (objectivePanel != null)
            objectivePanel.SetActive(false); // Hide objectives

        if (gameUI != null)
            gameUI.SetActive(true); // Show game UI
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        ScoreManager.instance.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNext()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Level3Unlocked", 1);
        ScoreManager.instance.score = 0;
        SceneManager.LoadScene("level3");
    }

    public void BackToSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
