using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCaught : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // a stop-gap solution until checkpoints are implemented
        }
    }
}
