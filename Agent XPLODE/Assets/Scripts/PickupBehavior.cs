using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public int pickupValue = 10;
    public AudioClip pickupSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            RocketLauncher rocketLauncher = other.GetComponentInParent<RocketLauncher>();
            rocketLauncher.currentRockets = rocketLauncher.currentRockets + pickupValue;
            
            //kind of a wraparound, gets the rocketlauncher text text value from rocketlauncher script, 
            //then updates the UI here. Could update UI over there for pickups
            rocketLauncher.rocketText.text = rocketLauncher.currentRockets.ToString();

            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
            Destroy(gameObject, 0.1f);
        }
    }
}
