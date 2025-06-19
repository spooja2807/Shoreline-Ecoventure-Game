using UnityEngine;
using UnityEngine.UI;

public class AnimalRescue : MonoBehaviour
{
    private bool playerNearby = false;

    public GameObject miniGamePanel; // Mini-game UI Panel
    public Text jumbledWordText;
    public InputField answerInputField;
    public Text feedbackText;
    public Button submitButton;
    public GameObject helpText; // "Press E to Rescue" text
    public GameObject animalObject; // The animal being rescued

    private string correctWord; // Correct answer
    private string[] words = { "LEAF", "LION", "FROG", "BIRD", "TREE" }; // Word list
    private string[] jumbledWords = { "EALF", "NILO", "ROFG", "RDBI", "RETE" }; // Scrambled words

    void Start()
    {
        miniGamePanel.SetActive(false); // Hide mini-game
        helpText.SetActive(false); // Hide "Press E to Rescue"
        submitButton.onClick.AddListener(CheckAnswer);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            helpText.SetActive(true); // Show "Press E to Rescue"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            helpText.SetActive(false); // Hide help text
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            StartMiniGame();
        }
    }

    void StartMiniGame()
    {
        if (miniGamePanel == null || jumbledWordText == null || answerInputField == null)
        {
            Debug.LogError("UI elements are missing in the Inspector!");
            return;
        }

        int index = Random.Range(0, words.Length);
        correctWord = words[index];
        jumbledWordText.text = jumbledWords[index];

        answerInputField.text = "";
        feedbackText.text = "";

        miniGamePanel.SetActive(true);
        helpText.SetActive(false); // Hide "Press E to Rescue"
        Time.timeScale = 0; // Pause game while solving puzzle
    }

    void CheckAnswer()
    {
        if (answerInputField.text.ToUpper() == correctWord)
        {
            feedbackText.text = "✅ Correct! Animal Rescued!";
            Invoke("CloseMiniGame", 2);
        }
        else
        {
            feedbackText.text = "❌ Try Again!";
        }
    }

    void CloseMiniGame()
    {
        miniGamePanel.SetActive(false);
        Time.timeScale = 1; // Resume game

        if (animalObject != null)
        {
            animalObject.SetActive(false); // Remove rescued animal
        }
    }
}
