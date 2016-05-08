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
        if (other.gameObject.layer == K.LAYER_PLAYER) other.gameObject.GetComponent<VehicleController>().PushRamp(forceRamp);
    }
}
