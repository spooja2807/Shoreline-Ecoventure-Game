using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimalHelp : MonoBehaviour
{
    [Header("UI Elements")]
    public Text helpText;           // e.g., "Press E to help"
    public GameObject mathPuzzleUI; // The math puzzle panel
    public Text resultText;         // The rescue message ("Animal Rescued")

    [Header("Movement Settings")]
    public float moveSpeed = 2f;    // Speed at which the animal moves left after rescue

    private bool playerNearby = false;
    private Rigidbody2D rb;

    // Static variable to hold the animal the player is interacting with.
    public static AnimalHelp currentAnimal = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Freeze animal movement initially so it blocks the player.
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Hide UI elements at start.
        helpText.gameObject.SetActive(false);
        mathPuzzleUI.SetActive(false);
        resultText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            helpText.gameObject.SetActive(true);
            // Set the currentAnimal to this instance.
            currentAnimal = this;
            // Stop the player's movement.
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            helpText.gameObject.SetActive(false);
            // If this animal was the current one, clear the reference.
            if (currentAnimal == this)
            {
                currentAnimal = null;
            }
        }
    }

    void Update()
    {
        // When the player is nearby and presses E, open the puzzle.
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Opening puzzle...");
            mathPuzzleUI.SetActive(true);
            helpText.gameObject.SetActive(false);
            // Pause the game while the puzzle is active.
            Time.timeScale = 0;
        }
    }

    // Called when the puzzle is solved correctly.
    public void SolvePuzzle()
    {
        Debug.Log("Puzzle solved! Animal rescued.");
        // Increase score by 10 points.
        ScoreManager.instance.AddWasteScore(10);

        // Hide the puzzle UI and display the rescue message.
        mathPuzzleUI.SetActive(false);
        resultText.gameObject.SetActive(true);

        // Disable the animal's collider so the player can't stand on it.
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Resume normal game time.
        Time.timeScale = 1;

        // Start a coroutine that moves the animal left and then destroys it after 2 seconds.
        StartCoroutine(RescueAnimalRoutine());
    }

    // Called when the puzzle answer is wrong.
    public void FailPuzzle()
    {
        Debug.Log("Wrong answer! Try again.");
        mathPuzzleUI.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(ShowHelpTextAfterDelay());
    }

    // Re-show help text after a failed puzzle attempt.
    private IEnumerator ShowHelpTextAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        if (playerNearby)
        {
            helpText.gameObject.SetActive(true);
        }
    }

    // Coroutine that moves the animal left for 2 seconds, then hides the rescue message and destroys the animal.
    private IEnumerator RescueAnimalRoutine()
    {
        float elapsed = 0f;
        while (elapsed < 1f) // Changed duration from 5 seconds to 2 seconds.
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Hide the rescued message.
        resultText.gameObject.SetActive(false);
        Debug.Log("Removing animal object.");
        Destroy(gameObject);
    }
}
