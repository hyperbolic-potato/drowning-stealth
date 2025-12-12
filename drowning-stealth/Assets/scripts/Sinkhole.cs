using UnityEngine;

public class Sinkhole : MonoBehaviour
{
    Collider2D col;
    public Sprite sunkHole;
    SpriteRenderer sr;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Sink()
    {
        col.isTrigger = false;
        sr.sprite = sunkHole;
    }
}
