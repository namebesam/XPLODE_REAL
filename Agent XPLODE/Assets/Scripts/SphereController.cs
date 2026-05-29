using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SphereController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = true;
    public Rigidbody rb;

    //public AudioClip jumpSFX;

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log("Horizontal: " + horizontal);
        Debug.Log("Vertical: " + vertical);

        // compute a movement vector
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        // apply force 
        rb.AddForce(movement * speed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //disable when in midair
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with: " + collision.transform.name);
        ContactPoint contact = collision.contacts[0];
        Debug.Log("Collided with floor at position: " + contact.point);
        Debug.Log("Contact normal: " + contact.normal);

        //anything above .5 generally represents a horizontal surface
        if (contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }

        //could also do where you collide with anthing tagged with Ground (how I usually do it)

    }
}
