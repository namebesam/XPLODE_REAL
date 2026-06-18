using System;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    float minVolume = -80f; // assumed is same as min value of ui slider
    float maxVolume = 0f; // assumed is same as max value of ui slider
    
    public SettingsHolder settingsHolderPrefab;

    public void SetVolume(float volume)
    {
        // convert from logarithmic scale to linear scale
        double decibelRatio = Math.Pow(10f, 1f / 10f);
        double minLinearVolume = Math.Pow(decibelRatio, minVolume);
        double maxLinearVolume = Math.Pow(decibelRatio, maxVolume);
        double alpha = (volume - minVolume) / (maxVolume - minVolume);
        double newVolumeLinear = (maxLinearVolume - minLinearVolume) * alpha + minLinearVolume;
        double newVolumeDecibel = Math.Log(newVolumeLinear) / Math.Log(decibelRatio);

        //hooks up to the Mater Audio mixer and adjusts value for output based on slider value
        Debug.Log("Volume Changed " + alpha
            + " ratio: " + decibelRatio
            + " " + minLinearVolume + " " + maxLinearVolume + " "
            + newVolumeLinear + " " + newVolumeDecibel);
        audioMixer.SetFloat("Volume", (float) newVolumeDecibel);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSens(float sens)
    {
        SettingsHolder settingsHolder = FindAnyObjectByType<SettingsHolder>();
        if (!settingsHolder)
        {
            settingsHolder = Instantiate(settingsHolderPrefab);
        }
        DontDestroyOnLoad(settingsHolder);
        settingsHolder.playerSens = sens;
    }
}
