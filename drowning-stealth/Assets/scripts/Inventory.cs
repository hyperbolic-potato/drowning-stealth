using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] pockets;
    public int selectedItem;
    public GameObject interactable;

    

    private void Start()
    {
        pockets = new GameObject[5];
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            interactable = collision.gameObject;
        }
    }


}
