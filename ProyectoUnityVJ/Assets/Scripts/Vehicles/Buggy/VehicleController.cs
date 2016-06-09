using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System;

public class VehicleController : Vehicle
{
    //public float maxTorque;
    //public Transform centerOfMass;
    //public Text speedText;
    //public Text wrongDirectionText;
    //public GameObject trailRenderModel;
    //public float currentSpeed { get; private set; }

    //public Transform[] wheelMeshList;
    //private Rigidbody _rb;
    //private float _acceleration;
    //public float topSpeed;
    //public float maxReverseSpeed;
    //private float _steerInput;
    //private float _motorInput;
    //private float _handbrakeInput;
    //public int maximumTurn = 15;
    //public int minimumTurn = 10;
    //public Vector3 dragMultiplier;
    //private bool _handbrake;
    //private float _resetTimer;
    //public float resetTime;
    //public float stuckMaxDist;
    //public LayerMask layer;
    //public float fallForce = 10000;
    //public GameObject carModel;
    //private float _finalAngle;
    //private bool _isGrounded;
    //private bool _isGroundedRamp;
    //private Checkpoint _lastCheckpoint;

    //public float antiRoll = 1000f;
    //public float brakeForce;
    //private bool _impulseForceLeft;
    //private bool _impulseForceRight;
    //private bool _impulseForceFront;
    //private bool _impulseForceRear;
    //private float _antiRollForceRight;
    //private float _antiRollForceLeft;
    //private float _antiRollForceFront;
    //private float _antiRollForceRear;
    //private bool _reversing;
    //private int _checkpointNumber;

    //private bool _modeNitro = false;
    //public float nitroPower;
    //public float nitroTimer;
    //private float _nitroTimer;
    //public float rechargeNitro;
    //private bool _nitroEnd;
    //public Image visualNitro;
    //private float _lapsEnded;
    //private bool _canRechargeNitro;
    //private bool _nitroEmpty;
    //private bool _countInAir;
    //private float _timerWrongDirection;

    ////public GameObject varManager;

    //private bool _limitSpeed;
    //private bool resetVelocity;
    //private float _prevAcc;
    //private float _acce;
    //protected override void Start()
    //{
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
    //    _handbrake = false;
    //    Cursor.visible = false;
    //    lapCount = 0;
    //    positionWeight = -Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[0].transform.position);
    //    _checkpointNumber = 0;
    //    _nitroTimer = nitroTimer;
    //    _lapsEnded = 1;
    //    _nitroEmpty = false;
    //}
    //void Update()
    //{
    //    positionWeight = Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[_checkpointNumber].transform.position);
    //    UpdateTiresPosition();

    //    currentSpeed = _rb.velocity.magnitude * 3f;
    //    //  Debug.Log(currentSpeed);
    //    if (Input.GetKeyUp(KeyCode.R)) ResetCar();


    //    CheckCarFlipped();
    //    CheckIfGrounded();
    //    CheckBars();
    //    CheckDirection();

    //    UIText();
    //}
    //void FixedUpdate()
    //{
    //    //Transforma una dirección de world space a local space.
    //    var relativeVelocity = transform.InverseTransformDirection(_rb.velocity);

    //    _acceleration = _rb.transform.InverseTransformDirection(_rb.velocity).z;

    //    //Maniobrabilidad
    //    ApplySteering(relativeVelocity);

    //    //Evitar Deslizamiento calculando dirección.
    //    UpdateDrag(relativeVelocity);

    //    FallSpeed();

    //    //Anti vuelco del vehículo
    //    AntiRollBars();

    //    NitroInput();
    //    //  Debug.Log(acceleration);

    //}
    //public override void GetInput(float accelInput, float brakeInput, float handbrakeInput, float steerInput, float nitro)
    //{
    //    _steerInput = steerInput;
    //    var forwardForce = accelInput * maxTorque;

    //    var brake = brakeInput;

    //    //Aplica Fuerza
    //    ApplyThrottle(forwardForce, brake);
    //    //Limite de velocidad
    //    CapSpeed();

    //    _motorInput = accelInput;
    //    _handbrakeInput = handbrakeInput;

    //    //Freno de mano
    //    CheckHandbrake();
    //}

    //protected void ApplyThrottle(float forwardForce, float brake)
    //{
    //    if (Input.GetAxis(K.INPUT_HORIZONTAL) == 0 && _isGrounded && !_isGroundedRamp) _prevAcc = _acceleration;
    //    else if (_isGrounded && !_isGroundedRamp)
    //    {
    //        _rb.velocity = Vector3.zero;
    //        _rb.velocity = new Vector3(transform.forward.x * _prevAcc, transform.forward.y * _prevAcc, transform.forward.z * _prevAcc);
    //    }
    //    else if (!_isGrounded) _prevAcc = 0f;

    //    /*    if (_motorInput > 0) _acce += Time.deltaTime / 10;
    //        else _acce -= Time.deltaTime / 10;
    //        var time = Mathf.Clamp(_acce, 0, 1);*/

    //    if (!_limitSpeed && _isGrounded) _rb.AddRelativeForce(0, 0, (forwardForce * Time.deltaTime));

    //    //  if (!_limitSpeed) _rb.AddForce(transform.forward * forwardForce);

    //    if (_motorInput == 0 || _nitroEnd)
    //    {
    //        _rb.drag = _rb.velocity.magnitude / 30f;
    //        if (currentSpeed < topSpeed) _nitroEnd = false;
    //    }
    //    else if (!_isGrounded) _rb.drag = _rb.velocity.magnitude / 100f;
    //    else _rb.drag = 0f;
    //}
    //protected void CapSpeed()
    //{
    //    if (currentSpeed > topSpeed) _limitSpeed = true;
    //    else _limitSpeed = false;
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
    //private void RechargeNitro()
    //{
    //    if (Mathf.FloorToInt(lapCount) == _lapsEnded)
    //    {
    //        _canRechargeNitro = true;
    //        _lapsEnded++;
    //    }

    //    if (!_modeNitro && _nitroTimer < nitroTimer && _canRechargeNitro) _nitroTimer += Time.deltaTime / rechargeNitro;
    //    if (visualNitro.fillAmount == 1)
    //    {
    //        _canRechargeNitro = false;
    //        _nitroEmpty = false;
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
    //    if (Input.GetKey(KeyCode.LeftShift) && _isGrounded && !_nitroEmpty)
    //    {
    //        _modeNitro = true;
    //        Camera.main.GetComponent<Bloom>().enabled = true;
    //        Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
    //        Camera.main.GetComponent<MotionBlur>().enabled = true;
    //    }
    //    else
    //    {
    //        _modeNitro = false;
    //        Camera.main.GetComponent<Bloom>().enabled = false;
    //        Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = false;
    //        Camera.main.GetComponent<MotionBlur>().enabled = false;
    //    }

    //    if (_modeNitro)
    //    {
    //        if (_motorInput < 0) _rb.AddForce(transform.forward * -nitroPower);
    //        else _rb.AddForce(transform.forward * nitroPower);
    //        _nitroTimer -= Time.deltaTime;
    //        if (_nitroTimer < 0)
    //        {
    //            _modeNitro = false;
    //            _nitroEnd = true;
    //            _nitroEmpty = true;
    //            Camera.main.GetComponent<Bloom>().enabled = false;
    //            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = false;
    //            Camera.main.GetComponent<MotionBlur>().enabled = false;
    //        }
    //    }
    //}
    //public void AntiRollBars()
    //{
    //    /*     WheelHit wheelHit;

    //         for (int i = 0; i < wheelMeshList.Length; i++)
    //         {
    //    //         bool grounded = wheelMeshList[i].GetGroundHit(out wheelHit);
    //             if (i == 0 || i == 1) if (!grounded) _rb.AddForceAtPosition(wheelMeshList[i].transform.up * -300f, wheelMeshList[i].transform.position);
    //                 else if (!grounded) _rb.AddForceAtPosition(wheelMeshList[i].transform.up * -500f, wheelMeshList[i].transform.position);
    //         }*/

    //    /*     float travelLeft = 1f;
    //         float travelRight = 1f;

    //         for (int i = 0; i < wheelMeshList.Length; i++)
    //         {
    //             if (i == 1|| i== 3)
    //             {
    //                 if (wheelMeshList[i].gameObject.GetComponentInParent<Suspension>().isGrounded)
    //                 {
    //                     travelLeft = (-wheelMeshList[i].transform.InverseTransformPoint(transform.position).y - wheelRadius) / wheelMeshList[i].GetComponentInParent<Suspension>().restLenght;
    //                     _antiRollForceLeft = (travelLeft - travelRight) * antiRoll;
    //                     _impulseForceLeft = false;
    //                 }

    //                 if (!wheelMeshList[i].gameObject.GetComponentInParent<Suspension>().isGrounded && !_impulseForceLeft)
    //                 {
    //                     _rb.AddForceAtPosition(wheelMeshList[i].transform.up * -_antiRollForceLeft, wheelMeshList[i].transform.position);
    //                     _impulseForceLeft = true;
    //                     Debug.Log("ANTIROLL BAR LEFT");
    //                 }
    //             }
    //             else if(i== 0 || i ==2)
    //             {
    //                 if (wheelMeshList[i].gameObject.GetComponentInParent<Suspension>().isGrounded)
    //                 {
    //                     travelRight = (-wheelMeshList[i].transform.InverseTransformPoint(transform.position).y - wheelRadius) / wheelMeshList[i].GetComponentInParent<Suspension>().restLenght;
    //                     _antiRollForceRight = (travelLeft - travelRight) * antiRoll;
    //                     _impulseForceRight = false;                
    //                 }

    //                 if (!wheelMeshList[i].gameObject.GetComponentInParent<Suspension>().isGrounded && !_impulseForceRight)
    //                 {
    //                     _rb.AddForceAtPosition(wheelMeshList[i].transform.up * _antiRollForceRight, wheelMeshList[i].transform.position);
    //                     _impulseForceRight = true;
    //                     Debug.Log("ANTIROLL BAR RIGHT");
    //                 }
    //             }        
    //         }*/
    //}
    //public void EndRaceHandbrake()
    //{
    //    dragMultiplier.z += 10 * Time.deltaTime;
    //    _handbrake = true;
    //}
    //private void CheckHandbrake()
    //{
    //    if (_handbrakeInput > 0)
    //    {
    //        dragMultiplier.z += 10 * Time.deltaTime;
    //        _handbrake = true;
    //    }
    //    else if (_motorInput < 0 && _acceleration > 0 || _motorInput > 0 && _acceleration < 0)
    //    {
    //        dragMultiplier.z += 2 * Time.deltaTime;
    //        _handbrake = true;
    //    }
    //    else
    //    {
    //        dragMultiplier.z = 0;
    //        _handbrake = false;
    //    }
    //}
    //private void UpdateDrag(Vector3 relativeVelocity)
    //{
    //    Vector3 relativeDrag = new Vector3(-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), -relativeVelocity.y * Mathf.Abs(relativeVelocity.y),
    //                                        -relativeVelocity.z * Mathf.Abs(relativeVelocity.z));

    //    var drag = Vector3.Scale(dragMultiplier, relativeDrag);

    //    if (!_handbrake) drag.x *= topSpeed / relativeVelocity.magnitude;

    //    if (Mathf.Abs(relativeVelocity.x) < 5 && !_handbrake) drag.x = -relativeVelocity.x * dragMultiplier.x;
    //    _rb.AddForce(transform.TransformDirection(drag) * _rb.mass * Time.deltaTime);
    //}
    //private void ApplySteering(Vector3 relativeVelocity)
    //{
    //    float turnRadius = 3f / Mathf.Sin((90 - (_steerInput * 30)) * Mathf.Deg2Rad);
    //    float minMaxTurn = EvaluateSpeedToTurn(_rb.velocity.magnitude);
    //    float turnSpeed = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
    //    transform.RotateAround(transform.position + transform.right * turnRadius * _steerInput, transform.up,
    //                            turnSpeed * Mathf.Rad2Deg * Time.deltaTime * _steerInput);
    //}
    //private float EvaluateSpeedToTurn(float speed)
    //{
    //    if (speed > topSpeed / 2) return minimumTurn;
    //    var speedIndex = 1 - (speed / (topSpeed / 2));

    //    return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
    //}
    //private void CheckCarFlipped()
    //{
    //    if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270 && currentSpeed < 1 && currentSpeed > -1) _resetTimer += Time.deltaTime;
    //    else _resetTimer = 0;
    //    if (_resetTimer > resetTime) FlipCar();
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
    //    _resetTimer = 0;
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
    //    _finalAngle = _steerInput * K.JEEP_MAX_STEERING_ANGLE;
    //    for (int i = 0; i < wheelMeshList.Length; i++)
    //    {
    //        if (i < 2f)
    //        {
    //            Vector3 steerAngle = wheelMeshList[i].localEulerAngles;
    //            steerAngle.y = _finalAngle;
    //            wheelMeshList[i].localEulerAngles = steerAngle;
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
    //        _rb.AddForce(-Vector3.up * fallForce * _rb.velocity.magnitude);
    //    }
    //}

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.layer == K.LAYER_IA)
    //    {
    //        //       other.gameObject.GetComponent<Rigidbody>().AddForce(other.transform.position);
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
