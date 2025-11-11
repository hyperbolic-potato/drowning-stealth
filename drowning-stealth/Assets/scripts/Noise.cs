using UnityEngine;
using System.Collections;

public class Noise : MonoBehaviour
{
    CircleCollider2D col;

    public float radius = 0f;
    public float duration = 0f;
    public bool timed = true;

    protected void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.radius = radius;

        if (timed) StartCoroutine(TimedSelfDestruct());
    }

    protected void Update()
    {
        col.radius = radius;
    }

    IEnumerator TimedSelfDestruct()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
