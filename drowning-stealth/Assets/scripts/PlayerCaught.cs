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
    Collider2D col;
    Rigidbody2D rb;

    AudioSource sfx;
    public AudioClip[] sfxList;

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
        sfx = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ( SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        }
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
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            anim.SetBool("isDead", true);

            inventory.enabled = false;
            movement.enabled = false;
            col.enabled = false;
            rb.simulated = false;
            movement.isTrapped = false;

            StartCoroutine(ResetCharacter());

            sfx.clip = sfxList[0];
            sfx.Play();
            
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
        col.enabled=true;
        rb.simulated = true;


        /*
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Doors");
            //for once, shallow copy works in our favor
            foreach(GameObject door in doors)
            {
                
            }
            */
    }
}
