using UnityEngine;

public class TentacleEnd : MonoBehaviour
{

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.localPosition = Vector3.zero;

        this.transform.parent.gameObject.SetActive(false);
    }
}
