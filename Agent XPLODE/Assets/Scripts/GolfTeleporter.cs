using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class GolfTeleporter : MonoBehaviour
{
    public Transform tpDestination;
    public AudioSource golfSound;
    public float delayTime = 2.0f; //make a delay to allow sound effect to play before teleport
    public int rocketsAfterTeleport; //to ensure the player gets a specific number of rockets for the next hole

    public GameObject player;
    public RocketLauncher rocketLauncher;

    private bool isTeleporting = false; // Prevents triggering multiple times simultaneously

    private void OnTriggerEnter(Collider other)
    {
        // Check if player collides and we aren't already teleporting
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(TeleportSequence());
        }
    }

    private IEnumerator TeleportSequence()
    {
        isTeleporting = true;
        golfSound.Play();

        yield return new WaitForSeconds(delayTime);

        //sets current rockets to exactly the number specified
        rocketLauncher.currentRockets = rocketsAfterTeleport;

        //updates the UI
        rocketLauncher.rocketText.text = rocketLauncher.currentRockets.ToString();

        // Get the Rigidbody component from the player and turn physics off since they're goofing the teleportation
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        //send player object to inspector-assigned tp destination
        player.transform.position = tpDestination.position;
        player.transform.rotation = tpDestination.rotation;

        // Force Unity's physics engine to register the new position immediately
        Physics.SyncTransforms();
        rb.isKinematic = false;

        isTeleporting = false;
    }
}
