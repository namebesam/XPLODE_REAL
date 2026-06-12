using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehavior : MonoBehaviour
{
    public bool isGamePaued = false;
    public GameObject pauseMenu;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaued)
            {
                //resumes the game
                ResumeGame();
            }

            else
            {
                //pauses the game
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        isGamePaued = true;
        GameTimer.SavePlaytime(); //saves total playtime to gametimer singleton on pause
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; // unlocks cursor so UI is clickable
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isGamePaued = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked; // re-locks cursor for gameplay
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); //load first scene in build which is main  menu
        Time.timeScale = 1.0f;
    }
}
