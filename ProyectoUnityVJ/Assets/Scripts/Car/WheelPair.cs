using UnityEngine;
using System.Collections;
[System.Serializable]
public class WheelPair
{
    public WheelCollider rightWheel, leftWheel;
    public GameObject rightWheelMesh, leftWheelMesh;
    public bool motor, steer;
}
