using UnityEngine;
using System.Collections;

public class Noise : MonoBehaviour
{
    public CircleCollider2D col;

    public float radius = 0f;
    public float duration = 0f;
    public bool timed = true;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.radius = radius;

        if (timed) StartCoroutine(TimedSelfDestruct());
    }

    IEnumerator TimedSelfDestruct()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
