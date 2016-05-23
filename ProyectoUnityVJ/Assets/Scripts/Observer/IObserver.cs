using UnityEngine;
using System.Collections;

public interface IObserver
{
    void Notify(string msg);
}
