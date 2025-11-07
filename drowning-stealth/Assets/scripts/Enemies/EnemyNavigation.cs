using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    public Alertness alertness;

    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        alertness = GetComponent<Alertness>();
        //agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (alertness.alertLevel == 1)
        {
            agent.SetDestination(alertness.target);
            agent.stoppingDistance = 0.32f;
        }
        else
        {
            agent.SetDestination(transform.position);
            agent.stoppingDistance = 0;
        }
        
    }
}
