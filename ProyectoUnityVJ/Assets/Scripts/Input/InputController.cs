using UnityEngine;
using System.Collections;

public abstract class InputController : MonoBehaviour
{
    protected Vehicle _vehicleReference;
    protected float _accel, _brake, _handbrake, _steer, _nitro;

    protected void Start()
    {
        _vehicleReference = GetComponent<Vehicle>();
    }

    protected virtual void FixedUpdate()
    {
        //print("aceleracion input: " + _accel + " stering input: " + _steer);
        _vehicleReference.Move(_accel, _brake, _handbrake, _steer, _nitro);
    }
}
