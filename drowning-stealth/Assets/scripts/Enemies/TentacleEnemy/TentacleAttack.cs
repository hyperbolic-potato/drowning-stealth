using UnityEngine;
using System.Collections;

public class TentacleAttack : MonoBehaviour
{
    public float delay = 0f;
    public float interval = 1f;
    public Vector2 direction = -Vector2.up;
    public float initiative = 0.5f;
    public float speed = 1f;
    public bool isRepeating;

    public GameObject tentacle;

    public LineRenderer lr;

    public LayerMask lm;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        StartCoroutine(AttackLoop());
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        RaycastHit2D[] playerHit = Physics2D.CircleCastAll((Vector2)transform.position, 0.05f, tentacle.transform.GetChild(0).localPosition, tentacle.transform.GetChild(0).localPosition.magnitude);
        foreach (RaycastHit2D hit in playerHit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                
                tentacle.transform.GetChild(0).position = hit.point;
            }
        }
    }

    void SummonTentacle()
    {
        lr.enabled = false;

        tentacle.SetActive(true);
        tentacle.transform.position = transform.position;
        tentacle.transform.GetChild(0).position = transform.position + (Vector3)direction.normalized;
        tentacle.transform.GetChild(0).GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    void StartAttack()
    {
        lr.enabled = true;

        tentacle.SetActive(false);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + direction.normalized, direction, 100f, lm);
        lr.SetPosition(0, transform.position);
        if (hit) lr.SetPosition(1, hit.point);
        else lr.SetPosition(1, (Vector2)transform.position + direction * 100f);
    }

    IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(delay);

        do
        {
            StartAttack();
            yield return new WaitForSeconds(initiative);
            //attack sequence here
            SummonTentacle();

            yield return new WaitForSeconds(interval);
        }
        while (isRepeating);
    }

}
