using UnityEngine;
using System.Collections;
using System;

public class InputControllerPlayer : InputController
{
    protected override void FixedUpdate()
    {
        _accel = Mathf.Clamp(Input.GetAxis(K.INPUT_VERTICAL), 0, 1);
        _brake = Mathf.Clamp(Input.GetAxis(K.INPUT_VERTICAL), -1, 0);
        _steer = Input.GetAxis(K.INPUT_HORIZONTAL);
        _handbrake = Input.GetAxis(K.INPUT_HANDBRAKE);
        _nitro = Input.GetAxis(K.INPUT_NITRO);
        base.FixedUpdate();
    }
}
