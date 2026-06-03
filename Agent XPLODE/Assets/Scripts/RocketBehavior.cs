using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    [Header("General Settings")]
    // public float damage = 50f;
    public float lifetime = 5f;

    [Header("Explosion Effects")]
    public GameObject explosionEffect;
    public AudioClip explosionHitSFX;

    [Header("Explosion Physics")]
    public float explosionForce = 30f;
    public ForceMode forceMode = ForceMode.Impulse;
    public float explosionRadius = 6f; // gets weaker the further out
    public float upwardsModifier = 1f; // how many meters to shift the y pos of force down by
    public bool affectsPlayer = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Explode", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Rocket collision");

        // Check collision
        if (collision.gameObject.GetComponent<RocketBehavior>()) // do not explode on same projectile
        {
            return;
        }
        Explode();
    }

    void Explode()
    {
        Debug.Log("Rocket exploding");
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionHitSFX, transform.position);

        ApplyExplosionForce(transform.position);
        Destroy(gameObject);
    }

    void ApplyExplosionForce(Vector3 explosionPos)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // Debug.Log("Collider: " + collider.name);
            if (collider.gameObject == gameObject) {continue;}
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (!rb)
            {
                if (affectsPlayer)
                {
                    Transform parent = collider.GetComponent<Transform>().parent;
                    if (parent && parent.CompareTag("Player"))
                    {
                        // Debug.Log("Player");
                        rb = parent.GetComponent<Rigidbody>();
                    }
                }
            }
            if (!rb) {continue;}
            Debug.Log("Explosion hit rigidbody: " + rb);

            rb.AddExplosionForce(explosionForce, explosionPos,
                explosionRadius, upwardsModifier, forceMode);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
