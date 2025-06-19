using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    [Header("Score Settings")]
    public int targetWasteScore = 10;

    [Header("Timer Settings")]
    public float levelTime = 60f;
    private float currentTime;
    public Text timerText;

    [Header("UI Panels")]
    public GameObject levelCompleteUI;
    public GameObject levelFailedUI;
    public GameObject objectivePanel;     // Panel that shows objectives
    public GameObject gameplayUI;         // Score/Timer display panel

    private bool levelStarted = false;
    private bool levelEnded = false;

    void Start()
    {
        Time.timeScale = 0f; // Pause at start until "Start" is pressed
        currentTime = levelTime;

        // Only show objective at beginning
        objectivePanel.SetActive(true);
        gameplayUI.SetActive(false);
        levelCompleteUI.SetActive(false);
        levelFailedUI.SetActive(false);
    }

    void Update()
    {
        if (!levelStarted || levelEnded) return;

        // TIMER COUNTDOWN
        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0f)
        {
            TriggerLevelFailed();
        }

        // LEVEL COMPLETE CHECK
        if (ScoreManager.instance.score >= targetWasteScore)
        {
            TriggerLevelComplete();
        }
    }

    // Called when Start button is clicked in objective panel
    public void StartLevel()
    {
        levelStarted = true;
        Time.timeScale = 1f;

        objectivePanel.SetActive(false);
        gameplayUI.SetActive(true);
    }

    void TriggerLevelComplete()
    {
        if (levelEnded) return;

        levelEnded = true;
        Time.timeScale = 0f;
        gameplayUI.SetActive(false);
        levelCompleteUI.SetActive(true);
    }

    void TriggerLevelFailed()
    {
        if (levelEnded) return;

        levelEnded = true;
        Time.timeScale = 0f;
        gameplayUI.SetActive(false);
        levelFailedUI.SetActive(true);
    }

    // Retry current level
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        ScoreManager.instance.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load next level
    public void LoadNext()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Level2Unlocked", 1);
        ScoreManager.instance.score = 0;
        SceneManager.LoadScene("level2");
    }

    // Return to level selection
    public void BackToSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }
    // Go back to the main menu scene
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");  // Replace with your main menu scene name
    }

}
