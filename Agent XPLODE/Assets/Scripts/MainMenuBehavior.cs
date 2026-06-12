using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class MainMenuBehavior : MonoBehaviour
{
    public GameObject creditsMenu;
    public GameObject mainMenu;
    public GameObject settingsMenu;

    //text object tracking playtime
    public TMP_Text playtimeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        settingsMenu.SetActive(false);

        UpdatePlaytimeText(); //Updates clock with every new session (less performance intensive)
    }

    //debugging
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdatePlaytimeText();
        }
    }

    public void OpenCredits()
    {
        //check to see if both are attached
        if(mainMenu && creditsMenu && settingsMenu)
        {
            //setting the credits menu active and turning everything else off (reused  later)
            creditsMenu.SetActive(true);
            mainMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Attach the menu panels in the inspector");
        }
    }

    public void OpenSettings()
    {
        //check to see if both are attached
        if (mainMenu && creditsMenu && settingsMenu)
        {
            settingsMenu.SetActive(true);
            creditsMenu.SetActive(false);
            mainMenu.SetActive(false);  
        }
        else
        {
            Debug.LogWarning("Attach the menu panels in the inspector");
        }
    }

    public void BackButton()
    {
        //check to see if both are attached
        if (mainMenu && creditsMenu && settingsMenu)
        {
            mainMenu.SetActive(true);
            creditsMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Attach the menu panels in the inspector");
        }
    }

    public void StartGame()
    {
        //load next scene in scenelist which is level 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void QuitGame()
    {
        //CRASHES THE GAME ON WEBGL but still need this button so gonna keep it
        Application.Quit();
    }

    public void UpdatePlaytimeText()
    {
        float seconds = GameTimer.GetTotalPlaytime();
        int h = (int)(seconds / 3600);
        int m = (int)(seconds % 3600) / 60;
        int s = (int)(seconds % 60);
        playtimeText.text = "Total Playtime: " + h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");

    }
}
