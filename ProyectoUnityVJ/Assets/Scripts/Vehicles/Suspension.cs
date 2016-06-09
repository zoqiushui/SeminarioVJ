using UnityEngine;
using System.Collections;

public class Suspension : MonoBehaviour
{
    public float springConstant, damperConstant, restLenght;

    private float _previousLength, _currentLength, _springVelocity, _springForce, _damperForce;
    private Rigidbody _rb;
    private Vehicle _vehicleScript;
    private bool _isGrounded;

    private void Start()
    {
        _isGrounded = false;
        _rb = transform.parent.parent.parent.GetComponent<Rigidbody>();
        _vehicleScript = transform.parent.parent.parent.GetComponent<Vehicle>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, restLenght + _vehicleScript.wheelRadius))
        {
            _previousLength = _currentLength;
            _currentLength = restLenght - (hit.distance - _vehicleScript.wheelRadius);
            _springVelocity = (_currentLength - _previousLength) / Time.fixedDeltaTime;
            _springForce = springConstant * _currentLength;
            _damperForce = damperConstant * _springVelocity;
            _rb.AddForceAtPosition(transform.up * (_springForce + _damperForce), transform.position);
            if (hit.collider.gameObject.layer == K.LAYER_GROUND || hit.collider.gameObject.layer == K.LAYER_RAMP)
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }
        else
        {
            _isGrounded = false;
        }
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_vehicleScript) Gizmos.DrawLine(transform.position, transform.position + (restLenght + _vehicleScript.wheelRadius) * -Vector3.up);
    }
}
