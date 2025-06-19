using UnityEngine;

public class PlayerMovement_3 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //  Block input if a mini-game is active
        if (InputBlocker.IsInputBlocked)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput = moveInput.normalized * speed;
        rb.linearVelocity = moveInput;
    }
}
