using UnityEngine;

public class HearingDetector : MonoBehaviour
{
    public Alertness alertness;

    private void Start()
    {
        alertness = GetComponent<Alertness>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckForNoise(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForNoise(collision);
    }

    private void CheckForNoise(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Noise"))
        {
            alertness.TriggerInvestigation(collision.transform.position);
            //Debug.Log(this.gameObject + " heard a noise at " + collision.transform.position + "!");
        }
    }
}
