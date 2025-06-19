using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject miniGamePanel; // Assign MiniGamePanel in the Inspector
    public GameObject rescuedAnimal; // Assign the rescued animal

    private int totalAnimals;
    private int sortedAnimals = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        totalAnimals = GameObject.FindGameObjectsWithTag("Animal").Length;
    }

    public void CheckIfAllAnimalsSorted()
    {
        sortedAnimals++;
        if (sortedAnimals >= totalAnimals)
        {
            Invoke("CompleteMiniGame", 1f);
        }
    }

    private void CompleteMiniGame()
    {
        miniGamePanel.SetActive(false); // Hide mini-game
        Time.timeScale = 1; // Resume main game

        // Make the rescued animal disappear
        if (rescuedAnimal != null)
        {
            Destroy(rescuedAnimal);
        }
    }
}
