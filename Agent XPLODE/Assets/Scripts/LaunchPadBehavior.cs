using UnityEngine;

public class LaunchPadBehavior : MonoBehaviour
{
    public float launchForce = 10.0f;
    public GameObject player;

    public AudioSource bounceSFX;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bounceSFX.Play();

            // Apply a single upward impulse
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        }
    }
}
