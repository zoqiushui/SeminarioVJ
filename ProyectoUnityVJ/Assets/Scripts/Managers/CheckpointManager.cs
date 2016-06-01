using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointManager : Manager
{
    public float checkpointValue { get; private set; }
    public List<Checkpoint> checkpointsList { get; private set; }

    private Dictionary<Vehicle, int> _vehiclesDictionary; // <Vehiculo, proximo checkpoint>

    private void Awake()
    {
        //if (instance == null) instance = this;
        checkpointsList = new List<Checkpoint>();
        _vehiclesDictionary = new Dictionary<Vehicle, int>();
        foreach (var checkpoint in GameObject.FindGameObjectWithTag(K.TAG_CHECKPOINTS).GetComponentsInChildren<Checkpoint>())
        {
            checkpointsList.Add(checkpoint);
        }
        _vehiclesDictionary.Add(GameObject.FindGameObjectWithTag(K.TAG_PLAYER).GetComponent<Vehicle>(), 0);
        var temp = GameObject.FindGameObjectsWithTag("Target");
        print(temp.Length);
        for (int i = 0; i < temp.Length; i++)
        {
            _vehiclesDictionary.Add(temp[i].GetComponent<Vehicle>(), 0);
        }
        checkpointValue = (float)1 / checkpointsList.Count;
        int aux = 1;
        foreach (var chk in checkpointsList)
        {
            chk.SetNextCheckpoint(checkpointsList[aux]);
            if (aux == checkpointsList.Count - 1)
            {
                aux = 0;
            }
            else
            {
                aux++;
            }
        }
    }

    public bool CheckVehicleCheckpoint(Vehicle vehicle, Checkpoint chk)
    {
        if (_vehiclesDictionary[vehicle] == checkpointsList.IndexOf(chk))
        {
            if (_vehiclesDictionary[vehicle] == checkpointsList.Count - 1)
            {
                _vehiclesDictionary[vehicle] = 0;
                return true;
            }
            else
            {
                _vehiclesDictionary[vehicle]++;
                return true;
            }
        }
        return false;
    }

    public override void Notify(Vehicle caller, string msg)
    {
        switch (msg)
        {
            case K.OBS_MESSAGE_DESTROYED:
                if (_vehiclesDictionary.ContainsKey(caller))
                {
                    _vehiclesDictionary.Remove(caller);
                }
                break;

            default:
                break;
        }
    }
}
