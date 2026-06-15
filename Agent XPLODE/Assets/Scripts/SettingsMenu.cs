using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        //hooks up to the Mater Audio mixer and adjusts value for output based on slider value
        Debug.Log("Volume Changed");
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void changeSens() //placeholder for sensitivity change code
    {

    }
}
