using UnityEngine;
using System.Collections;

public class FollowWaypoint : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public int currentWaypoint;
    public Waypoint[] waypoints;
    void Update()
    {
        var dirToWaypoint = waypoints[currentWaypoint].transform.position - transform.position;
        dirToWaypoint.y = transform.forward.y;
        transform.forward = Vector3.Slerp(transform.forward, dirToWaypoint, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= 2)
        {
            if (currentWaypoint < waypoints.Length - 1) currentWaypoint++;
            else currentWaypoint = 0;
        }
    }
}
