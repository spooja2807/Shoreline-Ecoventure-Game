using UnityEngine;

public class AnimalMover : MonoBehaviour
{
    public float speed = 2f;
    private bool shouldMove = false;

    // Call this method to start the leftward movement.
    public void StartMovingLeft()
    {
        shouldMove = true;
    }

    void Update()
    {
        if (shouldMove)
        {
            // Moves the animal left at the given speed.
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
