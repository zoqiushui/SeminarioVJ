using UnityEngine;
using System.Collections;
using System;

public abstract class Manager : MonoBehaviour, IObserver
{
    public abstract void Notify(Vehicle caller, string msg);
}
