using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System;

public class BuggyController : Vehicle
{
    //public float maxTorque;
    //public Transform centerOfMass;
    //public Text speedText;
    //public Text wrongDirectionText;
    //public GameObject trailRenderModel;
    //public WheelCollider[] wheelColliders;
    //public float currentSpeed { get; private set; }

    //public Transform[] tiresCar;
    //private Rigidbody _rb;
    //private float acceleration;
    //public float maxSpeed;
    //public float maxReverseSpeed;
    //private float steerInput;
    //private float motorInput;
    //public int maximumTurn = 15;
    //public int minimumTurn = 10;
    //public Vector3 dragMultiplier;
    //private bool handbrake;
    //private float resetTimer;
    //public float resetTime;
    //public float stuckMaxDist;
    //public LayerMask layer;
    //public float fallForce = 10000;
    //public GameObject carModel;
    //private float finalAngle;
    //private bool _isGrounded;
    //private bool _isGroundedRamp;
    //private Checkpoint _lastCheckpoint;

    //public float antiRoll = 1000f;
    //public float brake;
    //private bool impulseForceLeft;
    //private bool impulseForceRight;
    //private bool impulseForceFront;
    //private bool impulseForceRear;
    //private float antiRollForceRight;
    //private float antiRollForceLeft;
    //private float antiRollForceFront;
    //private float antiRollForceRear;
    //private bool _reversing;
    //private int _checkpointNumber;

    //private bool modeNitro = false;
    //public float nitroPower;
    //public float nitroTimer;
    //private float _nitroTimer;
    //public float rechargeNitro;
    //private bool nitroEnd;
    //public Image visualNitro;
    //private float lapsEnded;
    //private bool canRechargeNitro;
    //private bool nitroEmpty;
    //private bool countInAir;
    //private float _timerWrongDirection;

    public Image visualNitro;
    public Text wrongDirectionText;

    ////public GameObject varManager;

    protected override void Start()
    {

        wrongDirectionText.gameObject.SetActive(false);
        vehicleName = PlayerPrefs.GetString("PilotName");
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        ChangeToRearView();
        CheckBars();
        CheckDirection();
        if (Input.GetKeyUp(KeyCode.R)) ResetCar();

    }

    private void CheckBars()
    {
        CheckNitroBar();
        RechargeNitro();
    }
    private void CheckNitroBar()
    {
        visualNitro.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_nitro = _nitroTimer / nitroTimer;
        visualNitro.fillAmount = calc_nitro;
    }

    
    private void RechargeNitro()
    {
        if (Mathf.FloorToInt(lapCount) == _lapsEnded)
        {
            _canRechargeNitro = true;
            _lapsEnded++;
        }

        if (!_modeNitro && _nitroTimer < nitroTimer && _canRechargeNitro) _nitroTimer += Time.deltaTime / rechargeNitro;
        if (visualNitro.fillAmount == 1)
        {
            _canRechargeNitro = false;
            _nitroEmpty = false;
        }
    }

    private void CheckDirection()
    {
        if (_lastCheckpoint)
        {
            var currentDirection = _lastCheckpoint.nextCheckpoint.transform.position - transform.position;
            if (Vector3.Angle(transform.forward, currentDirection) > 80)
            {
                _timerWrongDirection += Time.deltaTime;
            }
            else
            {
                wrongDirectionText.gameObject.SetActive(false);
                _timerWrongDirection = 0;
            }
        }
        if (_timerWrongDirection > 2)
        {
            wrongDirectionText.gameObject.SetActive(true);
        }

    }
    private void ChangeToRearView()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rearMirror.gameObject.SetActive(true);
            Camera.main.depth = -1f;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            rearMirror.gameObject.SetActive( false);
            Camera.main.depth = 0f;
        }
    }

    public override void Move(float accelInput, float brakeInput, float handbrakeInput, float steerInput, float nitroInput)
    {
        base.Move(accelInput, brakeInput, handbrakeInput, steerInput, nitroInput);
        NotifyObserver(K.OBS_MESSAGE_SPEED);
    }

    //    base.Start();
    //    wrongDirectionText.gameObject.SetActive(false);

    //    /*
    //    varManager = GameObject.FindGameObjectWithTag("VarManager");
    //    if (varManager!=null)
    //    {
    //        vehicleName = varManager.GetComponent<VarManager>().pilotName;
    //    }
    //    */

    //    vehicleName = PlayerPrefs.GetString("PilotName");

    //    _rb = GetComponent<Rigidbody>();
    //    _rb.centerOfMass = centerOfMass.localPosition;
    //    handbrake = false;
    //    Cursor.visible = false;
    //    lapCount = 0;
    //    positionWeight = -Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[0].transform.position);
    //    _checkpointNumber = 0;
    //    _nitroTimer = nitroTimer;
    //    lapsEnded = 1;
    //    nitroEmpty = false;
    //}

    //void Update()
    //{
    //    positionWeight = Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[_checkpointNumber].transform.position);

    //    UpdateTiresPosition();
    //    UIText();
    //    currentSpeed = _rb.velocity.magnitude * 3f;
    //    //  Debug.Log(currentSpeed);

    //    GetInput();
    //    if (Input.GetKeyUp(KeyCode.R)) ResetCar();

    //    CheckCarFlipped();

    //    CheckIfGrounded();
    //    CheckBars();
    //    CheckDirection();
    //}

    //public override void GetInput(float _accel, float _brake,float _handbrake, float _steer, float _nitro)
    //{
    ///*    if (_accel > 0)
    //    {
    //        motorInput = _accel;
    //    }
    //    if (brake < 0)
    //    {
    //        motorInput = brake;
    //    }*/
    //    steerInput = _steer;
    //    motorInput = _accel;
    //}

    //private void GetInput()
    //{
    //    //motorInput = Input.GetAxis("Vertical");
    //    //steerInput = Input.GetAxis("Horizontal");

    //    finalAngle = steerInput * K.JEEP_MAX_STEERING_ANGLE;

    //    if (currentSpeed < maxSpeed && _isGrounded) for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].motorTorque = motorInput * maxTorque;
    //    else if (currentSpeed > maxSpeed && motorInput > 0 || currentSpeed > maxReverseSpeed && motorInput < 0)
    //    {
    //        for (int i = 0; i < wheelColliders.Length; i++)
    //        {
    //            wheelColliders[i].brakeTorque = 1000;
    //            wheelColliders[i].motorTorque = 0;
    //        }
    //    }
    //    if (motorInput == 0 || !_isGrounded || nitroEnd)
    //    {
    //        _rb.drag = _rb.velocity.magnitude / 100f;
    //        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].motorTorque = 0;
    //        if (currentSpeed < maxSpeed) nitroEnd = false;
    //    }
    //    else _rb.drag = 0f;

    //    CheckHandbrake();
    //}

    //private void CheckDirection()
    //{
    //    if (_lastCheckpoint)
    //    {
    //        var currentDirection = _lastCheckpoint.nextCheckpoint.transform.position - transform.position;
    //        if (Vector3.Angle(transform.forward, currentDirection) > 80)
    //        {
    //            _timerWrongDirection += Time.deltaTime;
    //        }
    //        else
    //        {
    //            wrongDirectionText.gameObject.SetActive(false);
    //            _timerWrongDirection = 0;
    //        }
    //    }
    //    if (_timerWrongDirection > 2)
    //    {
    //        wrongDirectionText.gameObject.SetActive(true);
    //    }   
    //}

    //void FixedUpdate()
    //{
    //    //Transforma una dirección de world space a local space.
    //    var relativeVelocity = _rb.transform.InverseTransformDirection(_rb.velocity);

    //    acceleration = _rb.transform.InverseTransformDirection(_rb.velocity).z;

    //    //Maniobrabilidad
    //    ApplySteering(relativeVelocity);

    //    //ADDFORCE de Deslizamiento calculando dirección.
    //    UpdateDrag(relativeVelocity);

    //    FallSpeed();

    //    //Anti vuelco del vehículo
    //    AntiRollBars();

    //    NitroInput();
    //  //  Debug.Log(acceleration);

    //}

    //private void RechargeNitro()
    //{
    //    if (Mathf.FloorToInt(lapCount) == lapsEnded)
    //    {
    //        canRechargeNitro = true;
    //        lapsEnded++;
    //    }

    //    if (!modeNitro && _nitroTimer < nitroTimer && canRechargeNitro) _nitroTimer += Time.deltaTime / rechargeNitro;
    //    if (visualNitro.fillAmount == 1)
    //    {
    //        canRechargeNitro = false;
    //        nitroEmpty = false;
    //    }
    //}

    //private void CheckBars()
    //{
    //    CheckNitroBar();
    //    RechargeNitro();
    //}
    //private void CheckNitroBar()
    //{
    //    visualNitro.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
    //    float calc_nitro = _nitroTimer / nitroTimer;
    //    visualNitro.fillAmount = calc_nitro;
    //}
    //private void NitroInput()
    //{
    //    if (Input.GetKey(KeyCode.LeftShift) && _isGrounded && !nitroEmpty)
    //    {
    //        modeNitro = true;
    //        Camera.main.GetComponent<Bloom>().enabled = true;
    //        Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
    //        Camera.main.GetComponent<MotionBlur>().enabled = true;
    //    }
    //    else 
    //    {
    //        modeNitro = false;
    //        Camera.main.GetComponent<Bloom>().enabled = false;
    //        Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = false;
    //        Camera.main.GetComponent<MotionBlur>().enabled = false;
    //    }

    //    if (modeNitro)
    //    {
    //        if (motorInput < 0) _rb.AddForce(transform.forward * -nitroPower);
    //        else _rb.AddForce(transform.forward * nitroPower);
    //        _nitroTimer -= Time.deltaTime;
    //        if (_nitroTimer < 0)
    //        {
    //            modeNitro = false;
    //            nitroEnd = true;
    //            nitroEmpty = true;
    //            Camera.main.GetComponent<Bloom>().enabled = false;
    //            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = false;
    //            Camera.main.GetComponent<MotionBlur>().enabled = false;
    //        }
    //    }
    //}
    //public void AntiRollBars()
    //{
    //    WheelHit wheelHit;

    //    for (int i = 0; i < wheelColliders.Length; i++)
    //    {
    //        bool grounded = wheelColliders[i].GetGroundHit(out wheelHit);
    //        if (i == 0 || i == 1) if (!grounded) _rb.AddForceAtPosition(wheelColliders[i].transform.up * -300f, wheelColliders[i].transform.position);
    //        else if (!grounded) _rb.AddForceAtPosition(wheelColliders[i].transform.up * -500f, wheelColliders[i].transform.position);
    //    }

    //    /*  WheelHit leftWheelHit;
    //    WheelHit rightWheelHit;
    //    float travelLeft = 1f;
    //    float travelRight = 1f;

    //    for (int i = 0; i < wheelColliders.Length; i++)
    //    {
    //        if (i == 1|| i== 3)
    //        {
    //            bool groundedLeft = wheelColliders[i].GetGroundHit(out leftWheelHit);
    //            if (groundedLeft)
    //            {
    //                travelLeft = (-wheelColliders[i].transform.InverseTransformPoint(leftWheelHit.point).y - wheelColliders[i].radius) / wheelColliders[i].suspensionDistance;
    //                antiRollForceLeft = (travelLeft - travelRight) * antiRoll;
    //                impulseForceLeft = false;
    //            }

    //            if (!groundedLeft && !impulseForceLeft)
    //            {
    //                _rb.AddForceAtPosition(wheelColliders[i].transform.up * -antiRollForceLeft, wheelColliders[i].transform.position);
    //                impulseForceLeft = true;
    //                Debug.Log("ANTIROLL BAR LEFT");
    //            }
    //        }
    //        else if(i== 0 || i ==2)
    //        {
    //            bool groundedRight = wheelColliders[i].GetGroundHit(out rightWheelHit);
    //            if (groundedRight)
    //            {
    //                travelRight = (-wheelColliders[i].transform.InverseTransformPoint(rightWheelHit.point).y - wheelColliders[i].radius) / wheelColliders[i].suspensionDistance;
    //                antiRollForceRight = (travelLeft - travelRight) * antiRoll;
    //                impulseForceRight = false;                
    //            }

    //            if (!groundedRight && !impulseForceRight)
    //            {
    //                _rb.AddForceAtPosition(wheelColliders[i].transform.up * antiRollForceRight, wheelColliders[i].transform.position);
    //                impulseForceRight = true;
    //                Debug.Log("ANTIROLL BAR RIGHT");
    //            }
    //        }        
    //    }*/
    //}

    public void EndRaceHandbrake()
    {
        //for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = brake;
        //dragMultiplier.z += 10 * Time.deltaTime;
        //handbrake = true;
        _rb.drag += 1f * Time.deltaTime;
    }

    //private void CheckHandbrake()
    //{     
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = brake;
    //        dragMultiplier.z += 10 * Time.deltaTime;
    //        handbrake = true;
    //    }
    //    else if (motorInput < 0 && acceleration > 0 || motorInput > 0 && acceleration < 0)
    //    {
    //        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = brake;
    //        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].motorTorque = 0;
    //        dragMultiplier.z += 2 * Time.deltaTime;
    //        handbrake = true;
    //    }
    //    else
    //    {
    //        dragMultiplier.z = 0;
    //        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 0;
    //        handbrake = false;
    //    }
    //}
    //private void UpdateDrag(Vector3 relativeVelocity)
    //{
    //    Vector3 relativeDrag = new Vector3(-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), -relativeVelocity.y * Mathf.Abs(relativeVelocity.y),
    //                                        -relativeVelocity.z * Mathf.Abs(relativeVelocity.z));

    //    var drag = Vector3.Scale(dragMultiplier, relativeDrag);

    //    if (!handbrake) drag.x *= maxSpeed / relativeVelocity.magnitude;

    //    if (Mathf.Abs(relativeVelocity.x) < 5 && !handbrake) drag.x = -relativeVelocity.x * dragMultiplier.x;
    //    _rb.AddForce(transform.TransformDirection(drag) * _rb.mass * Time.deltaTime);
    //}
    //private void ApplySteering(Vector3 relativeVelocity)
    //{
    //    float turnRadius = 3f / Mathf.Sin((90 - (steerInput * 30)) * Mathf.Deg2Rad);
    //    float minMaxTurn = EvaluateSpeedToTurn(_rb.velocity.magnitude);
    //    float turnSpeed = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
    //    transform.RotateAround(transform.position + transform.right * turnRadius * steerInput, transform.up,
    //                            turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steerInput);
    //}
    //private float EvaluateSpeedToTurn(float speed)
    //{
    //    if (speed > maxSpeed / 2) return minimumTurn;
    //    var speedIndex = 1 - (speed / (maxSpeed / 2));

    //    return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
    //}
    //private void CheckCarFlipped()
    //{
    //    if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270 && currentSpeed < 1 && currentSpeed > -1) resetTimer += Time.deltaTime;
    //    else resetTimer = 0;
    //    if (resetTimer > resetTime) FlipCar();
    //}

    //private void FlipCar()
    //{
    //    //transform.rotation = Quaternion.LookRotation(transform.forward);
    //    transform.position += Vector3.up * 0.5f;
    //    Vector3 forwardDirection = _lastCheckpoint.nextCheckpoint.transform.position - transform.position;
    //    forwardDirection.y = transform.position.y;
    //    transform.forward = forwardDirection;
    //    _rb.velocity = Vector3.zero;
    //    _rb.angularVelocity = Vector3.zero;
    //    resetTimer = 0;
    //}

    ///// <summary>
    ///// Obtengo el ultimo checkpoint por el que pase.
    ///// </summary>
    ///// <param name="chk">Checkpoint</param>
    //public void SetCheckpoint(Checkpoint chk)
    //{
    //    _checkpointNumber = _checkpointMananagerReference.checkpointsList.Count - 1 == _checkpointNumber ? 0 : _checkpointNumber + 1;
    //    _lastCheckpoint = chk;
    //    lapCount += _checkpointMananagerReference.checkpointValue;
    //}

    ///// <summary>
    ///// El auto regresa a un punto del ultimo checkpoint.
    ///// </summary>
    //private void ResetCar()
    //{
    //    if (_lastCheckpoint == null) return;

    //    _rb.velocity = Vector3.zero;
    //    transform.position = _lastCheckpoint.GetRespawnPoint(transform.position) + Vector3.up;
    //    transform.rotation = _lastCheckpoint.transform.rotation;

    //    /*RaycastHit hit;
    //    if (Physics.Raycast(transform.position, -transform.up, out hit, layer))
    //    {
    //        if (hit.distance < 5f)
    //        {
    //            transform.rotation = Quaternion.LookRotation(transform.forward);

    //            if (Physics.Raycast(transform.position + transform.up, transform.forward, stuckMaxDist))
    //            {
    //                print("forward");
    //                transform.Translate(-10, 0, -10);
    //            }
    //            else if (Physics.Raycast(transform.position + transform.up, transform.right, stuckMaxDist))
    //                transform.Translate(-10, 0, 0);
    //            else if (Physics.Raycast(transform.position + transform.up, -transform.right, stuckMaxDist))
    //                transform.Translate(10, 0, 0);

    //            transform.rotation = Quaternion.LookRotation(transform.forward);
    //            transform.position += Vector3.up * 0.5f;
    //            _rb.velocity = Vector3.zero;
    //            _rb.angularVelocity = Vector3.zero;
    //        }
    //    }*/
    //}

    //private void UpdateTiresPosition()
    //{
    //    for (int i = 0; i < tiresCar.Length; i++)
    //    {
    //        Quaternion quat;
    //        Vector3 pos;
    //        wheelColliders[i].GetWorldPose(out pos, out quat);
    //        tiresCar[i].position = pos;
    //        tiresCar[i].rotation = quat;
    //        if (i < 2f)
    //        {
    //            Vector3 steerAngle = tiresCar[i].localEulerAngles;
    //            steerAngle.y = finalAngle;
    //            tiresCar[i].localEulerAngles = steerAngle;
    //        }
    //    }
    //}

    ///// <summary>
    ///// Checkea si el vehiculo esta tocando el piso.
    ///// </summary>
    //protected void CheckIfGrounded()
    //{
    //    Ray ray = new Ray(centerOfMass.transform.position, -transform.up);
    //    Debug.DrawRay(ray.origin, ray.direction, Color.red);
    //    RaycastHit hit;
    //    _isGrounded = false;
    //    _isGroundedRamp = false;
    //    if (Physics.Raycast(ray, out hit, 1))
    //    {
    //        if (hit.collider.gameObject.layer == K.LAYER_GROUND || hit.collider.gameObject.layer == K.LAYER_SIDEGROUND || hit.collider.gameObject.layer == K.LAYER_RAMP)
    //        {
    //            _isGrounded = true;
    //            if (hit.collider.gameObject.layer == K.LAYER_RAMP) _isGroundedRamp = true;
    //        }
    //    }
    //}
    //public void PushRamp(float amount)
    //{
    //    if (_isGroundedRamp) _rb.AddForce(transform.forward * amount);
    //}

    ///// <summary>
    ///// Hace que el auto caiga mas rapido en los saltos.
    ///// </summary>
    //protected void FallSpeed()
    //{
    //    if (!_isGrounded)
    //    {
    //        _rb.AddForce(-Vector3.up * fallForce);
    //    }
    //}

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.layer == K.LAYER_IA)
    //    {
    // //       other.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.position);
    //    }
    //}
    //protected void UIText()
    //{
    //    NotifyObserver(K.OBS_MESSAGE_SPEED);
    //    NotifyObserver(K.OBS_MESSAGE_LAPCOUNT);
    //}
    //private void OnDrawGizmos()
    //{

    //}    
}
