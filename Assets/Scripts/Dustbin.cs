using UnityEngine;

public class Dustbin : MonoBehaviour
{
    // Set this in the Inspector (e.g., "RecyclableTrash", "OrganicTrash")
    public string correctTrashTag;

    void Start()
    {
        Debug.Log("Dustbin setup with correctTrashTag: " + correctTrashTag);
    }
}
