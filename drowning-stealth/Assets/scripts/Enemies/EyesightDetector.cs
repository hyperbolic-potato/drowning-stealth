using UnityEngine;


public class EyesightDetector : MonoBehaviour
{
    private void Update()
    {
        RaycastScan(90f, 5, 1f, true);
    }
    private bool RaycastScan(float angle, int count, float dist, bool debug)
    {
        Ray2D[] rays = new Ray2D[count];
        for (int i = 0; i < count; i++)
        {
            Quaternion currentAngle = Quaternion.AngleAxis((-angle / 2) + (angle / (float)count * i), Vector3.forward);
            Vector2 direction = currentAngle * Vector2.up;
            rays[i] = new Ray2D(transform.position, direction);
            Debug.DrawRay(rays[i].origin, rays[i].direction);
        }
        return false;
    }
}
