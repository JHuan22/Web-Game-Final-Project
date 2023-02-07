using UnityEngine;
using UnityEngine.AI;

public class ObjectPathing : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = transform.position + new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        agent.destination = destination;
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            destination = transform.position + new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            agent.destination = destination;
        }
    }
}
