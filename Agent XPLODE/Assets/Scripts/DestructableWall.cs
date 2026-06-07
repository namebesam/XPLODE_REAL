using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    public GameObject destroyEffectPrefab;
    public AudioClip destroySFX; // Change AudioSource to AudioClip


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rocket"))
        {
            Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(destroySFX, transform.position, 1f); //has some 3d spatial audio issues that cause quiet falloff
            Destroy(gameObject, 0.1f);
        }
    }
}
