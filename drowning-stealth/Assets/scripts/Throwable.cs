using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Audio;

public class Throwable : Item
{
    public float power;
    Rigidbody2D rb;
    Collider2D col;

    AudioSource sfx;

    public AudioClip[] sfxList;

    public GameObject noise;
    public float noiseVolume = 7f;

    public LayerMask interactable;
    public LayerMask throwable;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sfx = GetComponent<AudioSource>();
    }

    public override bool Usage()
    {
        gameObject.SetActive(true);

        sfx.clip = sfxList[0];
        sfx.Play();

        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        transform.rotation = Quaternion.identity;
        col.isTrigger = false;
        col.excludeLayers = throwable;

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
        StartCoroutine(Impact());
    }

    IEnumerator Impact()
    {

        sfx.clip = sfxList[1];
        sfx.Play();

        GameObject n = Instantiate(noise, null);
        n.transform.position = transform.position;
        Noise noiseComponent = n.GetComponent<Noise>();
        noiseComponent.duration = 0.25f;
        noiseComponent.radius = noiseVolume;
        noiseComponent.timed = true;

        rb.linearDamping = 10f;
        rb.angularDamping = 10f;
        while(rb.linearVelocity.magnitude > 0.05f)
        {
            yield return null;
        }
        col.isTrigger = true;
        col.excludeLayers = interactable;
        
        yield return null;
    }
}
