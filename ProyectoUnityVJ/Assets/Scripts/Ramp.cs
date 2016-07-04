﻿using UnityEngine;
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
            if (other.gameObject.GetComponent<Vehicle>().currentVelZ > 20f)
            {
                if (other.gameObject.GetComponentInParent<InputControllerPlayer>() != null) other.gameObject.GetComponentInParent<Vehicle>().PushRamp(forceRamp);
                else if (other.gameObject.GetComponent<InputControllerIA>() != null) other.gameObject.GetComponent<Vehicle>().PushRamp(forceRamp);
            }
        }


    }
}
