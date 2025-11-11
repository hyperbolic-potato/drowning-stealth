using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    public Vector2 moveInput;

    public float moveSpeed = 3f;
    public float sprintSpeed = 5f;
    public float crawlSpeed = 1f;


    public bool isMoving;
    public bool isSprinting;
    public bool isCrawling;

    public float moveNoise = 1.5f;
    public float crawlNoise = 0.75f;
    public float sprintNoise = 3f;
    public float idleNoise = 0.5f;

    public float noise;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //movement
        Vector2 move = Vector2.zero;
        if (isMoving)
        {
            if (isCrawling)
            {
                move = moveInput * crawlSpeed;
                noise = crawlNoise;
            }
            else if (isSprinting)
            {
                move = moveInput * sprintSpeed;
                noise = sprintNoise;
            }
            else
            {
                move = moveInput * moveSpeed;
                noise = moveNoise;
            }                    

            
        }
        else
        {
            noise = idleNoise;
        }
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
        isMoving = context.ReadValue<Vector2>().magnitude > 0;
    }
}
