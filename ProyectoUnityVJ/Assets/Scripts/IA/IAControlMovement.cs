using UnityEngine;
using System.Collections;
using System;

public class IAControlMovement //: Vehicle
{
    //public Vector3 centerOfMass;
    //public WheelCollider wheelFL;
    //public WheelCollider wheelFR;
    //public WheelCollider wheelBL;
    //public WheelCollider wheelBR;
    //public Transform currentTarget;
    //public Transform frontSensor;
    //public Transform rightSensor;
    //public Transform leftSensor;
    //public Transform frontRightSensor;
    //public Transform frontLeftSensor;
    //public Transform angleRightSensor;
    //public Transform angleLeftSensor;
    //public float maxSteer;
    //public float maxTorque;
    //public float distancePath;
    //public float currentSpeed;
    //public float maxSpeed;
    //public float breakingSpeed;
    //public float sensorsLength;
    //public float evadeSpeed;
    //public float waitToReverse = 2.5f;
    //public float reverseFor = 1.5f;

    //private float _reverseCounter;
    //private Vector3 _steerVector;
    //private float _steer;
    //private int _evade;
    //private bool _reverse;
    //private Rigidbody _rb;

    //public override void GetInput(float _accel, float _brake,float _handbrake, float _steer, float _nitro)
    //{

    //}

    //// Use this for initialization
    //protected override void Start()
    //{
    //    base.Start();

    //    _rb = GetComponent<Rigidbody>();
    //    _rb.centerOfMass = centerOfMass;
    //}

    //void FixedUpdate()
    //{

    //    if (_evade == 0)
    //        GetSteering();

    //    Movement();
    //    SensorsIA();

    //}

    //private void Movement()
    //{
    //    //transforma la informacion de la rueda en km/h 
    //    currentSpeed = Mathf.Round(2 * Mathf.PI * wheelBR.radius * wheelBR.rpm * 60 / 1000);
    //    if (currentSpeed <= maxSpeed)
    //    {
    //        if (!_reverse)
    //        {
    //            wheelBR.motorTorque = maxTorque;
    //            wheelBL.motorTorque = maxTorque;
    //        }
    //        else
    //        {
    //            wheelBR.motorTorque = -maxTorque;
    //            wheelBL.motorTorque = -maxTorque;
    //        }
    //    }
    //    else
    //    {
    //        wheelBR.motorTorque = 0;
    //        wheelBL.motorTorque = 0;
    //    }

    //}

    //private void GetSteering()
    //{
    //    Vector3 steerVector = transform.InverseTransformPoint(currentTarget.position.x, transform.position.y, currentTarget.position.z);
    //    float steer = (steerVector.x / steerVector.magnitude);

    //    steer *= maxSteer;
    //    wheelFL.steerAngle = steer;
    //    wheelFR.steerAngle = steer;

    //}

    //private void SensorsIA()
    //{
    //    _evade = 0;
    //    RaycastHit hit;
    //    float evadeSensitivity = 0;

    //    //Brake
    //    if (Physics.Raycast(frontSensor.position, frontSensor.forward, out hit, sensorsLength))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _evade++;
    //            wheelBR.brakeTorque = breakingSpeed;
    //            wheelBL.brakeTorque = breakingSpeed;
    //            Debug.DrawLine(frontSensor.position, hit.normal, Color.blue);
    //        }
    //    }
    //    else
    //    {
    //        wheelBR.brakeTorque = 0;
    //        wheelBL.brakeTorque = 0;
    //    }

    //    //FrontRightSensor
    //    if (Physics.Raycast(frontRightSensor.position, angleRightSensor.forward, out hit, sensorsLength))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _evade++;
    //            evadeSensitivity -= 1;
    //            print(hit.normal.x);

    //            Debug.DrawLine(frontRightSensor.position, hit.point, Color.white);
    //        }
    //    }
    //    else if (Physics.Raycast(angleRightSensor.position, angleRightSensor.forward, out hit, sensorsLength))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _evade++;
    //            evadeSensitivity -= 0.5f;
    //            print(hit.normal.x);
    //            Debug.DrawLine(angleRightSensor.position, hit.point, Color.white);
    //        }
    //    }


    //    //FrontLeftSensor
    //    if (Physics.Raycast(frontLeftSensor.position, frontLeftSensor.forward, out hit, sensorsLength))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _evade++;
    //            evadeSensitivity += 1;

    //            print(hit.normal.x);
    //            Debug.DrawLine(frontLeftSensor.position, hit.point, Color.white);
    //        }
    //    }
    //    else if (Physics.Raycast(angleLeftSensor.position, angleLeftSensor.forward, out hit, sensorsLength))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _evade++;
    //            evadeSensitivity += 0.5f;
    //            print(hit.normal.x);
    //            Debug.DrawLine(angleLeftSensor.position, hit.point, Color.white);
    //        }
    //    }


    //    //RightSensor
    //    if (Physics.Raycast(rightSensor.position, rightSensor.forward, out hit, sensorsLength))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _evade++;
    //            evadeSensitivity -= 0.5f;
    //           // print(hit.normal.x);

    //            Debug.DrawLine(rightSensor.position, hit.point, Color.green);
    //        }
    //    }

    //    //LeftSensor
    //    if (Physics.Raycast(leftSensor.position, leftSensor.forward, out hit, sensorsLength))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _evade++;
    //            evadeSensitivity += 0.5f;

    //            //print(hit.normal.x);
    //            Debug.DrawLine(leftSensor.position, hit.point, Color.green);
    //        }
    //    }

    //    if (evadeSensitivity == 0)
    //    {
    //        if (Physics.Raycast(frontSensor.position, frontSensor.forward, out hit, sensorsLength))
    //        {
    //            if (hit.transform.tag != "Track" && hit.normal.x < 0)
    //                evadeSensitivity = -1;
    //            else
    //                evadeSensitivity = 1;
    //        }
    //    }

    //    if (_rb.velocity.magnitude < 2 && !_reverse)
    //    {
    //        _reverseCounter += Time.deltaTime;
    //        if (_reverseCounter >= waitToReverse)
    //        {
    //            _reverseCounter = 0;
    //            _reverse = true;
    //        }
    //    }
    //    else if (!_reverse)
    //        _reverseCounter = 0;

    //    if (_reverse)
    //    {
    //        print("reverse " + _evade);
    //        evadeSensitivity *= -1;
    //        _reverseCounter += Time.deltaTime;
    //        if (_reverseCounter >= reverseFor)
    //        {
    //            _reverseCounter = 0;
    //            _reverse = false;
    //        }
    //    }


    //    if (_evade != 0)
    //    {
    //        EvadeSteering(evadeSensitivity);
    //    }
    //}

    //private void EvadeSteering(float sensitivity)
    //{
    //   // print("evadeSteer " + sensitivity * evadeSpeed);

    //    wheelFL.steerAngle = sensitivity * evadeSpeed;
    //    wheelFR.steerAngle = sensitivity * evadeSpeed;
    //}

    //public void ChangeTargetMovement(Transform target)
    //{
    //    currentTarget = target;
    //}
}
