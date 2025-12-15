using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerCaught : MonoBehaviour
{
    public Vector2 respawnPosition;

    GameManager manager;
    Inventory inventory;
    public List<int> doorsOpened;

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
            doorsOpened = null;
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
            manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

            /*
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Doors");
            //for once, shallow copy works in our favor
            foreach(GameObject door in doors)
            {
                
            }
            */
        }
    }
}
