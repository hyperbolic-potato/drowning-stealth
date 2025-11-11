using UnityEngine;

public class SoftCover : MonoBehaviour
{

    public PlayerMovement pm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if(pm != null && pm.isCrawling)
        {
            gameObject.layer = 0; //layer two is ignore raycast
        }
        else
        {
            gameObject.layer = 2;
        }
    }
}
