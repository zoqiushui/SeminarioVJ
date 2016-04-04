using UnityEngine;
using System.Collections;

public class VehicleCamera : MonoBehaviour
{
    public Transform target = null;
    private float height;
    public float velocityDamping;
    private Vector3 prevVelocity;
    private Vector3 currentVelocity;

    void Awake()
    {
        prevVelocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        height = transform.localPosition.y;
    }
    void FixedUpdate()
    {
        currentVelocity = Vector3.Lerp(prevVelocity, target.transform.parent.GetComponent<Rigidbody>().velocity, velocityDamping * Time.deltaTime);
        currentVelocity.y = 0;
        prevVelocity = currentVelocity;
    }

    void LateUpdate()
    {
        float speedFactor = Mathf.Clamp01(target.transform.parent.GetComponent<Rigidbody>().velocity.magnitude / 70f);
        Camera.main.fieldOfView = Mathf.Lerp(55, 72, speedFactor);
        float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);

        currentVelocity = currentVelocity.normalized;

        Vector3 newTargetPosition = target.position + new Vector3(0,height - target.position.y,0);
        Vector3 newPosition = newTargetPosition - (currentVelocity * currentDistance);
        newPosition.y = newTargetPosition.y;

        transform.position = newPosition;
        transform.LookAt(target.position + Vector3.up *3);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.transform.position + transform.up * 3);
    }
}
