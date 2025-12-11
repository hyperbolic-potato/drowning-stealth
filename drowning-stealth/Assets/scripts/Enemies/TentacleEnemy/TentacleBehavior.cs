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
    }

    private void Update()
    {

        isAlive = alertness.alertLevel > -1;



        isAlert = alertness.alertLevel >= 1;



            anim.SetBool("isAlive", isAlive);
            anim.SetBool("isAlert", isAlert);
        if (!isAlive)
        {
            col.enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);

            }
        }
        

    }
}
