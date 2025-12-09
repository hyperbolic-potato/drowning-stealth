using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Inventory playerInv;

    public int doorID;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInv = collision.gameObject.GetComponent<Inventory>();

            foreach (GameObject item in playerInv.pockets)
            {
                Key temp = null;
                if (item != null) temp = item.GetComponent<Key>();

                if(temp != null && temp.id == doorID)
                {
                    Destroy(item);
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            playerInv = null;
        }
    }
}
