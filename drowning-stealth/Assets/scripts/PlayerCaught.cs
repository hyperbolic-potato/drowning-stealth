using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PlayerCaught : MonoBehaviour
{
    public Vector2 respawnPosition;

    GameManager manager;
    Inventory inventory;
    PlayerMovement movement;
    public List<int> doorsOpened;

    Animator anim;

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
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
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
            manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
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
            anim.SetBool("isDead", true);

            inventory.enabled = false;
            movement.enabled = false;
            

            StartCoroutine(ResetCharacter());
            
        }
    }

    IEnumerator ResetCharacter()
    {
        yield return new WaitForSeconds(1.2f);

        inventory.Clear();
        transform.position = respawnPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        anim.SetBool("isDead", false);
        inventory.enabled = true;
        movement.enabled = true;

        /*
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Doors");
            //for once, shallow copy works in our favor
            foreach(GameObject door in doors)
            {
                
            }
            */
    }
}
