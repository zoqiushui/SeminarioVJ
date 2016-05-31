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
            if (other.gameObject.GetComponent<JeepController>() != null) other.gameObject.GetComponent<JeepController>().PushRamp(forceRamp);
            else other.gameObject.GetComponentInParent<VehicleController>().PushRamp(forceRamp);

        }


    }
}
