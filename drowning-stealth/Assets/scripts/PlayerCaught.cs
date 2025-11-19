using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCaught : MonoBehaviour
{
    public Vector2 respawnPosition;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            respawnPosition = collision.transform.position;
            Destroy(collision.gameObject);
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            transform.position = respawnPosition;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
