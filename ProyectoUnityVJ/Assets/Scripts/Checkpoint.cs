using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour
{
    public List<GameObject> checkpointNodes { get; private set; }
    public Checkpoint nextCheckpoint { get; private set; }

    private void Awake()
    {
        checkpointNodes = new List<GameObject>();
        GameObject go = new GameObject("LeftNode");
        go.transform.position = transform.position + -transform.right * transform.localScale.x / 3;
        checkpointNodes.Add(go);

        go = new GameObject("RightNode");
        go.transform.position = transform.position + transform.right * transform.localScale.x / 3;
        checkpointNodes.Add(go);

        go = new GameObject("MiddleNode");
        go.transform.position = transform.position;
        checkpointNodes.Add(go);

        RaycastHit hit;
        Ray ray;
        List<GameObject> nodesToRemove = new List<GameObject>() ;
        foreach (var item in checkpointNodes)
        {
            item.transform.parent = transform;
            item.AddComponent(typeof(Node));
            ray = new Ray(item.transform.position, -item.transform.up);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.layer == K.LAYER_GROUND)
                {
                    item.transform.position = hit.point + Vector3.up;
                }
                else
                {
                    nodesToRemove.Add(item);
                }
            }
        }
        foreach (var item in nodesToRemove)
        {
            checkpointNodes.Remove(item);
            Destroy(item);
        }
    }

    public void SetNextCheckpoint(Checkpoint chk)
    {
        nextCheckpoint = chk;
    }

    public Vector3 GetRespawnPoint(Vector3 vehiclePos)
    {
        float aux = float.MaxValue;
        Vector3 selectedNode = new Vector3();
        foreach (var node in checkpointNodes)
        {
            if (Vector3.Distance(node.transform.position, vehiclePos) < aux)
            {
                aux = Vector3.Distance(node.transform.position, vehiclePos);
                selectedNode = node.transform.position;
            }
        }
        return selectedNode;
    }

    public Vector3 GetRandomPositionFromNode()
    {
        int rnd = Random.Range(0, checkpointNodes.Count);
        return checkpointNodes[rnd].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<VehicleController>() != null)
        {
            print(other.gameObject);
            if (CheckpointManager.instance.CheckVehicleCheckpoint(other.gameObject, this)) other.gameObject.GetComponent<VehicleController>().SetCheckpoint(this);
        }
    }

    private void OnDrawGizmos()
    {
        if (nextCheckpoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + transform.right * transform.localScale.x / 2, transform.position - transform.right * transform.localScale.x / 2);
        Gizmos.DrawLine(transform.position, nextCheckpoint.transform.position);
    }
}
