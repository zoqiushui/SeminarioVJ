using UnityEngine;
using System.Collections;

public class DestructibleElement : MonoBehaviour
{
    public GameObject destructibleElement;
    private Rigidbody[] childsRB;
    public float explosionForce;
    public float explosionRadius;

    void Awake()
    {
       if (destructibleElement != null)
       {
           if (destructibleElement.layer == K.LAYER_DESTRUCTIBLE) childsRB = destructibleElement.GetComponentsInChildren<Rigidbody>();
       }

    }
	void Start ()
    {
	}
	
	void Update ()
    {
	
	}

    private void Explode(float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        foreach (var rb in childsRB) rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
    }
   private void OnCollisionEnter(Collision coll)
   {
       if (coll.gameObject.layer == K.LAYER_PLAYER && coll.gameObject.GetComponentInParent<Vehicle>().currentVelZ > 20 ||
           coll.gameObject.layer == K.LAYER_MISSILE || coll.gameObject.layer == K.LAYER_IA)
       {
           Destroy(this.gameObject);
           var newElement = (GameObject)Instantiate(destructibleElement, transform.position, transform.rotation);

           Explode(explosionForce, transform.position, explosionRadius);

           Destroy(newElement, 3);
       }
   }
   void OnTriggerEnter(Collider coll)
   {
       print("DRONE " + coll.gameObject.layer);
    /*   if (coll.gameObject.layer == K.LAYER_MISSILE && GetComponent<Animation>() != null)
       {
           Destroy(gameObject);
           transform.parent.transform.parent.gameObject.GetComponentInChildren<Animation>().enabled = false;
           transform.parent.transform.parent.gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
           transform.parent.transform.parent.gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
           transform.parent.transform.parent.gameObject.GetComponentInChildren<ConstantForce>().force = new Vector3(0, -100, 0);
           Instantiate(destructibleElement, transform.position, transform.rotation);
       }*/
       if (coll.gameObject.layer == K.LAYER_IA)
       {
           coll.GetComponentInParent<IAController>().Damage(100f);
           Destroy(transform.parent.gameObject, 5);
           Instantiate(destructibleElement, transform.position, transform.rotation);
       }
       else Destroy(transform.parent.gameObject, 5);          
   }

   public void DestroyDrone()
   {
       Destroy(gameObject);
       if (transform.parent.transform.parent.gameObject.GetComponentInChildren<Rigidbody>() != null)
       {
           transform.parent.transform.parent.gameObject.GetComponentInChildren<Animation>().enabled = false;
           transform.parent.transform.parent.gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
           transform.parent.transform.parent.gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
           transform.parent.transform.parent.gameObject.GetComponentInChildren<ConstantForce>().force = new Vector3(0, -100, 0);
           Instantiate(destructibleElement, transform.position, transform.rotation);
       }
   }
}
