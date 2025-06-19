using UnityEngine;

public class TrashSorting : MonoBehaviour
{
    public string correctBinTag;  // Tag of the correct bin for this trash

    void OnTriggerEnter2D(Collider2D other)
    {
        // This method is used to store the current bin in PlayerPickup for proximity checking
    }

    public bool CheckIfCorrectTrash(GameObject trash)
    {
        // Compare the tag of the trash with the correct bin's tag
        if (trash.CompareTag(correctBinTag))
        {
            return true; // Correct trash
        }
        return false; // Incorrect trash
    }
}
