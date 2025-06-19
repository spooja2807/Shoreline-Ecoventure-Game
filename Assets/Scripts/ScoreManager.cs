using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public Text scoreText;
    public Text errorPrompt; // UI text for error messages

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddWasteScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void DeductWasteScore(int points)
    {
        score -= points;
        UpdateScoreUI();
        ShowErrorPrompt("Wrong Bin! -5 Points", 5f);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "" +score;
    }

    public void ShowSuccessPrompt(string message)
    {
        if (errorPrompt != null)
        {
            errorPrompt.text = message;
            errorPrompt.color = Color.green;
            Debug.Log("Success Message: " + message);
            StartCoroutine(HidePromptAfterDelay(5f));
        }
        else
        {
            Debug.LogError("Error: errorPrompt Text component is not assigned!");
        }
    }

    // Updated method with a duration parameter.
    public void ShowErrorPrompt(string message, float duration)
    {
        if (errorPrompt != null)
        {
            errorPrompt.text = message;
            errorPrompt.color = Color.red;
            Debug.Log("Error Message: " + message);
            StartCoroutine(HidePromptAfterDelay(duration));
        }
        else
        {
            Debug.LogError("Error: errorPrompt Text component is not assigned!");
        }
    }

    private IEnumerator HidePromptAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        errorPrompt.text = "";
    }
}
