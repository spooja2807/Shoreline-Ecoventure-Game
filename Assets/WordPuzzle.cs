using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WordPuzzle : MonoBehaviour
{
    public TextMeshProUGUI headingText;
    public TextMeshProUGUI jumbledWordText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI rescuePromptText; // Rescue prompt text
    public Button submitButton;
    public GameObject miniGameUI;

    private string[] words = { "LEAF", "TREE", "FLOWER", "BIRD", "FISH", "SEA", "SHELL" };
    private string[] jumbledWords = { "ELAF", "ERET", "WEROLF", "RIBD", "ISFH", "EAS", "LSELH" };
    private HashSet<int> remainingIndices = new HashSet<int>();

    private string currentAnswer;
    private GameObject currentAnimal;

    void Start()
    {
        miniGameUI.SetActive(false);
        submitButton.onClick.AddListener(CheckAnswer);

        for (int i = 0; i < words.Length; i++)
        {
            remainingIndices.Add(i);
        }

        if (headingText != null)
        {
            headingText.text = "UNSCRAMBLE THE WORD!";
            headingText.fontSize = 60;
            headingText.color = new Color(0.2f, 0.6f, 1f);
            headingText.fontStyle = FontStyles.Bold;
        }

        // Hide rescue prompt at start
        if (rescuePromptText != null)
        {
            rescuePromptText.text = ""; // Clear any default text
            rescuePromptText.gameObject.SetActive(false);
        }
    }

    public void StartWordPuzzle(GameObject animal, int wordIndex)
    {
        if (!remainingIndices.Contains(wordIndex))
        {
            Debug.Log("This animal has already been rescued.");
            return;
        }

        currentAnimal = animal;
        jumbledWordText.text = jumbledWords[wordIndex];
        currentAnswer = words[wordIndex];
        remainingIndices.Remove(wordIndex);

        answerInput.text = "";
        feedbackText.text = "";
        feedbackText.gameObject.SetActive(true);
        miniGameUI.SetActive(true);
    }

    public void ShowRescuePrompt(bool show)
    {
        if (rescuePromptText != null)
        {
            rescuePromptText.text = show ? "Press 'S' to Rescue!" : "";
            rescuePromptText.gameObject.SetActive(show);
        }
    }

    void CheckAnswer()
    {
        string userInput = answerInput.text.ToUpper().Trim();
        Debug.Log($"Checking answer... Input: {userInput}, Expected: {currentAnswer}");

        if (userInput == currentAnswer)
        {
            feedbackText.text = "Well Done! The Animal is Safe & Sound!";
            feedbackText.color = Color.green;

            Game_Manager.Instance.UpdateAnimalScore(20);

            StartCoroutine(ShowFeedbackAndHideUI());
        }
        else
        {
            feedbackText.text = "Try Again!";
            feedbackText.color = Color.red;
            feedbackText.gameObject.SetActive(true);
        }
    }

    IEnumerator ShowFeedbackAndHideUI()
    {
        yield return new WaitForSeconds(1.5f);
        miniGameUI.SetActive(false);

        if (currentAnimal != null)
        {
            currentAnimal.SetActive(false);
            Debug.Log("Animal rescued and hidden.");
            currentAnimal = null;
        }
    }
}
