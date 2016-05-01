using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleController : MonoBehaviour, IVehicle
{
    public float maxTorque;
    public Transform centerOfMass;
    public Text speedText;
    public GameObject trailRenderModel;
    public WheelCollider[] wheelColliders;
    public Transform[] tiresCar;
    public float lapCount { get; private set; }

    private Rigidbody _rb;
    private float currentSpeed;
    private float acceleration;
    public float maxSpeed;
    public float maxReverseSpeed;
    private float steer = 0;
    private float throttle = 0;
    public int maximumTurn = 15;
    public int minimumTurn = 10;
    public Vector3 dragMultiplier;
    private bool handbrake;
    private float resetTimer;
    public float resetTime;
    public float stuckMaxDist;
    public LayerMask layer;
    public float fallForce = 10000;
    public GameObject carModel;
    private float finalAngle;
    private bool _isGrounded;
    private Checkpoint _lastCheckpoint;

    public float antiRoll = 1000f;
    private bool impulseForceLeft;
    private bool impulseForceRight;
    private float antiRollForceRight;
    private float antiRollForceLeft;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass.localPosition;
        handbrake = false;
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateTiresPosition();
        UIText();
        currentSpeed = _rb.velocity.magnitude * 3f;

        GetInput();
        if (Input.GetKeyUp(KeyCode.R)) ResetCar();

        CheckCarFlipped();

        CheckIfGrounded();
    }

    void FixedUpdate()
    {
        //Transforma una dirección de world space a local space.
        var relativeVelocity = _rb.transform.InverseTransformDirection(_rb.velocity);

        acceleration = _rb.transform.InverseTransformDirection(_rb.velocity).z;

        //   Debug.Log(acceleration);

        //Maniobrabilidad
        ApplySteering(relativeVelocity);

        //ADDFORCE de Deslizamiento calculando dirección.
        UpdateDrag(relativeVelocity);

      //  FallSpeed();
      //  BlockCarRotation();

        //Anti vuelco del vehículo
        AntiRollBars();

    }

    private void GetInput()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        finalAngle = steer * K.JEEP_MAX_STEERING_ANGLE;

        //   wheelColliders[0].steerAngle = finalAngle;
        //     wheelColliders[1].steerAngle = finalAngle;

        if (_isGrounded) for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].motorTorque = throttle * maxTorque;

        if (currentSpeed > maxSpeed && throttle > 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
        }
        else if (currentSpeed > maxReverseSpeed && throttle < 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 0;
        }

        if (throttle == 0) _rb.drag = _rb.velocity.magnitude / 100f;
        else _rb.drag = 0f;
        CheckHandbrake();
    }
    public void AntiRollBars()
    {
        WheelHit leftWheelHit;
        WheelHit rightWheelHit;
        float travelLeft = 1f;
        float travelRight = 1f;

        for (int i = 0; i < wheelColliders.Length; i++)
        {
            if (i == 1|| i== 3)
            {
                bool groundedLeft = wheelColliders[i].GetGroundHit(out leftWheelHit);
                if (groundedLeft)
                {
                    travelLeft = (-wheelColliders[i].transform.InverseTransformPoint(leftWheelHit.point).y - wheelColliders[i].radius) / wheelColliders[i].suspensionDistance;
                    antiRollForceLeft = (travelLeft - travelRight) * antiRoll;
                    impulseForceLeft = false;
                }

                if (!groundedLeft && !impulseForceLeft)
                {
                    _rb.AddForceAtPosition(wheelColliders[i].transform.up * -antiRollForceLeft, wheelColliders[i].transform.position);
                    impulseForceLeft = true;
                    Debug.Log("ANTIROLL BAR LEFT");
                }
            }
            if(i== 0 || i ==2)
            {
                bool groundedRight = wheelColliders[i].GetGroundHit(out rightWheelHit);
                if (groundedRight)
                {
                    travelRight = (-wheelColliders[i].transform.InverseTransformPoint(rightWheelHit.point).y - wheelColliders[i].radius) / wheelColliders[i].suspensionDistance;
                    antiRollForceRight = (travelLeft - travelRight) * antiRoll;
                    impulseForceRight = false;                
                }

                if (!groundedRight && !impulseForceRight)
                {
                    _rb.AddForceAtPosition(wheelColliders[i].transform.up * antiRollForceRight, wheelColliders[i].transform.position);
                    impulseForceRight = true;
                    Debug.Log("ANTIROLL BAR RIGHT");
                }
            }
        }
    }
    private void CheckHandbrake()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S) && acceleration > 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 7000;
            dragMultiplier.z += 10 * Time.deltaTime;
            handbrake = true;
        }
        else if (Input.GetKey(KeyCode.S) && acceleration > 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
            //    dragMultiplier.z += 10 * Time.deltaTime;
            dragMultiplier.z += 2 * Time.deltaTime;
            handbrake = true;
        }
        else
        {
            dragMultiplier.z = 0;
            handbrake = false;
        }
    }
    private void UpdateDrag(Vector3 relativeVelocity)
    {
        Vector3 relativeDrag = new Vector3(-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), -relativeVelocity.y * Mathf.Abs(relativeVelocity.y),
                                            -relativeVelocity.z * Mathf.Abs(relativeVelocity.z));

        var drag = Vector3.Scale(dragMultiplier, relativeDrag);

        if (!handbrake) drag.x *= maxSpeed / relativeVelocity.magnitude;

        if (Mathf.Abs(relativeVelocity.x) < 5 && !handbrake) drag.x = -relativeVelocity.x * dragMultiplier.x;
        _rb.AddForce(transform.TransformDirection(drag) * _rb.mass * Time.deltaTime);
    }
    private void ApplySteering(Vector3 relativeVelocity)
    {
        float turnRadius = 3f / Mathf.Sin((90 - (steer * 30)) * Mathf.Deg2Rad);
        float minMaxTurn = EvaluateSpeedToTurn(_rb.velocity.magnitude);
        float turnSpeed = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
        transform.RotateAround(transform.position + transform.right * turnRadius * steer, transform.up,
                                turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steer);
    }
    private float EvaluateSpeedToTurn(float speed)
    {
        if (speed > maxSpeed / 2) return minimumTurn;
        var speedIndex = 1 - (speed / (maxSpeed / 2));

        return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
    }
    private void CheckCarFlipped()
    {
        if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270 && currentSpeed < 1 && currentSpeed > -1) resetTimer += Time.deltaTime;
        else resetTimer = 0;
        if (resetTimer > resetTime) FlipCar();
    }

    private void FlipCar()
    {
        transform.rotation = Quaternion.LookRotation(transform.forward);
        transform.position += Vector3.up * 0.5f;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        resetTimer = 0;
    }
    */

    /// <summary>
    /// Obtengo el ultimo checkpoint por el que pase.
    /// </summary>
    /// <param name="chk">Checkpoint</param>
    public void SetCheckpoint(Checkpoint chk)
    {
        _lastCheckpoint = chk;
        print(_lastCheckpoint.gameObject);
        lapCount += CheckpointManager.instance.checkpointValue;
    }

    private void ResetCar()
    {
        _rb.velocity = Vector3.zero;
        transform.position = _lastCheckpoint.GetRespawnPoint(transform.position) + Vector3.up;
        transform.rotation = _lastCheckpoint.transform.rotation;

        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, layer))
        {
            if (hit.distance < 5f)
            {
                transform.rotation = Quaternion.LookRotation(transform.forward);

                if (Physics.Raycast(transform.position + transform.up, transform.forward, stuckMaxDist))
                {
                    print("forward");
                    transform.Translate(-10, 0, -10);
                }
                else if (Physics.Raycast(transform.position + transform.up, transform.right, stuckMaxDist))
                    transform.Translate(-10, 0, 0);
                else if (Physics.Raycast(transform.position + transform.up, -transform.right, stuckMaxDist))
                    transform.Translate(10, 0, 0);

                transform.rotation = Quaternion.LookRotation(transform.forward);
                transform.position += Vector3.up * 0.5f;
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }*/
    }

    private void UpdateTiresPosition()
    {
        for (int i = 0; i < tiresCar.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tiresCar[i].position = pos;
            tiresCar[i].rotation = quat;
            if (i < 2f)
            {
                Vector3 steerAngle = tiresCar[i].localEulerAngles;
                steerAngle.y = finalAngle;
                tiresCar[i].localEulerAngles = steerAngle;
            }
        }
    }

    /// <summary>
    /// Checkea si el vehiculo esta tocando el piso.
    /// </summary>
    protected void CheckIfGrounded()
    {
        Ray ray = new Ray(centerOfMass.transform.position, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit hit;
        _isGrounded = false;
        if (Physics.Raycast(ray, out hit, 1))
        {
            if (hit.collider.gameObject.layer == K.LAYER_GROUND)
            {
                _isGrounded = true;
            }
        }
    }

    /// <summary>
    /// Hace que el auto caiga mas rapido en los saltos.
    /// </summary>
    protected void FallSpeed()
    {
        if (!_isGrounded)
        {
            _rb.AddForce(-transform.up * fallForce);
        }
    }

    /// <summary>
    /// Impide que el auto se de vuelta.
    /// </summary>
    protected void BlockCarRotation()
    {

        if (transform.eulerAngles.z > 20 && transform.eulerAngles.z <= 180)
        {
            //Debug.Log("rotar a dcha");
            _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddTorque(carModel.transform.forward * K.JEEP_ROTATION_FORCE);
        }
        else if (transform.eulerAngles.z < 340 && transform.eulerAngles.z > 180)
        {
            //Debug.Log("rotar a izq");
            _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddTorque(-carModel.transform.forward * K.JEEP_ROTATION_FORCE);
        }

        if (transform.eulerAngles.x > 20 && transform.eulerAngles.x <= 180)
        {
            //Debug.Log("rotar a arriba");
            _rb.constraints = RigidbodyConstraints.FreezeRotationX;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddTorque(carModel.transform.forward * K.JEEP_ROTATION_FORCE);
        }
        else if (transform.eulerAngles.x < 340 && transform.eulerAngles.x > 180)
        {
            //Debug.Log("rotar a abajo");
            _rb.constraints = RigidbodyConstraints.FreezeRotationX;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddTorque(-carModel.transform.forward * K.JEEP_ROTATION_FORCE);
        }

    }

    protected void UIText()
    {
        //_localVelocity = transform.InverseTransformDirection(_rb.velocity);
        //speedText.text = "Speed: " + (int)currentSpeed;
        IngameUIManager.instance.GetPlayerSpeed(currentSpeed / K.SPEEDOMETER_MAX_SPEED);
        IngameUIManager.instance.GetPlayerLapCount(Mathf.FloorToInt(lapCount));
    }
}
