using UnityEngine;
using UnityEngine.InputSystem;

public class Throwable : Item
{
    public float power;
    Rigidbody2D rb;
    Collider2D col;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public override bool Usage()
    {
        gameObject.SetActive(true);

        col.isTrigger = false;

        Vector2 temp = transform.parent.position;
        transform.parent = null;
        transform.position = temp;

        Vector2 target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 ThrowDirection = (target - new Vector2(transform.position.x, transform.position.y)).normalized;
        Vector2 ThrowVector = ThrowDirection * power;

        rb.bodyType = RigidbodyType2D.Dynamic;
        
        rb.linearVelocity = ThrowVector;

        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
