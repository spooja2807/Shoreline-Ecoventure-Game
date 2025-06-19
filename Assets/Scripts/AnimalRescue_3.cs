using UnityEngine;
using System.Collections;

public class AnimalRescue_3 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite rescuedSprite; // Assign the rescued sprite in Inspector
    public float moveSpeed = 2f; // Speed for animal to move away

    private bool isRescued = false; //  Add this line

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isRescued) return; //  Prevent triggering if already rescued

        if (other.CompareTag("Player"))
        {
            QuickReactionChallenge_3 challenge = FindFirstObjectByType<QuickReactionChallenge_3>();
            if (challenge != null)
            {
                challenge.StartChallenge(this); // Start challenge and pass reference
            }
        }
    }

    public void CompleteRescue()
    {
        if (spriteRenderer != null && rescuedSprite != null)
        {
            spriteRenderer.sprite = rescuedSprite; // Change sprite
        }

        isRescued = true; //  Mark as rescued so it won't trigger again

        // Move the animal away
        StartCoroutine(MoveAway());
    }

    IEnumerator MoveAway()
    {
        float moveTime = 3f; // Move for 3 seconds
        float timer = 0;

        while (timer < moveTime)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime; // Move left
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // Remove animal after moving
    }
}
