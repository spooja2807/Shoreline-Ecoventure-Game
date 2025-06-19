using UnityEngine;

public class TrashItem : MonoBehaviour
{
    private GameObject heldTrash = null;
    public Transform holdPosition;
    private bool isNearBin = false;
    private GameObject currentBin;
    private string currentBinTag;

    public TrashMg trashManager; // Drag your TrashMg GameObject here in Inspector

    void Update()
    {
        if (heldTrash != null && Input.GetKeyDown(KeyCode.Space))
        {
            if (isNearBin)
            {
                CheckTrashTypeAndDispose();
            }
            else
            {
                DropTrash(); // Drop normally if not near a bin
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect bins
        if (collision.CompareTag("RecycleBin") || collision.CompareTag("NonRecycleBin"))
        {
            isNearBin = true;
            currentBin = collision.gameObject;
            currentBinTag = collision.tag;
        }

        // Detect trash only if not holding anything
        if (heldTrash == null && collision.GetComponent<TrashType>() != null)
        {
            PickUpTrash(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentBin)
        {
            isNearBin = false;
            currentBin = null;
        }
    }

    void PickUpTrash(GameObject trash)
    {
        if (heldTrash == null)
        {
            heldTrash = trash;
            trash.transform.position = holdPosition.position;
            trash.transform.SetParent(transform);
            trash.GetComponent<Collider2D>().enabled = false;
            trash.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void DropTrash()
    {
        if (heldTrash != null)
        {
            heldTrash.GetComponent<Collider2D>().enabled = true;
            heldTrash.GetComponent<Rigidbody2D>().simulated = true;
            heldTrash.transform.SetParent(null);
            heldTrash = null;
        }
    }

    void CheckTrashTypeAndDispose()
    {
        TrashType trashType = heldTrash.GetComponent<TrashType>();

        if (trashType == null)
        {
            DropTrash();
            return;
        }

        bool correctBin =
            (trashType.isRecyclable && currentBinTag == "RecycleBin") ||
            (!trashType.isRecyclable && currentBinTag == "NonRecycleBin");

        if (correctBin)
        {
            trashManager.AddScore(10); //  Use your TrashMg script
            Destroy(heldTrash);
            heldTrash = null;
        }
        else
        {
            trashManager.AddScore(-10); // Penalize wrong bin
            DropTrash();
        }
    }
}
