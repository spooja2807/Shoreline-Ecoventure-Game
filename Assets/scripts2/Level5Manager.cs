using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Level5Manager : MonoBehaviour
{
    public int targetScore = 60;
    public float timeLimit = 50f;

    public GameObject levelCompleteUI;
    public GameObject levelFailedUI;
    public GameObject objectivePanel;
    public GameObject gameUI;
    public Text timerText;

    [HideInInspector] public bool levelEnded = false;
    private bool gameStarted = false;
    private float timer;

    [Header("Trash Manager Reference")]
    public TrashMg trashManager; // <-- Reference to TrashMg

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

        if (timer <= 0f)
        {
            levelEnded = true;
            Time.timeScale = 0f;

            if (gameUI != null)
                gameUI.SetActive(false);

            if (levelFailedUI != null)
                levelFailedUI.SetActive(true);
        }
    }

    public void CheckForLevelCompletion(int currentScore)
    {
        if (!levelEnded && currentScore >= targetScore)
        {
            levelEnded = true;
            Time.timeScale = 0f;

            if (gameUI != null)
                gameUI.SetActive(false);

            if (levelCompleteUI != null)
                levelCompleteUI.SetActive(true);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        if (objectivePanel != null)
            objectivePanel.SetActive(false);
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;

        // If TrashMg is referenced, reset its score
        if (trashManager != null)
        {
            trashManager.trashScore = 0;
        }

        SceneManager.LoadScene("lastlevel");
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
