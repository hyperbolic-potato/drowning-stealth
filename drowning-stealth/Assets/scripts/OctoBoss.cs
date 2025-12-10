using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class OctoBoss : MonoBehaviour
{
    public int phase;

    public Transform leftWall;
    public Transform rightWall;
    public Transform face;
    Rigidbody2D faceRB;
    public Transform player;

    public GameObject tentacleAttack;
    public GameObject floorTentacle;

    public float attackDelay = 3f;

    public LineRenderer lr;

    public LayerMask lm;


    public bool isDormant = true;
    private void Start()
    {
        phase = 0;
        gameObject.SetActive(false);

        leftWall = transform.GetChild(4);
        rightWall = transform.GetChild(5);
        face = transform.GetChild(0);
        faceRB = face.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

        lr = GetComponent<LineRenderer>();
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
            //float coinFlip = Random.Range(-1f, 1f);


            //if (coinFlip < 0) StartCoroutine(WaveAttack(leftWall));
            //else StartCoroutine(WaveAttack(rightWall));

            StartCoroutine(ChargeAttack());

            yield return new WaitForSeconds(7f);
        }
    }

    IEnumerator WaveAttack(Transform wall)
    {
        GameObject inst = Instantiate(tentacleAttack, transform);
        inst.SetActive(false);
        TentacleAttack temp = inst.GetComponent<TentacleAttack>();
        temp.delay = 0;
        temp.isRepeating = false;
        if (wall == leftWall) temp.direction = Vector2.right;
        else temp.direction = Vector2.left;
        //temp.direction = Quaternion.AngleAxis(Random.Range(-5f, 5f), Vector3.forward) * temp.direction;

        temp.initiative = 0.75f;
        temp.speed = 14f;

        for(int i = 0; i < wall.childCount; i++)
        {
            GameObject subInst = Instantiate(inst, wall.GetChild(i));
            subInst.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
        inst = null;
    }

    IEnumerator ChargeAttack()
    {
        Vector2 tgt = Vector2.zero;
        float countdown = 3f;
        do
        {
            lr.enabled = true;
            lr.SetPosition(0, face.position);

            RaycastHit2D hit = Physics2D.Raycast(face.position, player.position - face.position, 100f, lm);

            lr.SetPosition(1, hit.point);

            countdown -= Time.deltaTime;

             tgt = player.position - face.position;

            yield return new WaitForEndOfFrame();
        }
        while (countdown > 0f);

        yield return new WaitForSeconds(0.75f);

        lr.enabled = false;

        faceRB.linearVelocity = tgt.normalized * 10f;
    }
}
