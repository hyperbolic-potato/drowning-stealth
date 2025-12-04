using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavigation : MonoBehaviour
{

    Alertness alertness;

    NavMeshAgent agent;

    EyesightDetector eye;

    public Transform[] patrolPoints;
    public float patrolInterval;

    bool patrolling = false;

    public float patrolSpeed = 1.0f;
    public float chaseSpeed = 3.5f;

    int nesw = 0;

    public Coroutine co;

    Animator anim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        alertness = GetComponent<Alertness>();
        anim = GetComponent<Animator>();
        eye = GetComponent<EyesightDetector>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //StartCoroutine(Patrol());
    }

    private void Update()
    {

        switch (alertness.alertLevel)
        {
            case 0:
                
                if (patrolPoints.Length > 0)
                {
                    if(!patrolling) co = StartCoroutine(Patrol());
                }
                else
                {
                    agent.SetDestination(transform.position);
                    
                }
                agent.speed = patrolSpeed;
                agent.stoppingDistance = 0f;
                break;
            case 1:
                patrolling = false;
                if(co != null) StopCoroutine(co);
                agent.SetDestination(alertness.target);
                agent.stoppingDistance = 0.32f;
                agent.speed = patrolSpeed;
                break;
            case 2:
                patrolling = false;
                if (co != null) StopCoroutine(co);
                agent.SetDestination(alertness.target);
                agent.stoppingDistance = 0f;
                agent.speed = chaseSpeed;
                break;
            
            default:
                patrolling = false;
                if (co != null) StopCoroutine(co);
                agent.SetDestination(transform.position);
                agent.stoppingDistance = 0;
                break;

        }
        
        if (eye.orientation.x < 0 && Mathf.Abs(eye.orientation.x) > Mathf.Abs(eye.orientation.y)) nesw = 4;
        else if (eye.orientation.x > 0 && Mathf.Abs(eye.orientation.x) > Mathf.Abs(eye.orientation.y)) nesw = 2;
        else if (eye.orientation.y > 0 && Mathf.Abs(eye.orientation.y) > Mathf.Abs(eye.orientation.x)) nesw = 1;
        else if (eye.orientation.y < 0 && Mathf.Abs(eye.orientation.y) > Mathf.Abs(eye.orientation.x)) nesw = 3;

        anim.SetBool("isAlert", alertness.alertLevel > 1);
        anim.SetInteger("NESW", nesw);

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sinkhole"))
        {
            agent.enabled = false;
            eye.enabled = false;
            alertness.enabled = false;
            Destroy(transform.GetChild(0).gameObject);
            this.enabled = false;
            collision.enabled = false;
        }
    }

    IEnumerator Patrol()
    {
        
        patrolling = true;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            

            float distance = 1f;
            do
            {
                agent.SetDestination(patrolPoints[i].position); 

                distance = (
                            new Vector2(patrolPoints[i].position.x, patrolPoints[i].position.y) -
                            new Vector2(transform.position.x, transform.position.y)
                            ).magnitude;


                yield return null;
            } while (distance > 0.05);

            yield return new WaitForSeconds(patrolInterval);
        }
        patrolling = false;
    }
    
}
