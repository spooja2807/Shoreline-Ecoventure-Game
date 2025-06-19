using UnityEngine;

public class Trash_pic : MonoBehaviour
{
    private GameObject heldTrash = null;
    public Transform holdPosition;
    private bool isNearBin = false;
    private GameObject currentBin;
    private string currentBinTag;

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
        if (collision.CompareTag("RecyclingBin") || collision.CompareTag("TrashBin"))
        {
            isNearBin = true;
            currentBin = collision.gameObject;
            currentBinTag = collision.tag;
        }
        else if ((collision.CompareTag("Recyclable") || collision.CompareTag("NonRecyclable")) && heldTrash == null)
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
        if (heldTrash == null) // Only pick up if not holding anything
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
        string trashTag = heldTrash.tag;

        if ((trashTag == "Recyclable" && currentBinTag == "RecyclingBin") ||
            (trashTag == "NonRecyclable" && currentBinTag == "TrashBin"))
        {
            Game_Manager.Instance.UpdateTrashScore(10);
            Destroy(heldTrash); // Remove trash from scene
            heldTrash = null;
        }
        else
        {
            Game_Manager.Instance.UpdateTrashScore(-5);
            DropTrash(); // Drop trash if incorrect bin
        }
    }
}
