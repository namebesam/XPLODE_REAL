using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(GeneralHealth))]
public class DroneEnemyBehavior : MonoBehaviour
{
    public enum DroneState
    {
        Patrolling,
        Hunting,
        Dead
    }

    [Header("General Settings")]
    public float movementForce = 75f;
    public float rotationSpeed = 10f;

    [Header("Roam Settings")]
    public Vector3 surveyPosition1;
    public Vector3 surveyPosition2;

    [Header("Attack Settings")]
    public float detectiopRange = 10f;
    public float followRange = 5f;
    public float escapeRange = 20f;
    public float fireRate = 1f;
    public float bulletForce = 750f;
    public GameObject projectilePrefab;
    public GameObject barrelPoint;

    public DroneState state;

    private GeneralHealth health;
    private Transform target;
    private Rigidbody rb;
    private float fireCoolDown;
    private bool IsEnabled = true;
    private Vector3 roamTargetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = DroneState.Patrolling;
        health = GetComponent<GeneralHealth>();
        roamTargetPosition = surveyPosition2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isAlive && state != DroneState.Dead)
        {
            state = DroneState.Dead;
        }
    }

    void FixedUpdate()
    {
            switch (state)
            {
                case DroneState.Patrolling:
                    DoRoamIfPossible();
                    CheckForPlayer();
                    break;
                case DroneState.Hunting:
                    AttackPlayer();
                    break;
                case DroneState.Dead:
                    if (IsEnabled)
                    {
                        IsEnabled = false;
                        Die();
                    }
                    break;
            }
    }

    void CheckForPlayer()
    {
        Collider[] potentials = Physics.OverlapSphere(transform.position, detectiopRange);
        foreach (Collider potential in potentials)
        {
            if (potential.CompareTag("Player"))
            {
                target = potential.transform;
                state = DroneState.Hunting;
            }
        }
    }

    void DoRoamIfPossible()
    {
        if (surveyPosition1 != null && surveyPosition2 != null)
        {
            Vector3 directionTo = GetDirectionTo(roamTargetPosition);
            if (IsLookingInDirection(directionTo))
            {
                AddForceInDirection(directionTo, movementForce);
            }
            LookInDirectionSmoothly(directionTo);

            float distanceTo = Vector3.Distance(roamTargetPosition, transform.position);
            if (distanceTo <= 0.05)
            {
                SwitchRoamTarget();
            }
        }
    }

    private void SwitchRoamTarget()
    {
        if (roamTargetPosition == surveyPosition1)
        {
            roamTargetPosition = surveyPosition2;
        } else
        {
            roamTargetPosition = surveyPosition1;
        }
    }

    private Vector3 GetDirectionTo(Vector3 pos)
    {
        return (pos - transform.position).normalized;
    }

    private void AddForceInDirection(Vector3 dir, float magnitude)
    {
        rb.AddForce(dir * magnitude * Time.fixedDeltaTime);
    }

    private void LookInDirectionSmoothly(Vector3 dir)
    {
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
    }

    private bool IsLookingInDirection(Vector3 dir)
    {
        Quaternion lookRot = Quaternion.LookRotation(dir);
        float difference = Vector3.Distance(lookRot.eulerAngles, transform.rotation.eulerAngles);
        if (difference >= 10)
        {
            return false;
        }
        return true;
    }

    void AttackPlayer()
    {
        Vector3 targetPos = target.position;
        float distanceTo = Vector3.Distance(targetPos, transform.position);

        if (distanceTo <= escapeRange)
        {
            Vector3 targetDir = Vector3.Normalize(targetPos - transform.position);
            transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), rotationSpeed * Time.fixedDeltaTime);

            if (distanceTo > followRange)
            {
                rb.AddForce(targetDir * movementForce * Time.fixedDeltaTime);
            }

            if (fireCoolDown <= 0)
            {
                ShootProjectile(targetDir);
                fireCoolDown = 1f / fireRate;
            }

            fireCoolDown -= Time.deltaTime;
        } else
        {
            state = DroneState.Patrolling;
        }
        
    }

    void ShootProjectile(Vector3 targetDirection)
    {
        var bullet = Instantiate(projectilePrefab, barrelPoint.transform.position, Quaternion.identity);
        
        DroneBullet script = bullet.GetComponentInChildren<DroneBullet>();
        if (script)
        {
            script.FireAway(targetDirection * bulletForce, transform.localRotation);
        }
    }

    void Die()
    {
        Debug.Log("man im dead...(drone enemy)");
        Destroy(gameObject, 3);
    }
}
