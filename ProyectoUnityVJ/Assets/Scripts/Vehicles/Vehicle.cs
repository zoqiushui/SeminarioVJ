using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(VehicleData))]
[RequireComponent(typeof(InputController))]
public abstract class Vehicle : MonoBehaviour, IObservable
{
    public float positionWeight { get; protected set; }
    public float lapCount { get; protected set; }
    public string vehicleName;
    public float wheelRadius, downForce, topSpeed;
    public Transform leftTurnWheelPosition, rightWheelTurnPosition;
    public Transform centerOfMass;
    public GameObject carFootprintPrefab;
    public List<Transform> wheelMeshList;
    public List<Suspension> wheelSuspensionList;
    public float maxSteerForce, maxForce, brakeForce;
    [Range(0, 1)]
    public float airFriction = 0.98f;
    [Range(0, 1)]
    public float groundFriction = 0.96f;
    public float currentVelZ { get; private set; }
    public bool isGrounded { private set; get; }


    protected bool _modeNitro = false;
    public float nitroPower;
    public float nitroTimer;
    protected float _nitroTimer;
    public float rechargeNitro;
    protected bool _nitroEnd;
    protected float _lapsEnded;
    public bool _canRechargeNitro;
    protected bool _nitroEmpty;
    protected bool _countInAir;
    protected float _timerWrongDirection;

    public Camera rearMirror;
    protected float _steerInput, _motorInput;
    private float _finalAngle;
    private float resetTimer;
    public float resetTime;
    public float maximumTurn, minimumTurn;
    public ParticleSystem backDust;
    private AudioSource engineSound;
    public List<int> totalGears;

    protected int _checkpointNumber;
    protected Checkpoint _lastCheckpoint;
    protected List<IObserver> _obsList;
    protected bool _isDestroyed;
    protected CheckpointManager _checkpointMananagerReference;
    protected IngameUIManager _ingameUIMananagerReference;
    protected SoundManager _soundManagerReference;
    protected GameManager _gameManagerReference;
    protected Rigidbody _rb;
    protected bool _isGroundedRamp;
    protected List<GameObject> _wheelTrails;


    protected float friction
    {
        get
        {
            if (isGrounded)
                return groundFriction;
            else
                return airFriction;
        }
    }

    protected virtual void Start()
    {
        _obsList = new List<IObserver>();
        _checkpointMananagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<CheckpointManager>();
        _ingameUIMananagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<IngameUIManager>();
        _gameManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<GameManager>();
        _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
        AddObserver(_checkpointMananagerReference);
        AddObserver(_ingameUIMananagerReference);
        AddObserver(_gameManagerReference);
        _isDestroyed = false;
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass.localPosition;
        /*_wheelTrails = new List<TrailRenderer>();
        foreach (var wheel in wheelSuspensionList)
        {
            var trail = wheel.GetComponentInChildren<TrailRenderer>();
            trail.enabled = false;
            _wheelTrails.Add(trail);
        }*/
        Cursor.visible = false;
        lapCount = 0;
        positionWeight = -Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[0].transform.position);
        _checkpointNumber = 0;
        _nitroTimer = nitroTimer;
        _lapsEnded = 1;
        _nitroEmpty = false;
        _wheelTrails = new List<GameObject>();
        foreach (var wheel in wheelMeshList)
        {
            var go = Instantiate(carFootprintPrefab);
            go.transform.parent = wheel.transform.parent;
            go.transform.localPosition = new Vector3(0, -wheel.GetComponent<MeshRenderer>().bounds.extents.y, 0);
            _wheelTrails.Add(go);
        }
        engineSound = GetComponent<AudioSource>();
    }

    public void AddObserver(IObserver obs)
    {
        if (!_obsList.Contains(obs)) _obsList.Add(obs);
    }

    public void NotifyObserver(string msg)
    {
        foreach (var obs in _obsList)
        {
            obs.Notify(this, msg);
        }
    }

    public void RemoveObserver(IObserver obs)
    {
        if (_obsList.Contains(obs)) _obsList.Remove(obs);
    }

    public virtual void SetCheckpoint(Checkpoint chk)
    {
        _checkpointNumber = _checkpointMananagerReference.checkpointsList.Count - 1 == _checkpointNumber ? 0 : _checkpointNumber + 1;
        _lastCheckpoint = chk;
        lapCount += _checkpointMananagerReference.checkpointValue;
    }

    public virtual void Move(float accelInput, float brakeInput, float handbrakeInput, float steerInput, float nitroInput)
    {
        _steerInput = steerInput;
        _motorInput = accelInput;
        currentVelZ = transform.InverseTransformDirection(_rb.velocity).z;
        var steerForce = steerInput * maxSteerForce;
        var forwardForce = accelInput * maxForce;
        var brakeForce = brakeInput * this.brakeForce;
        //_rb.drag = K.AIR_DRAG;
        foreach (var wheel in wheelSuspensionList)
        {
            isGrounded = false;
            _isGroundedRamp = false;

            if (wheel.IsGroundedRamp())
            {
                _isGroundedRamp = true;
                break;
            }
            else if (wheel.IsGrounded())
            {
                isGrounded = true;
                //_rb.drag = 1;
                //CalculateSteerForceWithVelocity(out steerForce,steerInput); 
                break;
            }
        }
        ApplyDrive(forwardForce, accelInput, brakeForce);
        //   ApplySteer(steerForce, steerInput);
        Drag(accelInput, brakeInput);
        AddDownForce();
        CapSpeed();
        NitroInput(nitroInput, brakeInput);

        //Transforma una dirección de world space a local space.
        var relativeVelocity = transform.InverseTransformDirection(_rb.velocity);
        //Maniobrabilidad
        ApplySteering(relativeVelocity);
    }
    protected virtual void Update()
    {
        positionWeight = Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[_checkpointNumber].transform.position);

        UpdateTyres();
        //ChangeToRearView();
        //CheckBars();
        //CheckDirection();
        CheckCarFlipped();
        CheckDustVehicle();
        EngineSound();

        if (currentVelZ*K.KPH_TO_MPS_MULTIPLIER > 50 && isGrounded && (_steerInput > .5f || _steerInput < -.5f))
        {
            for (int i = 0; i < wheelMeshList.Count; i++)
            {
                if (wheelMeshList[i].parent.GetComponent<Suspension>().IsGrounded())
                {
                    _wheelTrails[i].GetComponentInChildren<TrailRenderer>().enabled = true;
                    _wheelTrails[i].transform.localPosition = new Vector3(0, -wheelMeshList[i].GetComponent<MeshRenderer>().bounds.extents.y/3, 0);
                }
            }
        }
        else
        {
            foreach (var footprint in _wheelTrails)
            {
                footprint.GetComponentInChildren<TrailRenderer>().enabled = false;
            }
        }
    }
    public void EngineSound()
    {
        //FIVE GEARS

        /*   for (var i = 0; i < totalGears.Count; i++)
        {
            if (totalGears[i] > currentVelZ) break;
            float gearMinValue;
            float gearMaxValue;
            if (i == 0) gearMinValue = 0;
            else gearMinValue = totalGears[i - 1];
            gearMaxValue = totalGears[i];
            var enginePitch = ((currentVelZ - gearMinValue) / (gearMaxValue - gearMinValue)) + 1;
            engineSound.pitch = enginePitch;
        }*/


        //ONE GEAR

        var enginePitch = ((currentVelZ - 0) / (25 - 0)) + 1;
        engineSound.pitch = enginePitch;

    }
    private void CheckDustVehicle()
    {
        if (currentVelZ > 10 && isGrounded || currentVelZ < -5 && isGrounded) backDust.Play();
        else backDust.Stop();
    }
    //private void ChangeToRearView()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        rearMirror.enabled = true;
    //        Camera.main.depth = -1f;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.Q))
    //    {
    //        rearMirror.enabled = false;
    //        Camera.main.depth = 0f;
    //    }
    //}

    protected void UpdateTyres()
    {
        _finalAngle = _steerInput * K.JEEP_MAX_STEERING_ANGLE;
        for (int i = 0; i < wheelMeshList.Count; i++)
            if (i < 2f)
                wheelMeshList[i].localEulerAngles = new Vector3(0, _finalAngle, 0);
    }

    protected void Drag(float a, float b)
    {
        var vel = _rb.velocity;
        vel.x *= friction;
        vel.z *= friction;
        _rb.velocity = vel;
        _rb.angularVelocity *= friction;
    }

    protected void AddDownForce()
    {
        _rb.AddForce(-Vector3.up * downForce * _rb.velocity.magnitude);
    }

    protected void ApplySteer(float steerF, float steerI)
    {
        //print("Abs(steerForce):" + Mathf.Abs(steerF) + " * ((Mathf.Floor(_velZ) * 3.6f) / topSpeed):" + ((Mathf.Floor(_velZ) * 3.6f) / topSpeed) + " = " + Mathf.Abs(steerF) * ((Mathf.Floor(_velZ)*3.6f) / topSpeed));
        var tempForce = Mathf.Abs(steerF) * ((Mathf.Floor(currentVelZ) * 3.6f) / topSpeed);
        if (tempForce < maxSteerForce * K.MIN_FORCE_MULTIPLIER && steerI != 0) tempForce = maxSteerForce * K.MIN_FORCE_MULTIPLIER;
        if (currentVelZ > 5)
        {
            if (steerI > 0)
            {
                _rb.AddForceAtPosition(leftTurnWheelPosition.right * tempForce, leftTurnWheelPosition.position, ForceMode.Acceleration);
            }
            else if (steerI < 0)
            {
                _rb.AddForceAtPosition(-rightWheelTurnPosition.right * tempForce, rightWheelTurnPosition.position, ForceMode.Acceleration);
            }
        }
        else if (currentVelZ < -5)
        {
            if (steerI < 0)
            {
                _rb.AddForceAtPosition(leftTurnWheelPosition.right * tempForce, leftTurnWheelPosition.position, ForceMode.Acceleration);
            }
            else if (steerI > 0)
            {
                _rb.AddForceAtPosition(-rightWheelTurnPosition.right * tempForce, rightWheelTurnPosition.position, ForceMode.Acceleration);
            }
        }
    }

    protected void ApplyDrive(float forwardForce, float accI, float brakeF)
    {
        //print("VelZ:" + Mathf.Floor(_velZ) + " / topSpeed:" + topSpeed + " = " + (Mathf.Floor(_velZ) / topSpeed));
        var tempForce = forwardForce * (Mathf.Floor(currentVelZ*K.KPH_TO_MPS_MULTIPLIER) / topSpeed);
        if (tempForce < maxForce * K.MIN_FORCE_MULTIPLIER && accI > 0) tempForce = maxForce * K.MIN_FORCE_MULTIPLIER;
        if (brakeF < 0)
        {
            _rb.AddRelativeForce(0, 0, brakeF, ForceMode.Acceleration);
        }
        else {
            if (isGrounded) _rb.AddRelativeForce(0, 0, tempForce, ForceMode.Acceleration);
            //else _rb.AddRelativeForce(0, 0, tempForce * 1.1f, ForceMode.Acceleration);
        }
    }
    protected void ResetCar()
    {
        if (_lastCheckpoint == null) return;

        _rb.velocity = Vector3.zero;
        var temp = Physics.RaycastAll(_lastCheckpoint.transform.position, -_lastCheckpoint.transform.up, Mathf.Infinity);
        foreach (var item in temp)
        {
            if (item.collider.tag == "Track")
            {
                transform.position = item.point + Vector3.up;
                transform.rotation = _lastCheckpoint.transform.rotation;
                return;
            }
        }
//          transform.position = _lastCheckpoint.GetRespawnPoint(transform.position) + Vector3.up;
        
    }
    private void CheckCarFlipped()
    {
        if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270 && currentVelZ < 1 && currentVelZ > -1) resetTimer += Time.deltaTime;
        else resetTimer = 0;
        if (resetTimer > resetTime) FlipCar();
    }
    private void FlipCar()
    {
        //transform.rotation = Quaternion.LookRotation(transform.forward);
        transform.position += Vector3.up * 0.5f;
        Vector3 forwardDirection = _lastCheckpoint.nextCheckpoint.transform.position - transform.position;
        forwardDirection.y = transform.position.y;
        transform.forward = forwardDirection;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        resetTimer = 0;
    }

    protected virtual void NitroInput(float nitroInput, float brakeInput)
    {
        if (nitroInput > 0 && isGrounded && !_nitroEmpty)
        {
            _modeNitro = true;            
        }
        else /*if (gameObject.GetComponent<BuggyController>() != null)*/
        {
            _modeNitro = false;            
        }

        if (_modeNitro)
        {
            if (brakeInput < 0) _rb.AddRelativeForce(0, 0, -nitroPower);
            else _rb.AddRelativeForce(0, 0, nitroPower);
            _nitroTimer -= Time.deltaTime;
            if (_nitroTimer < 0)
            {
                _modeNitro = false;
                _nitroEnd = true;
                _nitroEmpty = true;
            }
        }
    }
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

    protected void CapSpeed()
    {
        if (!_modeNitro)
        {
            if (currentVelZ > (topSpeed / K.KPH_TO_MPS_MULTIPLIER))
            {
                _rb.velocity -= .4f * _rb.velocity.normalized;
                currentVelZ = transform.InverseTransformDirection(_rb.velocity).z;
                if (currentVelZ > (topSpeed / K.KPH_TO_MPS_MULTIPLIER))
                {
                    _rb.velocity = (topSpeed / K.KPH_TO_MPS_MULTIPLIER) * _rb.velocity.normalized;
                    currentVelZ = transform.InverseTransformDirection(_rb.velocity).z;                    
                }
            }
        }
    }

    public void PushRamp(float amount)
    {
        if (_isGroundedRamp) _rb.AddForce(transform.forward * amount);
    }

    private void ApplySteering(Vector3 relativeVelocity)
    {
        float turnRadius = 3f / Mathf.Sin((90 - (_steerInput * 30)) * Mathf.Deg2Rad);
        float minMaxTurn = EvaluateSpeedToTurn(_rb.velocity.magnitude);
        float turnSpeed = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
        transform.RotateAround(transform.position + transform.right * turnRadius * _steerInput, transform.up,
                                turnSpeed * Mathf.Rad2Deg * Time.deltaTime * _steerInput);
    }
    private float EvaluateSpeedToTurn(float speed)
    {
        if (speed > topSpeed / 2) return minimumTurn;
        var speedIndex = 1 - (speed / (topSpeed / 2));

        return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (GetComponent<InputControllerPlayer>() &&
            hit.gameObject.layer != K.LAYER_RAMP &&
            hit.gameObject.layer != K.LAYER_SIDEGROUND &&
            hit.gameObject.layer != K.LAYER_MISSILE
            )
        {
            Camera.main.GetComponent<ShakeCamera>().DoShake();
        }
    }
}
