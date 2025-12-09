using UnityEngine;
using System.Collections;

public class OctoBoss : MonoBehaviour
{
    public int phase;

    public Transform leftWall;
    public Transform rightWall;

    public GameObject tentacleAttack;

    public float attackDelay = 3f;

    public bool isDormant = true;
    private void Start()
    {
        phase = 0;
        gameObject.SetActive(false);

        leftWall = transform.GetChild(4);
        rightWall = transform.GetChild(5);
    }

    private void Update()
    {
        if (isDormant && phase == 1)
        {
            StartCoroutine(AttackHandler());
            isDormant = false;
        }
        if (phase >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            phase++;

        }
    }

    IEnumerator AttackHandler()
    {
        
        while (phase < 4)
        {
            float coinFlip = Random.Range(-1f, 1f);

            if (coinFlip < 0) StartCoroutine(WaveAttack(leftWall));
            else StartCoroutine(WaveAttack(rightWall));

            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator WaveAttack(Transform wall)
    {
        GameObject inst = tentacleAttack;
        TentacleAttack temp = inst.GetComponent<TentacleAttack>();
        temp.delay = 0;
        temp.isRepeating = false;
        if (wall = leftWall) temp.direction = Vector2.right;
        else temp.direction = Vector2.left;
        temp.direction = Quaternion.AngleAxis(Random.Range(-5f, 5f), Vector3.forward) * temp.direction;

        temp.initiative = 0.75f;
        temp.speed = 14f;

        for(int i = 0; i < wall.childCount; i++)
        {
            Instantiate(inst, wall.GetChild(i));
            yield return new WaitForSeconds(0.05f);
        }
    }
}
