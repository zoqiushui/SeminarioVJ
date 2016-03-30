using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleController : MonoBehaviour 
{
    public List<WheelPair> wheelPairs;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    private void Start()
    {
        maxMotorTorque = K.MAX_MOTOR_TORQUE;
        maxSteeringAngle = K.MAX_STEERING_ANGLE;
    }

    public void ApplyLocalPositionToVisuals(WheelPair wheelP)
    {
        Vector3 positionLeft;
        Vector3 positionRight;
        Quaternion rotationLeft;
        Quaternion rotationRight;

        wheelP.leftWheel.GetWorldPose(out positionLeft,out rotationLeft);
        wheelP.rightWheel.GetWorldPose(out positionRight,out rotationRight);

        wheelP.leftWheelMesh.transform.position = positionLeft;
        wheelP.rightWheelMesh.transform.position = positionRight;

        wheelP.leftWheelMesh.transform.rotation = rotationLeft;
        wheelP.rightWheelMesh.transform.rotation = rotationRight;
    }

    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        foreach (WheelPair wheelP in wheelPairs)
        {
            if (wheelP.steer)
            {
                wheelP.leftWheel.steerAngle = steering;
                wheelP.rightWheel.steerAngle = steering;
            }
            if (wheelP.motor)
            {
                wheelP.leftWheel.motorTorque = motor;
                wheelP.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(wheelP);
        }        
    }
}
