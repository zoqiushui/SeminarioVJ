﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
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
    public List<Transform> wheelMeshList;
    public List<Suspension> wheelSuspensionList;
    public float maxSteerForce, maxForce, brakeForce;
    [Range(0, 1)]
    public float airFriction = 0.98f;
    [Range(0, 1)]
    public float groundFriction = 0.96f;
    public float currentVelZ { get; private set; }

    private bool _modeNitro = false;
    public float nitroPower;
    public float nitroTimer;
    private float _nitroTimer;
    public float rechargeNitro;
    private bool _nitroEnd;
    public Image visualNitro;
    private float _lapsEnded;
    private bool _canRechargeNitro;
    private bool _nitroEmpty;
    private bool _countInAir;
    private float _timerWrongDirection;

    public Camera rearMirror;
    protected float _steerInput, _motorInput;
    public Text wrongDirectionText;
    private float _finalAngle;

    protected int _checkpointNumber;
    protected Checkpoint _lastCheckpoint;
    protected List<IObserver> _obsList;
    protected bool _isDestroyed;
    protected CheckpointManager _checkpointMananagerReference;
    protected IngameUIManager _ingameUIMananagerReference;
    protected SoundManager _soundManagerReference;
    protected GameManager _gameManagerReference;
    protected Rigidbody _rb;
    protected bool _isGrounded;
    protected List<TrailRenderer> _wheelTrails;

    protected float friction
    {
        get
        {
            if (_isGrounded)
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

        wrongDirectionText.gameObject.SetActive(false);
        vehicleName = PlayerPrefs.GetString("PilotName");
        Cursor.visible = false;
        lapCount = 0;
        positionWeight = -Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[0].transform.position);
        _checkpointNumber = 0;
        _nitroTimer = nitroTimer;
        _lapsEnded = 1;
        _nitroEmpty = false;
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

    public void SetCheckpoint(Checkpoint chk)
    {
        _checkpointNumber = _checkpointMananagerReference.checkpointsList.Count - 1 == _checkpointNumber ? 0 : _checkpointNumber + 1;
        _lastCheckpoint = chk;
        lapCount += _checkpointMananagerReference.checkpointValue;
    }

    public void Move(float accelInput, float brakeInput, float handbrakeInput, float steerInput, float nitroInput)
    {
        _steerInput = steerInput;
        currentVelZ = transform.InverseTransformDirection(_rb.velocity).z;
        var steerForce = steerInput * maxSteerForce;
        var forwardForce = accelInput * maxForce;
        var brakeForce = brakeInput * this.brakeForce;
        //_rb.drag = K.AIR_DRAG;
        foreach (var wheel in wheelSuspensionList)
        {
            _isGrounded = false;
            if (wheel.IsGrounded())
            {
                _isGrounded = true;
                //_rb.drag = 1;
                //CalculateSteerForceWithVelocity(out steerForce,steerInput); 
                break;
            }
        }
        ApplyDrive(forwardForce, accelInput, brakeForce);
        ApplySteer(steerForce, steerInput);
        Drag(accelInput, brakeInput);
        AddDownForce();
        CapSpeed();
        NitroInput(nitroInput,brakeInput);
        NotifyObserver(K.OBS_MESSAGE_SPEED);

        if (accelInput == 0 || _nitroEnd)
        {
            _rb.drag = _rb.velocity.magnitude / 30f;
            if (currentVelZ < topSpeed) _nitroEnd = false;
        }
    }

    protected void Update()
    {
        positionWeight = Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[_checkpointNumber].transform.position);
        
        UpdateTyres();
        ChangeToRearView();
        CheckBars();
        CheckDirection();
    }
    private void ChangeToRearView()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rearMirror.enabled = true;
            Camera.main.depth = -1f;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            rearMirror.enabled = false;
            Camera.main.depth = 0f;
        }
    }

    protected void UpdateTyres()
    {
        _finalAngle = _steerInput * K.JEEP_MAX_STEERING_ANGLE;
        for (int i = 0; i < wheelMeshList.Count; i++)
        {
            if (i < 2f)
            {
                Vector3 steerAngle = wheelMeshList[i].localEulerAngles;
                steerAngle.y = _finalAngle;
                wheelMeshList[i].localEulerAngles = steerAngle;
            }
        }
    }    

    protected void CalculateSteerForceWithVelocity(out float sf, float si)
    {
        var a = (currentVelZ / topSpeed);
        sf = (a * maxSteerForce);
        print(sf);
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
        var tempForce = forwardForce * (Mathf.Floor(currentVelZ) / topSpeed);
        if (tempForce < maxForce * K.MIN_FORCE_MULTIPLIER && accI > 0) tempForce = maxForce * K.MIN_FORCE_MULTIPLIER;
        if (brakeF < 0)
        {
            _rb.AddRelativeForce(0, 0, brakeF, ForceMode.Acceleration);
        }
        else {
            if (_isGrounded) _rb.AddRelativeForce(0, 0, tempForce, ForceMode.Acceleration);
            //else _rb.AddRelativeForce(0, 0, tempForce * 1.1f, ForceMode.Acceleration);
        }
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
    private void NitroInput(float nitroInput, float brakeInput)
    {
        if (nitroInput > 0 && _isGrounded && !_nitroEmpty)
        {
            _modeNitro = true;
            Camera.main.GetComponent<Bloom>().enabled = true;
            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
            Camera.main.GetComponent<MotionBlur>().enabled = true;
        }
        else
        {
            _modeNitro = false;
            Camera.main.GetComponent<Bloom>().enabled = false;
            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = false;
            Camera.main.GetComponent<MotionBlur>().enabled = false;
        }

        if (_modeNitro)
        {
            if (brakeInput < 0) _rb.AddForce(transform.forward * -nitroPower);
            else _rb.AddForce(transform.forward * nitroPower);
            _nitroTimer -= Time.deltaTime;
            if (_nitroTimer < 0)
            {
                _modeNitro = false;
                _nitroEnd = true;
                _nitroEmpty = true;
                Camera.main.GetComponent<Bloom>().enabled = false;
                Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = false;
                Camera.main.GetComponent<MotionBlur>().enabled = false;
            }
        }
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

    protected void CapSpeed()
    {
        if (currentVelZ > (topSpeed / K.KPH_TO_MPS_MULTIPLIER))
        {
            _rb.velocity = (topSpeed / K.KPH_TO_MPS_MULTIPLIER) * _rb.velocity.normalized;
            currentVelZ = transform.InverseTransformDirection(_rb.velocity).z;
        }
    }
}
