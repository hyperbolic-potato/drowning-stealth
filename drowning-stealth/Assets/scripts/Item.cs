using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;

    public virtual bool Usage()
    {
        return false;
    }
}
