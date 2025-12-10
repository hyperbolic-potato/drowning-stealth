using UnityEngine;

public class OctoBossFace : MonoBehaviour
{
    public GameObject throwable;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Destructable"))
        {
            transform.position = new Vector3(-2.5f, 3.5f, 0);

            GameObject inst = Instantiate(throwable, null);
            inst.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Environment"))
        {
            transform.position = new Vector3(-2.5f, 3.5f, 0);
        }
    }
}
