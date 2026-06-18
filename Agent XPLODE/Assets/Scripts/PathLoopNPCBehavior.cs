using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathLoopNPCBehavior : MonoBehaviour
{
    public Vector3[] patrolPositions;

    private int patrolPosIndex = 0;
    private Animator anim;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        if (patrolPositions.Length > 0)
            StartCoroutine(RoamAndIdle());
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    IEnumerator RoamAndIdle()
    {
        while (true)
        {
            Debug.Log("Walking to next pos");
            agent.isStopped = false;
            anim.SetBool("isWalking", true);
            agent.SetDestination(GetNextPosition());

            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                // Debug.Log("Agent: " + agent.pathPending + " dist: " + agent.remainingDistance
                //     + " " + agent.stoppingDistance);
                yield return null;
            }

            Debug.Log("Stopping to wait");
            agent.isStopped = true;
            anim.SetBool("isWalking", false);
            yield return new WaitForSeconds(5);
        }
    }

    private Vector3 GetNextPosition()
    {   
        if (patrolPosIndex == patrolPositions.Length-1)
        {
            patrolPosIndex = 0;
        } else
        {
            patrolPosIndex++;
        }
        Vector3 pos = patrolPositions[patrolPosIndex];
        return pos;
    }
}
