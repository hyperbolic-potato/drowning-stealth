using UnityEngine;
using UnityEngine.Rendering;

public class Sinkhole : MonoBehaviour
{
    Collider2D col;
    public Sprite sunkHole;
    SpriteRenderer sr;
    AudioSource sfx;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        sfx = GetComponent<AudioSource>();
        
    }

    public void Sink()
    {

        col.isTrigger = false;
        sr.sprite = sunkHole;
        sr.color = Color.white;
        sfx.Play();

    }
}
