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
        if (destructibleElement.layer == K.LAYER_DESTRUCTIBLE) childsRB = destructibleElement.GetComponentsInChildren<Rigidbody>();

    }
	void Start ()
    {
	}
	
	void Update ()
    {
	
	}

    private void Explode(float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        foreach (var rb in childsRB) rb.AddExplosionForce(10000,transform.position,5);
    }
   private void OnCollisionEnter(Collision coll)
   {
       if (coll.gameObject.layer == K.LAYER_PLAYER && coll.gameObject.GetComponentInParent<Vehicle>().currentVelZ > 20)
       {
           Destroy(this.gameObject);
           var newElement = (GameObject)Instantiate(destructibleElement, transform.position, transform.rotation);

           Transform vehiclePosition = coll.gameObject.transform;
           Explode(explosionForce, vehiclePosition.position, explosionRadius);

           Destroy(newElement, 3);

       }
   }


}
