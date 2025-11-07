using UnityEngine;

public class HearingDetector : MonoBehaviour
{
    public Alertness alertness;

    private void Start()
    {
        alertness = GetComponent<Alertness>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Noise"))
        {
            if (alertness.alertLevel == 0)
            {
                alertness.SetAlertLevel(1);
                alertness.target = collision.transform.position;
            }
            Debug.Log(this.gameObject + " heard a noise at " + collision.transform + "!");
        }
        
    }
}
