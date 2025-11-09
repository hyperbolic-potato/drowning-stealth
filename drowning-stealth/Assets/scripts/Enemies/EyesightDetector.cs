using Unity.VisualScripting;
using UnityEngine;


public class EyesightDetector : MonoBehaviour
{
    //this class to be used on an object childed to the enemy

    public LayerMask lm;

    Vector2 orientation = Vector2.up;

    Alertness alertness;

    public float angle;
    public int count;
    public float distance;
    public bool debug;

    private void Start()
    {
        alertness = transform.GetComponentInParent<Alertness>();
    }

    private void Update()
    {
        RaycastHit2D check = RaycastScan(angle, count, distance, debug);
        if (check)
        {
            alertness.TriggerChase();
        }

        orientation = transform.localToWorldMatrix * Vector2.up;

    }
    private RaycastHit2D RaycastScan(float angle, int count, float dist, bool debug)
    {
        Ray2D[] rays = new Ray2D[count];
        for (int i = 0; i < count; i++)
        {
            Quaternion currentAngle = Quaternion.AngleAxis(
                (-angle / 2) +                              //chop the angle down the middle
                (angle / (float)count * i) +                //add sub-angle proportinal to index in the array
                Random.Range(0f, angle / (float)count),     //give it some chaos game
                Vector3.forward);                           //rotate around the z axis

            Vector2 direction = (currentAngle * orientation);
            rays[i] = new Ray2D(transform.position, direction);

            RaycastHit2D hit = Physics2D.Raycast(rays[i].origin, rays[i].direction, dist, lm);


            if (debug)
            {
                if (hit.collider != null) Debug.DrawLine(rays[i].origin, hit.point);
                else Debug.DrawLine(rays[i].origin, rays[i].origin + rays[i].direction * dist);
            } 

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                return hit;
            }


                
        }
        return new RaycastHit2D();
    }
}
