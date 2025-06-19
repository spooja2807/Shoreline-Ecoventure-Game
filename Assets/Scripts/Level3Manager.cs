using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Level3Manager : MonoBehaviour
{
    public float timeLimit = 60f;

    public GameObject levelCompleteUI;
    public GameObject levelFailedUI;
    public GameObject objectivePanel;
    public Text timerText;
    public int targetTotalScore = 90;
    private float timer;
    private bool levelEnded = false;
    private bool gameStarted = false;

    void Start()
    {
        timer = timeLimit;
        Time.timeScale = 1f;

        if (objectivePanel != null)
            objectivePanel.SetActive(true);

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    void Update()
    {
        if (!gameStarted || levelEnded) return;

        timer -= Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();
        int totalScore = ScoreManager_3.instance.wasteScore + ScoreManager_3.instance.animalScore;

        // Win condition
        if (totalScore >= targetTotalScore)
        {
            levelEnded = true;
            Time.timeScale = 0f;
            levelCompleteUI.SetActive(true);
        }

        // Fail condition
        else if (timer <= 0)
        {
            levelEnded = true;
            Time.timeScale = 0f;
            levelFailedUI.SetActive(true);
        }
    }

    // Start the game (called by Start button)
    public void StartGame()
    {
        gameStarted = true;

        if (objectivePanel != null)
            objectivePanel.SetActive(false);
    }

    // Retry current level
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        ScoreManager_3.instance.wasteScore = 0;
        ScoreManager_3.instance.animalScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Load next level
    public void LoadNext()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Level4Unlocked", 1);
        ScoreManager_3.instance.wasteScore = 0;
        ScoreManager_3.instance.animalScore = 0;
        SceneManager.LoadScene("level4");
    }

    // Back to level select
    public void BackToSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }

    // Back to main menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
