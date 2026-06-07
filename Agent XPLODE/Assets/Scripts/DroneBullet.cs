using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneBullet : MonoBehaviour
{
    public int bulletDamage = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3);
    }

    /* WILL BRING BACK AT SOME POINT SINCE IT'S MODULAR, FOR FP2 THO THERE'S AN IMPLEMENTATION ON PLAYER ALREADY
     * void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            var health = collider.GetComponentInParent<GeneralHealth>();
            if (health)
            {
                health.TakeDamage(bulletDamage);
                if (health.isAlive == false)
                {

                }
            }
            Destroy(gameObject);
        }
    }
     */

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void FireAway(Vector3 force, Quaternion initialRotation)
    {
        // Debug.Log("Firing Bullet!");
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.rotation = initialRotation;
        rb.AddForce(force);
    }
}
