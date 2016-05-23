using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IObservable
{
    void AddObserver(IObserver obs);
    void RemoveObserver(IObserver obs);
    void NotifyObserver(string msg);
}
