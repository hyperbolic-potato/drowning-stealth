using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHandler : MonoBehaviour
{

    public Transform player;
    public Camera cam;

    public float scaleFactor = 1f;

    private void Start()
    {
        player = transform.parent;
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 mousePos = player.position - cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 offset = mousePos * scaleFactor;
        transform.position = offset;
    }
}
