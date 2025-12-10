using UnityEngine;

public class OctoBossFace : MonoBehaviour
{
    public GameObject throwable;
    public bool stunned;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Destructable"))
        {
            stunned = true;

            GameObject inst = Instantiate(throwable, null);
            inst.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }
    }
}
