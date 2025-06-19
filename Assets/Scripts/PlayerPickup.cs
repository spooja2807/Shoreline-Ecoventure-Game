using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform handPosition; // HandPosition GameObject
    public GameObject carriedTrash = null;  // Trash that is picked up
    private TrashSorting currentBin = null; // Current bin the player is near

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player enters a trash bin area, and set the currentBin to that bin
        if (other.CompareTag("Organic") || other.CompareTag("Recyclable") || other.CompareTag("Dangerous") || other.CompareTag("General"))
        {
            if (carriedTrash == null) // Only pick up if not already carrying trash
            {
                carriedTrash = other.gameObject; // Store the picked-up trash
                carriedTrash.transform.SetParent(handPosition); // Attach trash to the hand position
                carriedTrash.transform.localPosition = new Vector3(0, 0.2f, 0); // Position it in front of the hand
                carriedTrash.GetComponent<Collider2D>().enabled = false; // Disable collider while carrying
                Debug.Log("Picked up trash: " + other.gameObject.name);
            }
        }

        // If the player is near any trash bin, store the bin's reference
        if (other.CompareTag("OrganicBin") || other.CompareTag("RecyclableBin") || other.CompareTag("DangerousBin") || other.CompareTag("GeneralBin"))
        {
            currentBin = other.GetComponent<TrashSorting>();
        }
    }

    void Update()
    {
        // Drop the trash when pressing Space and if near the correct bin
        if (Input.GetKeyDown(KeyCode.Space) && carriedTrash != null && currentBin != null)
        {
            // Check if the bin is correct for the type of trash
            if (currentBin.CheckIfCorrectTrash(carriedTrash))
            {
                // Correct bin, drop the trash there
                carriedTrash.GetComponent<Collider2D>().enabled = true; // Re-enable collider
                carriedTrash.transform.SetParent(null); // Detach trash from the hand
                Destroy(carriedTrash); // Destroy the trash (or disable it if you want it to stay)
                carriedTrash = null; // Clear reference
                Debug.Log("Correct bin! Trash dropped.");
            }
            else
            {
                // Wrong bin, keep trash and show feedback
                Debug.Log("Incorrect bin! Trash not dropped.");
                carriedTrash.transform.SetParent(handPosition); // Keep trash in hand
                carriedTrash.transform.localPosition = new Vector3(0, 0.2f, 0); // Position it correctly
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If player exits the bin's area, reset the current bin
        if (other.CompareTag("OrganicBin") || other.CompareTag("RecyclableBin") || other.CompareTag("DangerousBin") || other.CompareTag("GeneralBin"))
        {
            currentBin = null;
        }
    }
}
