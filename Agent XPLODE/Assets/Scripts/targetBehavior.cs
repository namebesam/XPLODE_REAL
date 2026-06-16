using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public bool isTargetDead = false;

    void Start()
    {
        isTargetDead = false;
    }
    
    public void Die()
    {
        isTargetDead = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rocket"))
        {
            isTargetDead = true;
            gameObject.SetActive(false);
        }
    }
}
