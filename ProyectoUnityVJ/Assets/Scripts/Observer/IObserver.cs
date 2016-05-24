using UnityEngine;
using System.Collections;

public interface IObserver
{
    void Notify(Vehicle caller, string msg);
}
