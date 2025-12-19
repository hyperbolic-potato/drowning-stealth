using UnityEngine;

public class TentacleBehavior : MonoBehaviour
{
    public Alertness alertness;
    Animator anim;
    public bool isAlive = true;
    public bool isAlert = true;

    Collider2D col;

    private void Start()
    {
        alertness = GetComponent<Alertness>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        anim.SetBool("isAlive", true);
    }

    private void Update()
    {

        isAlive = alertness.alertLevel > -1;



        isAlert = alertness.alertLevel >= 1;



            
            anim.SetBool("isAlert", isAlert);
            
        if (!isAlive)
        {
            anim.SetBool("isAlive", false);

            col.enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);

            }
        }
        

    }
}
