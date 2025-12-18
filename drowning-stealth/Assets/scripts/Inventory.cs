using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public GameObject[] pockets;
    public int selectedItem;
    public GameObject interactable;
    public int capacity = 5;

    Animator anim;
    public bool isThrowing;


    

    private void Start()
    {
        pockets = new GameObject[capacity];
        anim = GetComponent<Animator>();    
    }

    private void Update()
    {
        anim.SetBool("isThrowing", isThrowing);
        if (isThrowing)
        {

            StartCoroutine(ThrowAnim());
        }
    }

    IEnumerator ThrowAnim()
    {
        yield return new WaitForSeconds(0.65f);
        isThrowing = false;
    }
    public void Pickup()
    {
        if (interactable != null && pockets[selectedItem] == null)
        {
            pockets[selectedItem] = interactable;
            interactable.transform.parent = transform;
            interactable.transform.position = Vector3.zero;
            interactable.SetActive(false);
            interactable = null;
        }
            
    }

    public void Swap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selectedItem++;
            selectedItem %= capacity;
        }
        
    }

    public void Use()
    {
        if(pockets[selectedItem] != null)
        {
            Item item = pockets[selectedItem].GetComponent<Item>();

            if (item != null)
            {
                bool success = item.Usage();
                if (success)
                {
                    pockets[selectedItem] = null;
                }
            }
        }
        
    }

    public void Clear()
    {
        pockets = new GameObject[capacity];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            interactable = collision.gameObject;
        }
    }


}
