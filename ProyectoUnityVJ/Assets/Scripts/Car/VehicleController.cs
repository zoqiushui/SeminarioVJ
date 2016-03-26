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

    private void Update()
    {

    }

    public void ApplyLocalPositionToVisuals(WheelPair wheelP)
    {
            //wheelP.leftWheelMesh.transform.Rotate(Vector3.right, Time.deltaTime * wheelP.leftWheel.rpm * 10, Space.Self);
            //wheelP.rightWheelMesh.transform.Rotate(Vector3.right, Time.deltaTime * wheelP.rightWheel.rpm * 10, Space.Self);          
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
