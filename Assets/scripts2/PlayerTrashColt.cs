using UnityEngine;

public class PlayerTrashColt : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            FindObjectOfType<TrashMg>().AddScore(10);
            Destroy(other.gameObject);
        }
    }
}
