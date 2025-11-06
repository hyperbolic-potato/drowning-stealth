using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    public Vector2 moveInput;

    public float moveSpeed = 3f;
    public float sprintSpeed = 5f;
    public float crawlSpeed = 1f;

    public bool isSprinting;
    public bool isCrawling;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //movement
        Vector2 move;

        if (isCrawling)         move = moveInput * crawlSpeed;
        else if (isSprinting)   move = moveInput * sprintSpeed;
        else                    move = moveInput * moveSpeed;

        rb.linearVelocity = move;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValue<float>() > 0;
        
        
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        isCrawling = context.ReadValue<float>() > 0;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
