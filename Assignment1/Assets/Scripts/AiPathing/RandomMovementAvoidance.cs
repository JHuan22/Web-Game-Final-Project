using UnityEngine;
using UnityEngine.AI;

public class RandomMovementAvoidance : MonoBehaviour
{
    public float maxDistance = 10f; // maximum distance the object can move in any direction
    public float avoidanceRadius = .9f; // radius within which the object should avoid obstacles
    public float avoidanceStrength = 10f; // strength of the avoidance force
    public LayerMask obstacleLayer; // layer mask for obstacles

    private NavMeshAgent agent;
    private Vector3 previousPosition;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
        previousPosition = transform.position;
    }

    private void Update()
    {
        // check if the agent has reached the destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        // generate a random destination within the maxDistance from the current position
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxDistance, NavMesh.AllAreas);

        // set the agent's destination to the new position
        agent.destination = hit.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }

    private void FixedUpdate()
{
    // calculate the avoidance force
    Vector3 avoidanceForce = Vector3.zero;
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, avoidanceRadius, obstacleLayer);
    if (hitColliders.Length > 0)
    {
        foreach (Collider hitCollider in hitColliders)
        {
            Vector3 direction = transform.position - hitCollider.ClosestPoint(transform.position);
            avoidanceForce += direction.normalized * avoidanceStrength;
        }
    }
    else
    {
        // if there are no obstacles within the avoidance radius, move in the direction we were previously moving
        Vector3 direction = transform.position - previousPosition;
        avoidanceForce += direction.normalized * avoidanceStrength;
    }

    // calculate the desired velocity based on the agent's current velocity and the avoidance force
    Vector3 desiredVelocity = agent.velocity + avoidanceForce * Time.fixedDeltaTime;
    desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, agent.speed);

    // gradually apply the desired velocity to the agent's velocity using the agent's acceleration
    Vector3 acceleration = (desiredVelocity - agent.velocity) / Time.fixedDeltaTime;
    agent.velocity += acceleration * Time.fixedDeltaTime;

    // update the previous position
    previousPosition = transform.position;

    // check if the agent has reached the destination
    if (!agent.pathPending && agent.remainingDistance < 0.5f)
    {
        SetNewDestination();
    }
}

}
