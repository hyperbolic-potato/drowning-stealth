using System.Linq;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    Inventory playerInv;
    PlayerCaught pc;

    public int doorID;

    private void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerCaught>();
        /*
        for (int i = 0; i < pc.doorsOpened.Count; i++)
        {
            
            if (pc.doorsOpened[i] == doorID)
            {
                Destroy(transform.parent.gameObject);
            }

        }
        */
        if (pc.doorsOpened.Contains(doorID))
        {
            Destroy(transform.parent.gameObject);
            //this will make sure both the door and key get destroyed
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInv = collision.gameObject.GetComponent<Inventory>();
            pc = collision.gameObject.GetComponent<PlayerCaught>();

            foreach (GameObject item in playerInv.pockets)
            {
                Key temp = null;
                if (item != null) temp = item.GetComponent<Key>();

                if(temp != null && temp.id == doorID)
                {
                    pc.doorsOpened.Add(doorID);
                    

                    Destroy(item);
                    Destroy(this.gameObject);
                }
            }

            
        }
        else
        {
            playerInv = null;
            pc = null;
        }
    }
}
