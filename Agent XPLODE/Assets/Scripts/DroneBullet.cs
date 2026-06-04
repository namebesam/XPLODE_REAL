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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            var health = collider.GetComponentInParent<GeneralHealth>();
            if (health)
            {
                health.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
    }

    public void FireAway(Vector3 force, Quaternion initialRotation)
    {
        // Debug.Log("Firing Bullet!");
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.rotation = initialRotation;
        rb.AddForce(force);
    }
}
