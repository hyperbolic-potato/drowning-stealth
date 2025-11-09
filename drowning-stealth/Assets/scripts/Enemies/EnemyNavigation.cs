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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        alertness = GetComponent<Alertness>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {

        switch (alertness.alertLevel)
        {
            case 0:

                if (patrolPoints.Length > 0)
                {
                    if(!patrolling) StartCoroutine(Patrol());
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.stoppingDistance = 0;
                }
                break;
            case 1:
                patrolling = false;
                StopCoroutine(Patrol());
                    agent.SetDestination(alertness.target);
                    agent.stoppingDistance = 0.32f;
                break;
            case 2:
                patrolling = false;
                StopCoroutine(Patrol());
                agent.SetDestination(alertness.target);
                agent.stoppingDistance = 0f;
                break;
            
            default:
                patrolling = false;
                StopCoroutine(Patrol());
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

            agent.destination = patrolPoints[i].position;
            while(agent.remainingDistance > 0.05f)
            {
                Debug.Log(i);
                yield return null;
            }
            

            //yield return new WaitForSeconds(patrolInterval);
        }
        patrolling = false;
    }
    
}
