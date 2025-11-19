using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused = false;

    public InputActionAsset inp;




    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        if(pauseMenu != null) pauseMenu.SetActive(false);
        if (inp == null) Debug.LogError("GameManager is missing her input scheme :( make sure she has one in the editor");

        
    }

    private void Update()
    {
        if (inp != null && inp["Escape"].WasPerformedThisFrame()) Pause();
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
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
