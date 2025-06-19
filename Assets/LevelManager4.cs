using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Level4Manager : MonoBehaviour
{
    public int targetTotalScore = 90;
    public float timeLimit = 90f;

    public GameObject levelCompleteUI;
    public GameObject levelFailedUI;
    public GameObject objectivePanel;
    public GameObject gameUI;
    public GameObject minigameUI;
    public Text timerText;

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

        int totalScore = Game_Manager.Instance.trashScore + Game_Manager.Instance.animalScore;

        if (totalScore >= targetTotalScore)
        {
            levelEnded = true;
            Time.timeScale = 0f;
            if(minigameUI != null)
                minigameUI.SetActive(false);
            if (gameUI != null)
                gameUI.SetActive(false); // Hide game UI on completion
            levelCompleteUI.SetActive(true);
        }
        else if (timer <= 0)
        {
            levelEnded = true;
            Time.timeScale = 0f;
            if (minigameUI != null)
                minigameUI.SetActive(false);
            if (gameUI != null)
                gameUI.SetActive(false); // Hide game UI on failure
            levelFailedUI.SetActive(true);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        if (objectivePanel != null)
            objectivePanel.SetActive(false);
    }

    public void LoadNext()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Level5Unlocked", 1);
        Game_Manager.Instance.trashScore = 0;
        Game_Manager.Instance.animalScore = 0;
        SceneManager.LoadScene("lastlevel");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        Game_Manager.Instance.trashScore = 0;
        Game_Manager.Instance.animalScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
