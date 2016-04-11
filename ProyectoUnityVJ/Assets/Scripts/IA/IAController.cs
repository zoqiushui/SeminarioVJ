using UnityEngine;
using System.Collections;

public class IAController : MonoBehaviour
{
    public Node node;
    public float speed;

    private void Update()
    {
        if (Vector3.Distance(transform.position,node.transform.position) < 2)
        {
            node = node.GetNextPosition();
        }
        transform.forward = Vector3.Slerp(transform.forward, node.transform.position - transform.position, 5 * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
