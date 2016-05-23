using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ReferencesManager : MonoBehaviour, IObservable
{
    public CheckpointManager chkPointManagerReference { get; private set; }
    public GameManager gameManagerReference { get; private set; }
    public IngameUIManager ingameUIManagerReference { get; private set; }
    public SoundManager soundManagerReference { get; private set; }

    private List<Vehicle> _racerList;
    private List<IObserver> _obsList;

    private void Awake()
    {
        chkPointManagerReference = GetComponent<CheckpointManager>();
        gameManagerReference = GetComponent<GameManager>();
        ingameUIManagerReference = GetComponent<IngameUIManager>();
        soundManagerReference = GetComponent<SoundManager>();
        _racerList.AddRange(GameObject.FindGameObjectWithTag(K.TAG_VEHICLES).GetComponentsInChildren<Vehicle>());
    }

    public void AddObserver(IObserver obs)
    {
        if (!_obsList.Contains(obs)) _obsList.Add(obs);
    }

    public void NotifyObserver(string msg)
    {
        foreach (var obs in _obsList)
        {
            obs.Notify(msg);
        }
    }

    public void RemoveObserver(IObserver obs)
    {
        if (_obsList.Contains(obs)) _obsList.Remove(obs);
    }
}
