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
    public float explosionForce = 10f;
    public ForceMode forceMode = ForceMode.Impulse;
    public float explosionRadius = 3f; // gets weaker the further out
    public float upwardsModifier = 0f; // how many meters to shift the y pos of force down by
    public bool affectPlayer = true;

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
        Explode();
    }

    void Explode()
    {
        Debug.Log("Exploding");
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
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (!rb)
            {
                if (affectPlayer && collider.CompareTag("Player"))
                {
                    rb = collider.GetComponentInParent<Rigidbody>();
                }
            }
            if (!rb) {continue;}
            Debug.Log("Rigidbody: " + rb);

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
