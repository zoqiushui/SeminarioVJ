using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    private List<Node> _nextWaypointPosition;

    private void Start()
    {
        _nextWaypointPosition = new List<Node>();
        Physics.IgnoreLayerCollision(K.LAYER_NODE,K.LAYER_PLAYER);
        GetNextWaypoints();
    }

    private void GetNextWaypoints()
    {
        var waypoints = Physics.OverlapSphere(transform.position, 150);
        foreach (var w in waypoints)
        {
            if (w.gameObject != this && w.gameObject.layer == K.LAYER_NODE)
                if (Vector3.Angle(transform.forward, w.transform.position - transform.position) < 45)
                {
                    _nextWaypointPosition.Add(w.GetComponent<Node>());
                }
        }
    }

    /// <summary>
    /// Devuelve la proxima posicion.
    /// </summary>
    /// <returns></returns>
    public Node GetNextPosition()
    {
        var rnd = Random.Range(0, _nextWaypointPosition.Count);
        return _nextWaypointPosition[rnd];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        if (_nextWaypointPosition != null)
        {
            foreach (var waypoint in _nextWaypointPosition)
            {
                Gizmos.DrawLine(transform.position, waypoint.transform.position);
            }
        }
    }
}
