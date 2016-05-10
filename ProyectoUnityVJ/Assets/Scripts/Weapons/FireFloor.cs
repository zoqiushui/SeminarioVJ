using UnityEngine;
using System.Collections;

public class FireFloor : MonoBehaviour {

    public float staydamage = 0.01f;
    public float duration = 2f;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, duration);
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_IA)
        {
            print(col.gameObject);
            col.GetComponent<IAController>().Damage(staydamage * 20);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.layer == K.LAYER_IA)
        {
            print(col.gameObject);
            col.GetComponent<IAController>().Damage(staydamage);
        }
    }
}
