using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterScript : MonoBehaviour
{
    private bool isInWater = false;
    public AudioSource splooshSound;
    public float delayTime = 1.25f; //make a delay to allow sound effect to play before teleport


    private void OnTriggerEnter(Collider other)
    {
        // Check if player collides and we aren't already teleporting
        if (other.CompareTag("Player") && !isInWater)
        {
            StartCoroutine(inWater());
        }
    }

    //play sound effect, wait a lil bit and then reload scene
    private IEnumerator inWater()
    {
        isInWater = true;
        splooshSound.Play();

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
