using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused = false;

    public InputActionAsset inp;

    public RectTransform indicator;
    public Inventory inv;

    public Image slot1;
    public Image slot2;

    public Sprite defaultIcon;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        if(pauseMenu != null) pauseMenu.SetActive(false);
        if (inp == null) Debug.LogError("GameManager is missing her input scheme :( make sure she has one in the editor");
        

        
    }

    private void Update()
    {
        if (inp != null && inp["Escape"].WasPerformedThisFrame()) Pause();
        if (inv != null)
        {
            if (inv.pockets[0] != null) slot1.sprite = inv.pockets[0].GetComponent<SpriteRenderer>().sprite;
            if (inv.pockets[1] != null) slot2.sprite = inv.pockets[1].GetComponent<SpriteRenderer>().sprite;

            if(inv.selectedItem == 0) indicator.localPosition = new Vector2(-32, 45);
            if(inv.selectedItem == 1) indicator.localPosition = new Vector2(32, 45);
            
        }
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

    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
