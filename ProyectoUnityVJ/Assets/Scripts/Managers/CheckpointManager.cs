using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public float checkpointValue { get; private set; }

    private List<Checkpoint> _checkpointsList;
    private Dictionary<GameObject,int> _vehiclesDictionary; // <Vehiculo, proximo checkpoint>

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        _checkpointsList = new List<Checkpoint>();
        _vehiclesDictionary = new Dictionary<GameObject,int>();
        foreach (var checkpoint in GameObject.Find("CHECKPOINTS").GetComponentsInChildren<Checkpoint>())
        {
            _checkpointsList.Add(checkpoint);
        }
        _vehiclesDictionary.Add(GameObject.FindGameObjectWithTag("Player"), 0);
        checkpointValue = (float)1/_checkpointsList.Count;
        int aux = 1;
        foreach (var chk in _checkpointsList)
        {
            chk.SetNextCheckpoint(_checkpointsList[aux]);
            if (aux == _checkpointsList.Count-1)
            {
                aux = 0;
            }
            else
            {
                aux++;
            }
        }
    }

    public bool CheckVehicleCheckpoint(GameObject vehicle, Checkpoint chk)
    {
        if (_vehiclesDictionary[vehicle] == _checkpointsList.IndexOf(chk))
        {
            if (_vehiclesDictionary[vehicle] == _checkpointsList.Count - 1)
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
}
