using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IAVehicle : Vehicle
{
    //public GameObject hpBarContainer;
    //public RawImage hpBarImage;
    //public GameObject remains;
    //public float _maxHp, _currentHp;
    //private Vector3 _aux;
    //public float maxTorque;
    //public Transform centerOfMass;
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
    //private float respawnTimer;
    //public float respawnTime;
    //public float stuckMaxDist;
    //public LayerMask layer;
    //public float fallForce = 10000;
    //public GameObject carModel;
    //private float finalAngle;
    //public bool _isGrounded;
    //public bool _isGroundedRamp;
    //public Checkpoint _lastCheckpoint;
    public Checkpoint _nextCheckpoint;



    //public Transform frontSensor;
    //public Transform rightSensor;
    //public Transform leftSensor;
    //public Transform frontRightSensor;
    //public Transform frontLeftSensor;
    //public Transform angleRightSensor;
    //public Transform angleLeftSensor;
    //public float sensorsDistance;

    //public float antiRoll = 1000f;
    //public float brake;/*
    //private bool impulseForceLeft;
    //private bool impulseForceRight;
    //private bool impulseForceFront;
    //private bool impulseForceRear;
    //private float antiRollForceRight;
    //private float antiRollForceLeft;
    //private float antiRollForceFront;
    //private float antiRollForceRear;*/
    //private bool _reversing;
    //private bool _speedForce;
    //private int _checkpointNumber;

    private float _timerReset;
    private float _resetWaitTime = 1.5f;
    private bool _stoped = false;
    private InputControllerIA _myInput;
    private Vector3 _nextDestinationPoint;
    //private float _torque;
    //private float _steer;

    //private bool modeNitro = false;
    //public float nitroPower;
    //public float nitroTimer;
    //private float _nitroTimer;
    //public float rechargeNitro;
    //private bool nitroEnd;
    //private float lapsEnded;
    //private bool canRechargeNitro;
    //private bool nitroEmpty;
    //private bool countInAir;
    //private float _timerWrongDirection;

    protected override void Start()
    {
        _myInput = this.gameObject.GetComponentInParent<InputControllerIA>();
        base.Start();
        //lapCount = 0;
        //positionWeight = -Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[0].transform.position);
        //_checkpointNumber = 0;
        /*_nitroTimer = nitroTimer;
        lapsEnded = 1;
        nitroEmpty = false;*/
        _nextDestinationPoint = CalculateNextPoint(_nextCheckpoint, -0.75f, 0.75f);
        _myInput.SetDestinationPoint(_nextDestinationPoint);
        
       /* _maxHp = K.IA_MAX_HP;
        _currentHp = _maxHp;
        _aux = hpBarImage.transform.localScale;*/
    }


    protected override void Update()
    {
        base.Update();
        if (currentVelZ < 5f && !_stoped)
        {
            _stoped = true;
        } else if (_stoped && currentVelZ > 6f)
        {
            _stoped = false;
            _timerReset = 0;
        }

        if (_stoped)
        {
            _timerReset += Time.deltaTime;
            if (_timerReset >= _resetWaitTime)
            {
                _stoped = false;
                _timerReset = 0;
                ResetCar();
            }
        }

    }

    //// Update is called once per frame
    //void Update ()
    //{
    //    UpdateHpBar();

    //    positionWeight = Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[_checkpointNumber].transform.position);

    //    if (Vector3.Distance(transform.position, _nextDestinationPoint) < 15)
    //    {
    //        lapCount += _checkpointMananagerReference.checkpointValue;
    //        CalculateNextCheckpoint(_nextCheckpoint);
    //        _nextDestinationPoint = CalculateNextPoint(_nextCheckpoint);
    //    }
    //    currentSpeed = _rb.velocity.magnitude * 3f;
    //    //  Debug.Log(currentSpeed);
    //    Sensors();

    //    GetInput();

    //    CheckCarFlipped();

    //    CheckIfGrounded();

    //    if (!_speedForce && _rb.velocity.magnitude > 20f)
    //        _speedForce = true;

    //    CheckForReset();
    //}


    //private void UpdateHpBar()
    //{
    //    hpBarContainer.transform.LookAt(Camera.main.transform.position);
    //    _aux.x = _currentHp / _maxHp;
    //    hpBarImage.transform.localScale = _aux;
    //}

    //private void CheckForReset()
    //{
    //    if (_rb.velocity.magnitude < 1f && _speedForce)
    //    {
    //        respawnTimer += Time.deltaTime;
    //        if (respawnTimer >= respawnTime)
    //            ResetCar();
    //    }

    //    if (!_isGrounded)
    //    {
    //        respawnTimer += Time.deltaTime;
    //        if (respawnTimer >= respawnTime)
    //            ResetCar();
    //    }
    //}

    //private void ResetCar()
    //{
    //    if (_lastCheckpoint == null) return;
    //    print("reserting IA");
    //    respawnTimer = 0;
    //    _rb.velocity = Vector3.zero;
    //    transform.position = CalculateNextPoint(_lastCheckpoint) - Vector3.up;
    //    transform.rotation = _lastCheckpoint.transform.rotation;
    //}

    //public void Damage(float d)
    //{
    //    _currentHp -= d;

    //    if (_currentHp <= 0)
    //    {
    //        //_soundManagerReference.PlaySound(K.SOUND_CAR_DESTROY);
    //        NotifyObserver(K.OBS_MESSAGE_DESTROYED);
    //        Destroy(this.gameObject);
    //        Instantiate(remains, transform.position, transform.rotation);

    //    }
    //}

    //private void Sensors()
    //{
    //    _torque = 1;
    //    RaycastHit hit;
    //    _steer = 0;

    //    //Brake
    //    if (Physics.Raycast(frontSensor.position, frontSensor.forward, out hit, sensorsDistance/2))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _torque *= -1;
    //            Debug.DrawLine(frontSensor.position, hit.normal, Color.blue);
    //        }
    //    }

    //    //FrontRightSensor
    //    if (Physics.Raycast(frontRightSensor.position, angleRightSensor.forward, out hit, sensorsDistance*2))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _torque /= 2;
    //            _steer -= 0.5f;
    //            Debug.DrawLine(frontRightSensor.position, hit.point, Color.white);
    //        }
    //    }
    //    else if (Physics.Raycast(angleRightSensor.position, angleRightSensor.forward, out hit, sensorsDistance))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _torque /= 2;
    //            _steer -= 0.5f;
    //            Debug.DrawLine(angleRightSensor.position, hit.point, Color.white);
    //        }
    //    }


    //    //FrontLeftSensor
    //    if (Physics.Raycast(frontLeftSensor.position, frontLeftSensor.forward, out hit, sensorsDistance*2))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _torque /= 2;
    //            _steer += 0.5f;

    //            Debug.DrawLine(frontLeftSensor.position, hit.point, Color.white);
    //        }
    //    }
    //    else if (Physics.Raycast(angleLeftSensor.position, angleLeftSensor.forward, out hit, sensorsDistance))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _torque /= 2;
    //            _steer += 0.5f;
    //            Debug.DrawLine(angleLeftSensor.position, hit.point, Color.white);
    //        }
    //    }

    //    if (_steer != 0)
    //        steerInput = _steer;
    //    else
    //        SteeringToTarget();

    //    motorInput = _torque;

    //    /*
    //    //RightSensor
    //    if (Physics.Raycast(rightSensor.position, rightSensor.forward, out hit, sensorsDistance))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _torque /= 4;
    //            _steer -= 1f;

    //            Debug.DrawLine(rightSensor.position, hit.point, Color.green);
    //        }
    //    }

    //    //LeftSensor
    //    if (Physics.Raycast(leftSensor.position, leftSensor.forward, out hit, sensorsDistance))
    //    {
    //        if (hit.transform.tag != "Track")
    //        {
    //            _torque /= 4;
    //            _steer += 1f;

    //            Debug.DrawLine(leftSensor.position, hit.point, Color.green);
    //        }
    //    }*/
    //}

    //private void SteeringToTarget()
    //{
    //    Vector3 steerVector = transform.InverseTransformPoint(_nextDestinationPoint.x, transform.position.y, _nextDestinationPoint.z);
    //    steerInput = (steerVector.x / steerVector.magnitude) ;
    //}

    /// <summary>
    /// Tomo el proximo checkpoint y calculo un punto aleatorio dentro del mismo, si hay un obstaculo vuelvo a calcular. 
    /// </summary>
    /// <param name="chk">Proximo Checkpoint</param>
    private Vector3 CalculateNextPoint(Checkpoint chk ,float min, float max)
    {
        Vector3 randomPoint = new Vector3(Random.Range(min, max), 0, Random.Range(min, max));
        randomPoint = chk.transform.TransformPoint(randomPoint * 0.5f);
        return randomPoint;
    }

    //public void SetCheckpoint(Checkpoint chk)
    //{
    //    _checkpointNumber = _checkpointMananagerReference.checkpointsList.Count - 1 == _checkpointNumber ? 0 : _checkpointNumber + 1;
    //    _lastCheckpoint = chk;
    //    lapCount += _checkpointMananagerReference.checkpointValue;
    //}

    public void SetNextCheckpoint(Checkpoint chk, float minimo, float maximo)
    {
        _nextCheckpoint = chk;
        _nextDestinationPoint = CalculateNextPoint(chk, minimo, maximo);
        _myInput.SetDestinationPoint(_nextDestinationPoint);
    }

    //private void CalculateNextCheckpoint(Checkpoint chk)
    //{
    //    _nextCheckpoint = chk.nextCheckpoint;
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
    //    //  Debug.Log(acceleration);
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

    //private void UpdateDrag(Vector3 relativeVelocity)
    //{
    //    Vector3 relativeDrag = new Vector3(-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), -relativeVelocity.y * Mathf.Abs(relativeVelocity.y),
    //                                        -relativeVelocity.z * Mathf.Abs(relativeVelocity.z));

    //    var drag = Vector3.Scale(dragMultiplier, relativeDrag);

    //    if (!handbrake) drag.x *= maxSpeed / relativeVelocity.magnitude;

    //    if (Mathf.Abs(relativeVelocity.x) < 5 && !handbrake) drag.x = -relativeVelocity.x * dragMultiplier.x;
    //    _rb.AddForce(transform.TransformDirection(drag) * _rb.mass * Time.deltaTime);
    //}

    //protected void FallSpeed()
    //{
    //    if (!_isGrounded)
    //    {
    //        _rb.AddForce(-Vector3.up * fallForce);
    //    }
    //}

    //public void AntiRollBars()
    //{
    //    WheelHit wheelHit;

    //    for (int i = 0; i < wheelColliders.Length; i++)
    //    {
    //        bool grounded = wheelColliders[i].GetGroundHit(out wheelHit);
    //        if (i == 0 || i == 1)
    //            if (!grounded) _rb.AddForceAtPosition(wheelColliders[i].transform.up * -300f, wheelColliders[i].transform.position);
    //            else if (!grounded) _rb.AddForceAtPosition(wheelColliders[i].transform.up * -500f, wheelColliders[i].transform.position);
    //    }
    //}

    //public void PushRamp(float amount)
    //{
    //    if (_isGroundedRamp) _rb.AddForce(transform.forward * amount);
    //}

    public void ChangeGear(string id)
    {
        print(gameObject + id);
        switch (id)
        {
            case "normal":
                topSpeed = 180f;
                maxForce = 200f;
                break;
            case "high":
                topSpeed = 230f;
                maxForce = 260f;
                break;
            case "low":
                topSpeed = 150f;
                maxForce = 130f;
                break;
            default:
                print("Error de Id");
                break;
        }
    }
}
