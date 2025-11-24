using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    public Animator anim;

    public Vector2 moveInput;

    public float moveSpeed = 3f;
    public float sprintSpeed = 5f;
    public float crawlSpeed = 1f;


    public bool isMoving;
    public bool isSprinting;
    public bool isCrawling;

    public bool isTrapped;

    public int escapeThreshold;
    public int escapeProgress;
    public SpriteRenderer escapeIndicator;

    public float moveNoise = 1.5f;
    public float crawlNoise = 0.75f;
    public float sprintNoise = 3f;
    public float idleNoise = 0.5f;

    public float noise;

    public int nesw = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        escapeIndicator = transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isTrapped)
        {
            if (escapeProgress >= escapeThreshold)
            {
                isTrapped = false;
            }
            escapeIndicator.enabled = true;
            escapeIndicator.size = new Vector2((float)escapeProgress / (float)escapeThreshold, 1);
            escapeIndicator.transform.localPosition = new Vector3(-0.5f * (1 - (float)escapeProgress / (float)escapeThreshold), 0.6f, 0);
        }
        else
        {
            escapeIndicator.enabled = false;
        }

            //movement
            Vector2 move = Vector2.zero;
        if(!isTrapped)
        {
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



            if (move.x < 0) nesw = 4;
            else if (move.x > 0) nesw = 2;
            else if (move.y > 0) nesw = 1;
            else if (move.y < 0) nesw = 3;
            //else                    nesw = 0;



            rb.linearVelocity = move;


            
        }
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isCrouching", isCrawling);
        anim.SetBool("isSprinting", isSprinting);
        anim.SetInteger("NESW", nesw);


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
        if (!isTrapped)
        {
            moveInput = context.ReadValue<Vector2>();
            isMoving = context.ReadValue<Vector2>().magnitude > 0;
        }
        else if ( Random.Range(0f, 1f) <= 0.4f && context.performed)
        {
            escapeProgress++;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sinkhole"))
        {
            isTrapped = true;
            rb.linearVelocity = Vector2.zero;
            isMoving = false;
            escapeProgress = 0;

            collision.enabled = false;
        }
    }


}
