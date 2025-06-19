using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager_3 : MonoBehaviour
{
    public static ScoreManager_3 instance;

    public Text wasteScoreText;
    public Text animalScoreText;
    public int wasteScore = 0;
    public int animalScore = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddWasteScore(int amount)
    {
        wasteScore += amount;
        UpdateWasteScoreUI();
    }

    public void DeductWasteScore(int amount)
    {
        wasteScore -= amount;
        if (wasteScore < 0) wasteScore = 0; // Prevent negative score
        UpdateWasteScoreUI();
    }

    public void AddAnimalScore(int amount)
    {
        animalScore += amount;
        UpdateAnimalScoreUI();
    }

    private void UpdateWasteScoreUI()
    {
        wasteScoreText.text = "" + wasteScore;
    }

    private void UpdateAnimalScoreUI()
    {
        animalScoreText.text = "" + animalScore;
    }
}
