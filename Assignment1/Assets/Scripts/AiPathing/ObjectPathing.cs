using UnityEngine;
using UnityEngine.AI;

public class ObjectPathing : MonoBehaviour
{
    public float speed = 5f;
    public float directionChangeInterval = 1f;
    public float maxDistance = 10f;

    private NavMeshAgent agent;
    private Vector3 destination;
    private bool isTurningAround = false;
    private float turnAroundStartTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        // Check if we need to change direction
        if (!isTurningAround && agent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }

        // Check if we need to turn around
        if (isTurningAround)
        {
            float turnAroundDuration = Time.time - turnAroundStartTime;
            if (turnAroundDuration > directionChangeInterval)
            {
                isTurningAround = false;
                SetRandomDestination();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killers"))
        {
            Debug.Log("COLLIDING");
            // Change direction to opposite
            Vector3 directionToOther = other.transform.position - transform.position;
            Vector3 oppositeDirection = -directionToOther.normalized * maxDistance;
            Vector3 newDestination = transform.position + oppositeDirection;

            if (NavMesh.SamplePosition(newDestination, out NavMeshHit hit, maxDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                isTurningAround = true;
                turnAroundStartTime = Time.time;
            }
        }
    }

    private void SetRandomDestination()
    {
        // Generate a random destination on the NavMesh surface
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, maxDistance, NavMesh.AllAreas);
        destination = hit.position;

        // Set the destination for the agent
        agent.SetDestination(destination);
    }
}
