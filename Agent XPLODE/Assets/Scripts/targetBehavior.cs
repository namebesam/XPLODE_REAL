using UnityEngine;

public class targetBehavior : MonoBehaviour
{
    public bool isTargetDead = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rocket"))
        {
            isTargetDead = true;
            gameObject.SetActive(false);
        }
    }
}
