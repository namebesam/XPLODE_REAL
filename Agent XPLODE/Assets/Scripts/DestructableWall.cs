using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    public GameObject destroyEffectPrefab;
    public AudioSource destroySFX;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rocket"))
        {
            Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
            destroySFX.Play();
            Destroy(gameObject, 0.1f);
        }
    }
}
