using UnityEngine;

public class PlayerMvt : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    // New flag to enable/disable movement
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure Rigidbody2D settings are correct
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        // Check if movement is allowed
        if (canMove)
        {
            // Get input from Arrow Keys only
            movement.x = Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
            movement.y = Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0;

            // Flip sprite based on movement direction
            if (movement.x != 0)
            {
                spriteRenderer.flipX = movement.x < 0;
            }
        }
        else
        {
            // Stop movement when disabled
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    // Function to enable movement
    public void EnableMovement()
    {
        canMove = true;
    }

    // Function to disable movement
    public void DisableMovement()
    {
        canMove = false;
    }
}
