using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    public Text trashScoreText;  // Top-Left Trash Score
    public Text animalScoreText; // Top-Right Animal Rescue Score
    public int trashScore = 0;
    public int animalScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateTrashScore(0);  // Initialize Trash Score
        UpdateAnimalScore(0); // Initialize Animal Rescue Score
    }

    public void UpdateTrashScore(int points)
    {
        trashScore += points;
        trashScoreText.text = "" + trashScore;
    }

    public void UpdateAnimalScore(int points)
    {
        animalScore += points;
        animalScoreText.text = "" + animalScore;
    }
}
