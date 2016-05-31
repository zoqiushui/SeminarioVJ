using UnityEngine;
using System.Collections;

public abstract class InputController : MonoBehaviour
{
    protected Vehicle _vehicle;
    protected float _accel, _brake, _steer, _handbrake, _nitro;

    protected void Start()
    {
        _vehicle = GetComponent<Vehicle>();
    }

    protected virtual void FixedUpdate()
    {
        _vehicle.GetInput(_accel, _brake, _handbrake, _steer, _nitro);
    }
}
