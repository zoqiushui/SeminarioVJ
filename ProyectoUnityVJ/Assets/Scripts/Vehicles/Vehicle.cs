using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Vehicle : MonoBehaviour, IObservable
{
    public float positionWeight { get; protected set; }
    public float lapCount { get; protected set; }
    public string vehicleName;
    protected List<IObserver> _obsList;
    protected bool _isDestroyed;
    protected CheckpointManager _checkpointMananagerReference;
    protected IngameUIManager _ingameUIMananagerReference;
    protected SoundManager _soundManagerReference;
    protected GameManager _gameManagerReference;
    public float wheelRadius;

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
    }

    public abstract void GetInput(float _accel, float _brake,float _handbrake, float _steer, float _nitro);
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
}
