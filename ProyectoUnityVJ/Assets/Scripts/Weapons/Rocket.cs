using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public float velocity;
    public float turnSpeed;
    public Rigidbody myBody;
    private GameObject target;
    private Quaternion rotationTarget;

    
    void Start ()
    {
	
	}
	
	void FixedUpdate ()
    {
        if (target != null)
        {
            myBody.velocity = transform.forward * velocity;

            rotationTarget = Quaternion.LookRotation(target.transform.position - transform.position);
            myBody.MoveRotation(Quaternion.RotateTowards(transform.rotation , rotationTarget, turnSpeed));
        }
	}

    public void SetTarget(GameObject v)
    {
        target = v;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Target")
            Destroy(this.gameObject);
    }
}
