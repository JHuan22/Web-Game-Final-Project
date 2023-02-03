using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePathing : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1.25f;
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypoint < waypoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
            {
                currentWaypoint++;
            }
        }
        else
        {
            currentWaypoint = 0;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(waypoints[i].position, 0.2f);
        }
    }
}
