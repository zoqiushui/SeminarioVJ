using UnityEngine;
using System.Collections;

public abstract class Vehicle : MonoBehaviour
{
    public float positionWeight { get; protected set; }
    public float lapCount { get; protected set; }
    public string vehicleName;
    protected ReferencesManager _refManager;

    protected virtual void Start()
    {
        _refManager = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<ReferencesManager>();
    }
}
