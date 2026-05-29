using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform firePoint; //where the rocket gets fired from
    public GameObject rocketPrefab; //the rocket that gets shot

    public float rocketForce = 50f; //force on the outward rocket
    public float launchForce = 20f; //inward force on the player

    Rigidbody rb;
    Rigidbody rocketRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireRocket();
        }
    }

    void FireRocket()
    {
        //spawn rocket prefab at tip and make a local object
        GameObject rocketToLaunch = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);

        //send said local object flying forward
        rocketRb = rocketToLaunch.GetComponent<Rigidbody>();
        rocketRb.AddForce(firePoint.forward * rocketForce, ForceMode.Impulse); //might change this from impulse later

        //Push player in the opposite direction (cheap solution for now, may make more strictly physics-based later
        //documentation pull, -(direction vector) should just send the force in the exact opposite direction
        rb.AddForce(-firePoint.forward * launchForce, ForceMode.Impulse); //definitely keep impulse for now
    }
}
