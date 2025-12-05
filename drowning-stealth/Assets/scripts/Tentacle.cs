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
        lr.SetPosition(0, end.position + (transform.position - end.position).normalized);

        Quaternion rotation = Quaternion.AngleAxis(-(Vector3.SignedAngle(transform.position - end.position, Vector3.up, Vector3.forward) - 180), Vector3.forward);
        end.rotation = rotation;
    }
}
