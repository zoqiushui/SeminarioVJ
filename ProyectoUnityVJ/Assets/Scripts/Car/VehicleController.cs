using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class VehicleController : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float accelerationRate;
    public float decelerationRate;
    public float maxSteerAngle;
    public float steerSpeed;
    public float speed;
    public List<WheelPair> wheelPairList;
    public Text speedText; // PARA TEST
    public GameObject trailRenderModel;

    protected Rigidbody _rb;
    protected Vector3 _localVelocity;
    protected bool _isGrounded;
    protected Vector3 _wheelRotation;
    protected Vector3 _vehicleDirection;
    protected List<GameObject> _trailRenderList;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        speed = 0;
        _trailRenderList = new List<GameObject>();
        foreach (var wheelP in wheelPairList)
        {
            var go = GameObject.Instantiate<GameObject>(trailRenderModel);
            go.transform.position = wheelP.leftWheel.transform.position - transform.up * 1.7f;
            go.transform.parent = wheelP.leftWheel.transform;
            _trailRenderList.Add(go);
            go = GameObject.Instantiate<GameObject>(trailRenderModel);
            go.transform.position = wheelP.rightWheel.transform.position - transform.up * 1.7f;
            go.transform.parent = wheelP.rightWheel.transform;
            _trailRenderList.Add(go);
        }
    }

    protected void Update()
    {
        UIText();
        CheckIfGrounded();
        ApplyDownforce();
        WheelGraphics();
        Move();
    }

    /*protected void FixedUpdate()
    {
        _rb.AddRelativeForce(transform.forward * (_rb.mass * speed));
    }*/

    /// <summary>
    /// Checkea si el vehiculo esta tocando el piso.
    /// </summary>
    protected void CheckIfGrounded()
    {
        _isGrounded = false;
        foreach (var wheelP in wheelPairList)
        {
            Ray ray = new Ray(wheelP.leftWheel.transform.position, -Vector3.up);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * K.IS_GROUNDED_RAYCAST_DISTANCE, Color.red);
            if (Physics.Raycast(ray, out hit, K.IS_GROUNDED_RAYCAST_DISTANCE))
            {
                if (hit.collider.gameObject.layer == K.LAYER_GROUND)
                {
                    _isGrounded = true;
                    break;
                }
            }
            ray = new Ray(wheelP.rightWheel.transform.position, -Vector3.up);
            Debug.DrawRay(ray.origin, ray.direction * K.IS_GROUNDED_RAYCAST_DISTANCE, Color.red);
            if (Physics.Raycast(ray, out hit, K.IS_GROUNDED_RAYCAST_DISTANCE))
            {
                if (hit.collider.gameObject.layer == K.LAYER_GROUND)
                {
                    _isGrounded = true;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Gravedad.
    /// </summary>
    protected void ApplyDownforce()
    {
        if (!_isGrounded)
        {
            transform.position += -Vector3.up * K.GRAVITY * Time.deltaTime;
        }
    }

    /// <summary>
    /// Rotacion de las ruedas y huellas.
    /// </summary>
    protected void WheelGraphics()
    {
        if (_isGrounded)
        {
            foreach (var trail in _trailRenderList)
            {
                trail.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var trail in _trailRenderList)
            {
                trail.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _wheelRotation.y = 0;
        }
        _wheelRotation += Input.GetAxis("Horizontal") * transform.up * (steerSpeed * 50) * Time.deltaTime;
        _wheelRotation.y = Mathf.Clamp(_wheelRotation.y, -maxSteerAngle, maxSteerAngle);
        //_vehicleDirection = Vector3.zero;
        foreach (var wheelP in wheelPairList)
        {
            if (wheelP.steer)
            {
                wheelP.leftWheel.transform.localEulerAngles = _wheelRotation;
                wheelP.rightWheel.transform.localEulerAngles = _wheelRotation;
                /*_vehicleDirection += wheelP.leftWheel.transform.position + wheelP.leftWheel.transform.forward * 10;
                _vehicleDirection += wheelP.rightWheel.transform.position + wheelP.rightWheel.transform.forward * 10;
                _vehicleDirection /= 2;
                _vehicleDirection += transform.forward;*/
            }
        }
    }

    /// <summary>
    /// Mueve el vehiculo.
    /// </summary>
    protected void Move()
    {
        if (_isGrounded)
        {
            if (Input.GetKey(KeyCode.W) && speed < maxSpeed)
            {
                speed += accelerationRate * Time.deltaTime;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
            }
            else if (Input.GetKey(KeyCode.S) && speed > 0)
            {
                speed -= decelerationRate * K.JEEP_BRAKE * Time.deltaTime;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
            }
            else if (Input.GetKey(KeyCode.S) && speed <= 0)
            {
                speed -= accelerationRate * 0.5f * Time.deltaTime;
                speed = Mathf.Clamp(speed, -maxSpeed * 0.4f, 0);
            }
            else if (speed <= 0)
            {
                speed += decelerationRate * Time.deltaTime;
                speed = Mathf.Clamp(speed, -maxSpeed * 0.4f, 0);
            }
            else
            {
                speed -= decelerationRate * Time.deltaTime;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
            }


            if (speed > 1)
            {

                transform.forward += transform.right * Mathf.Clamp(Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime, -maxSteerAngle, maxSteerAngle);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else if (speed < -1)
            {
                transform.forward -= transform.right * Mathf.Clamp(Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime, -maxSteerAngle, maxSteerAngle);
                transform.position += transform.forward * speed * Time.deltaTime;
            }

        }
    }

    /// <summary>
    /// Muestra en pantalla la velocidad (BORRAR).
    /// </summary>
    protected void UIText()
    {
        //_localVelocity = transform.InverseTransformDirection(_rb.velocity);
        speedText.text = "Speed: " + (int)speed;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_vehicleDirection, 0.2f);
    }
}
