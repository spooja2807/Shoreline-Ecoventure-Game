using UnityEngine;

public class GameManagerGlobal : MonoBehaviour
{
    public static GameManagerGlobal Instance;

    public int wasteScore = 0;
    public int animalScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    public void ResetScores()
    {
        wasteScore = 0;
        animalScore = 0;
    }
}
