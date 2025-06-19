using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    private Transform player;
    private bool isHeld = false;
    private static GameObject heldTrash = null;  // Only one trash item at a time
    public float dropRange = 1.5f;  // Distance to drop trash into a bin

    private bool hasBeenInWrongBin = false;  // Track if the trash has been in the wrong bin

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Player not found. Ensure the player has the 'Player' tag.");
    }

    void Update()
    {
        if (isHeld && heldTrash != null)
        {
            // Move trash to player's hand position
            heldTrash.transform.position = player.position + new Vector3(0.5f, 0.5f, 0);
        }

        if (isHeld && Input.GetKeyDown(KeyCode.Space))  // Press Space to drop trash
        {
            TryDropTrash();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isHeld && heldTrash == null)
        {
            PickUpTrash();
        }
    }

    void PickUpTrash()
    {
        isHeld = true;
        heldTrash = this.gameObject;
        GetComponent<Collider2D>().enabled = false;  // Disable collider while holding trash
        Debug.Log("Picked up trash: " + gameObject.tag);  // Debug: Show picked-up trash tag
    }

    void TryDropTrash()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.position, dropRange);
        bool trashPlacedInBin = false;

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("OrganicBin") || hit.CompareTag("RecyclableBin") || 
                hit.CompareTag("DangerousBin") || hit.CompareTag("GeneralBin"))
            {
                Dustbin dustbin = hit.GetComponent<Dustbin>();

                if (dustbin != null)
                {
                    Debug.Log("Comparing trash tag: " + heldTrash.tag + " with bin tag: " + dustbin.correctTrashTag);

                    if (heldTrash.CompareTag(dustbin.correctTrashTag))  // Correct bin
                    {
                        if (!hasBeenInWrongBin)  // Only award points if it's not in the wrong bin
                        {
                            ScoreManager.instance.AddWasteScore(10);
                            ScoreManager.instance.ShowSuccessPrompt("Correct Bin! +10 Points");
                            Debug.Log("Correct bin! Awarded +10 points for " + heldTrash.tag);
                        }
                        else
                        {
                            ScoreManager.instance.ShowSuccessPrompt("Correct Bin (No points for previous wrong placement).");
                            Debug.Log("Correct bin, but no points awarded (wrong bin used earlier).");
                        }

                        Destroy(heldTrash);  // Destroy trash after correct placement
                        heldTrash = null;
                        isHeld = false;
                        trashPlacedInBin = true;
                        hasBeenInWrongBin = false;  // Reset wrong bin flag after correct placement
                        break;  // Exit loop after placing the trash
                    }
                    else  // Wrong bin
                    {
                        ScoreManager.instance.DeductWasteScore(5);
                        hasBeenInWrongBin = true;
                        ScoreManager.instance.ShowErrorPrompt("Wrong Bin! -5 Points", 5f);

                        Debug.Log("Wrong bin! Deducted -5 points for " + heldTrash.tag);

                        // Move the trash to the wrong bin position
                        heldTrash.transform.position = hit.transform.position;

                        // Allow for continued incorrect placement without preventing further penalties
                    }
                }
            }
        }

        if (!trashPlacedInBin)
        {
            Debug.Log("No bin detected! Move closer to a bin to drop the trash.");
        }
    }

    public static GameObject GetHeldTrash()
    {
        return heldTrash;
    }
}
