using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public float velocity;
    public float turnSpeed;
    public float radio;
    public float damage;
    public LayerMask layersDamage;
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
        var cols = Physics.OverlapSphere(transform.position, radio, layersDamage);
        for (int i = 0; i < cols.Length; i++)
        {/*
            if (cols[i].GetComponent<Rigidbody>() != null)
            {
                Vector3 direction = cols[i].transform.position - transform.position;
                float dist = direction.magnitude;
                direction.Normalize();
                print(cols[i].gameObject);
            
                cols[i].GetComponent<Rigidbody>().AddForce(direction * damage * (1 - (dist / radio)));
                // print("impact");
                //cols[i].GetComponent<Rigidbody>().AddExplosionForce(expPower, transform.position, expRadius, 1.5f, ForceMode.Impulse);
                //cols[i].rigidbody.AddForce(direction * expPower * (1 - (dist / expRadius)));
            }*/

            if (cols[i].gameObject.layer == K.LAYER_IA)
            {
				print(cols[i].gameObject);
                cols[i].GetComponent<IAController>().Damage(damage);
                
               
                //cols[i].gameObject.transform.parent.gameObject.GetComponent<IAController>().Damage(damage);

                Destroy(this);
            }
        }

        if (col.gameObject.tag == "Target")
            Destroy(this.gameObject);
    }
}
