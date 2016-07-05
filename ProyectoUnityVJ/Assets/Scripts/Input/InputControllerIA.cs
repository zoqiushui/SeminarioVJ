using UnityEngine;
using System.Collections;
using System;

public class InputControllerIA : InputController
{

    public Transform frontSensor;
    public Transform rightSensor;
    public Transform leftSensor;
    public Transform frontRightSensor;
    public Transform frontLeftSensor;
    public Transform angleRightSensor;
    public Transform angleLeftSensor;
    public float sensorsDistance;
    private float _torqueInput;
    private float _steerInput;
    private float _brakeInput;
    private Vector3 _destinationPoint;

    protected override void Start()
    {
        base.Start();
        StartCoroutine("SensorsCalculator");

    }

    protected override void FixedUpdate()
    {
        //
        // TODO: Cambiar valores accel, brake, steer.... 
        //
//        SteeringToTarget();
  //      Sensors();
        _steer = Mathf.Clamp(_steerInput, -1, 1);
        _accel = Mathf.Clamp(_torqueInput, 0.5f, 1);
        base.FixedUpdate();
    }


    IEnumerator SensorsCalculator()
    {
        SteeringToTarget();
        Sensors();
        yield return new WaitForSeconds(0.05f);
        yield return StartCoroutine("SensorsCalculator");
    }


    private void SteeringToTarget()
    {
        Vector3 steerVector = transform.InverseTransformPoint(_destinationPoint.x, transform.position.y, _destinationPoint.z);
        _steerInput = (steerVector.x / steerVector.magnitude);
    }

    public void SetDestinationPoint(Vector3 v)
    {
        _destinationPoint = v;
    }

    private void Sensors()
    {
        _torqueInput = 1;
        _steerInput = 0;
        _brakeInput = 0;
        RaycastHit hit;
        /*
        //Brake
        if (Physics.Raycast(frontSensor.position, frontSensor.forward, out hit, sensorsDistance / 2))
        {
            if (hit.transform.tag != "Track")
            {
                _brakeInput = -1;
                Debug.DrawLine(frontSensor.position, hit.normal, Color.blue);
            }
        }
        */
        //FrontRightSensor
        if (Physics.Raycast(frontRightSensor.position, angleRightSensor.forward, out hit, sensorsDistance * 2))
        {
            
            if (hit.collider.gameObject.layer != K.LAYER_GROUND)
            {
                _torqueInput /= 2;
                _steerInput -= 1f;
            }
        }
        else if (Physics.Raycast(angleRightSensor.position, angleRightSensor.forward, out hit, sensorsDistance))
        {
            if (hit.collider.gameObject.layer != K.LAYER_GROUND)
            {
                _torqueInput /= 2;
                _steerInput -= 0.5f;
            }
        }


        //FrontLeftSensor
        if (Physics.Raycast(frontLeftSensor.position, frontLeftSensor.forward, out hit, sensorsDistance * 2))
        {
            if (hit.collider.gameObject.layer != K.LAYER_GROUND)
            {
                _torqueInput /= 2;
                _steerInput += 1f;
                
            }
        }
        else if (Physics.Raycast(angleLeftSensor.position, angleLeftSensor.forward, out hit, sensorsDistance))
        {
            if (hit.collider.gameObject.layer != K.LAYER_GROUND)
            {
                _torqueInput /= 2;
                _steerInput += 5f;
            }
        }

        if (_steerInput == 0)
            SteeringToTarget();

        if (_brakeInput == -1)
        {
            _torqueInput = 0;
            _steerInput *= -1;
        }
    }
    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if(_destinationPoint != null)
            Gizmos.DrawWireSphere(_destinationPoint, 5f);
    }*/
}