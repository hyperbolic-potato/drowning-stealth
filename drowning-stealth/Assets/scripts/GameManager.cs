using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused = false;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    public void Pause()
    {
        if (pauseMenu != null && !paused)
        {
            paused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Resume();
        }

    }

    public void Resume()
    {
        if (pauseMenu != null)
        {
            paused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
