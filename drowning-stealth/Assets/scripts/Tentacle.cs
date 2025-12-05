using UnityEngine;

public class Tentacle : MonoBehaviour
{
    LineRenderer lr;
    Transform end;
    Vector2 endPosition;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        end = transform.GetChild(0);
    }


    void Update()
    {
        lr.SetPosition(1, transform.position);
        lr.SetPosition(0, end.position);

        Quaternion rotation = Quaternion.identity;
        rotation = Quaternion.LookRotation(transform.position - end.position) * Quaternion.AngleAxis(-90, Vector3.right);
        end.rotation = rotation;
    }
}
