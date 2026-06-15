using Unity.VisualScripting;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    //calm lil singleton structure, goal is to create a game timer that persists through scenes

    //Const pulled from documentation, since this is a variable that SHOULD NEVER EVER BE TOUCHED
    //BY ANYTHING ELSE, const helps us ensure that the key stays the same no matter what
    private const string playtimeKey = "TotalPlaytime";
    private float sessionStart;

    //Singleton pattern, initialize instance of this one-and-only game timer
    private static GameTimer instance;

    //
    void Awake()
    {
        if (instance != null) 
        { 
            //if there is another instance, destory this instance
            Destroy(gameObject); 
            return; 
        }

        instance = this;
        DontDestroyOnLoad(gameObject); //ensure this object stays throughout scene loads
        sessionStart = Time.realtimeSinceStartup; //Stores when the session was started
    }

    void OnApplicationQuit() //method that calls when the application is quit
    {
        SavePlaytime();
    }

    //for debugging
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SavePlaytime();
        }
    }

    public static void SavePlaytime()
    {
        if (instance == null)
        {
            return;
        }

        float elapsed = Time.realtimeSinceStartup - instance.sessionStart; //time that's passed minus start time
        float total = PlayerPrefs.GetFloat(playtimeKey, 0f) + elapsed; //float version of getint used in modules
        PlayerPrefs.SetFloat(playtimeKey, total);
        PlayerPrefs.Save();
        instance.sessionStart = Time.realtimeSinceStartup; //after elapsed time has already been saved, sets new start point for a recount
    }

    public static float GetTotalPlaytime() //the getter we'll actually use for the UI
    {
        return PlayerPrefs.GetFloat(playtimeKey, 0f);
    } 
}
