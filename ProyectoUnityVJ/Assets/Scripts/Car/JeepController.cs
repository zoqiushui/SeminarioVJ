using UnityEngine;
using System.Collections;

public class JeepController : VehicleController 
{
    protected override void Start()
    {
        base.Start();
        maxSpeed = K.JEEP_MAX_SPEED;
        maxSteerAngle = K.JEEP_MAX_STEERING_ANGLE;
        steerSpeed = K.JEEP_STEER_SPEED;
        accelerationRate = K.JEEP_ACCELERATION_RATE;
        decelerationRate = K.JEEP_DECELERATION_RATE;
    }
}
