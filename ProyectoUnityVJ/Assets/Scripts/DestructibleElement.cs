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
}
