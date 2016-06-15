using UnityEngine;
using System.Collections;

public class SightTrigger : MonoBehaviour
{
    private IAController myController;

	// Use this for initialization
	void Start ()
    {
        myController = this.GetComponentInParent<IAController>();
	
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider col)
    {
        if (col.gameObject.GetComponentInParent<Vehicle>() != null) 
           myController.EnemySee();
	}
}
