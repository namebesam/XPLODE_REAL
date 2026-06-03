using UnityEngine;

public class PushBackModel : MonoBehaviour
{
    public Transform startTransform; // where the raycast should start from
    public Transform modelTransform; // smoothly moves model
    public Transform holderTransform; // instantly moves fire point/holder
    public float lengthFromStart = 1.66f;
    public float maxPushBackDistance = 1.1f;
    public float speed = 4f;

    Vector3 goalOffset;
    Vector3 currentOffset; // current offset of model
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentOffset = modelTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        Vector3 startPos = startTransform.position;
        Vector3 startOffset = startTransform.localPosition;
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(startPos, startTransform.forward, out hitInfo, lengthFromStart);
        if (hit)
        {
            Debug.Log("Model: collides with " + hitInfo.collider.name);
            float pushDistance = Mathf.Min(lengthFromStart - hitInfo.distance, maxPushBackDistance);
            Debug.Log("Pushback: Hit: " + pushDistance);
            goalOffset = startOffset + Vector3.forward * -pushDistance;
        }
        else
        {
            Debug.Log("Pushback: NoHit");
            goalOffset = startOffset;
        }

        holderTransform.localPosition = goalOffset;

        Vector3 next = Vector3.MoveTowards(currentOffset, goalOffset, speed * Time.deltaTime);
        modelTransform.localPosition = next;
        Debug.Log("Pushback: goalOffset = " + goalOffset + " transform = " + holderTransform.localPosition
            + " speed = " + speed + " dt = " + Time.deltaTime);
        currentOffset = next;
    }
}
