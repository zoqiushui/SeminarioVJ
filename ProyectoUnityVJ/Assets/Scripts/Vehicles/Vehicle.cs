using UnityEngine;
using System.Collections;

public abstract class Vehicle : MonoBehaviour
{
    public float positionWeight { get; protected set; }
    public float lapCount { get; protected set; }
}
