using UnityEngine;
using System.Collections;
using System;

public class InputControllerPlayer : InputController
{
    protected override void FixedUpdate()
    {
        _accel = Input.GetAxis(K.INPUT_VERTICAL);
        _brake = Input.GetAxis(K.INPUT_VERTICAL);
        _steer = Input.GetAxis(K.INPUT_HORIZONTAL);
        _handbrake = Input.GetAxis(K.INPUT_HANDBRAKE);
        _nitro = Input.GetAxis(K.INPUT_NITRO);
        base.FixedUpdate();
    }
}
