using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The player
    public float smoothSpeed = 5f;  // Adjust speed
    public Vector3 offset = new Vector3(0, 2, -10);  // Ensure it's behind the player

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
