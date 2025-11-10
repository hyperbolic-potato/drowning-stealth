using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavigation : MonoBehaviour
{

    public Alertness alertness;

    public NavMeshAgent agent;

    public Transform[] patrolPoints;
    public float patrolInterval;

    bool patrolling = false;

    public float patrolSpeed = 1.0f;
    public float chaseSpeed = 3.5f;

    public Coroutine co;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        alertness = GetComponent<Alertness>();
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
                StopCoroutine(co);
                agent.SetDestination(alertness.target);
                agent.stoppingDistance = 0.32f;
                agent.speed = patrolSpeed;
                break;
            case 2:
                patrolling = false;
                StopCoroutine(co);
                agent.SetDestination(alertness.target);
                agent.stoppingDistance = 0f;
                agent.speed = chaseSpeed;
                break;
            
            default:
                patrolling = false;
                StopCoroutine(co);
                agent.SetDestination(transform.position);
                agent.stoppingDistance = 0;
                break;

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
