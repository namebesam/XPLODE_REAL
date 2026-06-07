using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevel;
    public GameObject player;
    public GameObject target;

    // Declare here at class level, not inside Start()
    private FPSPlayerController fpsController;
    private targetBehavior targetBehave;
    private RocketLauncher rocketLauncherScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fpsController = player.GetComponent<FPSPlayerController>();
        targetBehave = target.GetComponent<targetBehavior>();
        rocketLauncherScript = player.GetComponent<RocketLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fpsController.playerLost)
        {
            LevelLost();
        }

        if(rocketLauncherScript.outOfRockets)
        {
            LevelLost();
        }

        if (targetBehave.isTargetDead)
        {
            LevelBeat();
        }
    }

    public void LevelBeat()
    {
        //add more stuff down the line
        LoadNextLevel();
    }

    public void LevelLost()
    {
        //add other stuff down the line
        Invoke("ReloadSameScene", 0.5f);
    }

    void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    void ReloadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel.Length > 0)
        {
            LoadSceneByName(nextLevel);
        }
        else
        {
            LoadSceneByIndex(nextSceneIndex);
        }
    }
}
