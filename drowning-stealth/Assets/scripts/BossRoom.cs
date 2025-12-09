using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public Collider2D entryTrigger;
    public GameObject octoBoss;

    private void Start()
    {
        entryTrigger = GetComponent<Collider2D>();
        octoBoss = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            octoBoss.SetActive(true);
            octoBoss.GetComponent<OctoBoss>().phase = 1;

            


        }
    }
}
