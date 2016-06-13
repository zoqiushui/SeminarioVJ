using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour
{
    public float forceRamp;
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER)
        {
            if (other.gameObject.GetComponentInParent<BuggyController>() != null) other.gameObject.GetComponentInParent<BuggyController>().PushRamp(forceRamp);
            else if (other.gameObject.GetComponent<IAVehicle>() != null) other.gameObject.GetComponent<IAVehicle>().PushRamp(forceRamp);

        }


    }
}
