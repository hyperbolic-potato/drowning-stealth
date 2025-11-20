using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCaught : MonoBehaviour
{
    public Vector2 respawnPosition;

    GameManager manager;
    Inventory inventory;

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
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Checkpoint"))
        {
            respawnPosition = collision.transform.position;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Finish"))
        {
            inventory.Clear();
            transform.parent = manager.transform;
            manager.LoadNextLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            inventory.Clear();
            transform.position = respawnPosition;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
