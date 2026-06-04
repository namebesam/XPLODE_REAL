using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform firePoint; //where the rocket gets fired from
    public GameObject rocketPrefab; //the rocket that gets shot

    public float rocketForce = 50f; //force on the outward rocket
    public float launchForce = 20f; //inward force on the player

    public float rocketCooldown = 0.8f; // can lower and make the animation faster
    float cooldownTimer = 0;

    Rigidbody rb;
    Rigidbody rocketRb;

    public Animator rocketRecoilAnim;
    public GameObject muzzleExplosionPrefab; //for the muzzle flash

    public AudioSource rocketLaunchSFX;

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
            if (cooldownTimer <= 0)
            {
                FireRocket();
                cooldownTimer = rocketCooldown;
            }
        }
        cooldownTimer -= Time.deltaTime;
    }

    void FireRocket()
    {
        //spawn rocket prefab at tip and make a local object
        GameObject rocketToLaunch = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        Instantiate(muzzleExplosionPrefab, firePoint.position, firePoint.rotation);

        //Play sound effect
        rocketLaunchSFX.Play();

        //send said local object flying forward
        rocketRb = rocketToLaunch.GetComponent<Rigidbody>();
        rocketRb.AddForce(firePoint.forward * rocketForce, ForceMode.Impulse); //might change this from impulse later

        //Push player in the opposite direction (cheap solution for now, may make more strictly physics-based later
        //documentation pull, -(direction vector) should just send the force in the exact opposite direction
        rb.AddForce(-firePoint.forward * launchForce, ForceMode.Impulse); //definitely keep impulse for now

        //play recoil animation
        rocketRecoilAnim.SetTrigger("RPGFire");


    }
}
