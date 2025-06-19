using UnityEngine;

public class TrashPickup_3 : MonoBehaviour
{
    private Transform player;
    private bool isHeld = false;
    private static GameObject heldTrash = null;  // Only one trash item at a time
    public float dropRange = 3f;  // Drop range around the player

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log("Player found: " + player.name);
        }
        else
        {
            Debug.LogError("Player not found. Tag the player object with 'Player'.");
        }
    }

    void Update()
    {
        if (isHeld && heldTrash != null)
        {
            heldTrash.transform.position = player.position + new Vector3(0.5f, 0.5f, 0);
        }

        if (isHeld && Input.GetKeyDown(KeyCode.Space))  // SPACE to drop
        {
            Debug.Log("Drop attempt initiated.");
            TryDropTrash();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isHeld && heldTrash == null)
        {
            Debug.Log("Player collided with trash: " + gameObject.name);
            PickUpTrash();
        }
    }

    void PickUpTrash()
    {
        isHeld = true;
        heldTrash = this.gameObject;
        GetComponent<Collider2D>().enabled = false;
        Debug.Log("Picked up trash: " + gameObject.name + " (Tag: " + gameObject.tag + ")");
    }

    void TryDropTrash()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.position, dropRange);
        bool dropped = false;

        Debug.Log("Checking nearby bins within range: " + dropRange);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Dustbin"))
            {
                Dustbin_3 bin = hit.GetComponent<Dustbin_3>();

                if (bin != null)
                {
                    Debug.Log("Detected bin: " + hit.name + " | Bin expects: " + bin.correctTrashTag + " | Trash held: " + heldTrash.tag);

                    if (heldTrash.CompareTag(bin.correctTrashTag))
                    {
                        ScoreManager_3.instance.AddWasteScore(10);
                        Debug.Log("Correct bin. +10 points.");
                    }
                    else
                    {
                        ScoreManager_3.instance.DeductWasteScore(5);
                        Debug.Log("Wrong bin. -5 points.");
                    }

                    Destroy(heldTrash);
                    Debug.Log("Trash destroyed: " + heldTrash.name);
                    heldTrash = null;
                    isHeld = false;
                    dropped = true;
                    break;
                }
                else
                {
                    Debug.LogWarning("Dustbin found but missing Dustbin_3 script.");
                }
            }
        }

        if (!dropped)
        {
            Debug.Log("No valid bin nearby. Move closer to a dustbin.");
        }
    }

    public static GameObject GetHeldTrash()
    {
        return heldTrash;
    }

    public static void DropTrash()
    {
        if (heldTrash != null)
        {
            Debug.Log("Force drop: " + heldTrash.name);

            Collider2D col = heldTrash.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = true;

            Rigidbody2D rb = heldTrash.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            }

            heldTrash = null;
        }
    }
}
