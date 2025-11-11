using UnityEngine;

public class Alertness : MonoBehaviour
{
    public int alertLevel;

    public Vector2 target;

    float attentionSpan;
    public float attentionSpanMax = 5;

    public Transform player;
    // 0 for idle, 1 for investigating, 2 for chasing, -1 for stunned

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        if (attentionSpan > 0)
        {
            attentionSpan -= Time.deltaTime;
        }
        else
        {
            alertLevel = 0;
        }

        if (alertLevel == 2)
        {
            target = player.position;
        }
        if (alertLevel < -1 || alertLevel > 2) Debug.LogError("invalid alert level");
    }

    public void TriggerInvestigation(Vector2 newTarget)
    {
        if (alertLevel < 2 && alertLevel > -1)
        {
            alertLevel = 1;
        target = newTarget;
            attentionSpan = attentionSpanMax;
        }
        
    }

    public void TriggerChase()
    {
        if (alertLevel > -1)
        {
            alertLevel = 2;
            attentionSpan = attentionSpanMax;
        }
    }
}
