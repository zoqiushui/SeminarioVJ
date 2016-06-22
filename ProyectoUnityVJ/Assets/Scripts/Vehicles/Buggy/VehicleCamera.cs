using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class VehicleCamera : MonoBehaviour
{
    //public RawImage crosshair;
    public Transform target = null;
    //private float _height;
    //private float _distanceHeight;
    private Rigidbody _rbTarget;
    //public float rotationDamping = 3f;
    public float minFOV = 50f;
    public float maxFOVGround = 70f;
    public float maxFOVAir = 90f;
    //private Vector3 _crosshairFixedZPostion;
    private float _maxFOV;

    void Awake()
    {
        if (!target) return;
        _rbTarget = target.GetComponent<Rigidbody>();
        // _crosshairFixedZPostion = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
    }

    /*private void Update()
    {
        _crosshairFixedZPostion.x = Input.mousePosition.x;
        _crosshairFixedZPostion.y = Input.mousePosition.y;
        crosshair.transform.position = _crosshairFixedZPostion;
    }*/
    void LateUpdate()
    {
        if (!target || !_rbTarget) return;

        float speed = (_rbTarget.transform.InverseTransformDirection(_rbTarget.velocity).z) * K.KPH_TO_MPS_MULTIPLIER;

        //float speedFactor = Mathf.Clamp01(_rbTarget.velocity.magnitude / 70);
        float speedFactor = Mathf.Clamp01(speed / target.GetComponent<Vehicle>().topSpeed);
        Camera.main.fieldOfView = Mathf.Lerp(minFOV, CalculateMaxFov(), speedFactor);        
    }

    private float CalculateMaxFov()
    {
        if (target.GetComponent<Vehicle>().isGrounded)
        {
            if (_maxFOV > maxFOVGround)
            {
                _maxFOV--;
            }
            else
            {
                _maxFOV++;
            }
            _maxFOV = Mathf.Clamp(_maxFOV, minFOV, maxFOVGround);
        }
        else
        {
            if (_maxFOV > maxFOVAir)
            {
                _maxFOV--;
            }
            else
            {
                _maxFOV++;
            }
            _maxFOV = Mathf.Clamp(_maxFOV, minFOV, maxFOVAir);
        }
        return _maxFOV;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.transform.position + transform.up * 3);
    }
}
