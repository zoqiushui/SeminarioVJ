using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public float checkpointValue { get; private set; }
    public List<Checkpoint> checkpointsList { get; private set; }

    private Dictionary<GameObject, int> _vehiclesDictionary; // <Vehiculo, proximo checkpoint>

    private void Awake()
    {
        if (instance == null) instance = this;
        checkpointsList = new List<Checkpoint>();
        _vehiclesDictionary = new Dictionary<GameObject, int>();
        foreach (var checkpoint in GameObject.Find("CHECKPOINTS").GetComponentsInChildren<Checkpoint>())
        {
            checkpointsList.Add(checkpoint);
        }
        _vehiclesDictionary.Add(GameObject.FindGameObjectWithTag("Player"), 0);
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

    public bool CheckVehicleCheckpoint(GameObject vehicle, Checkpoint chk)
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
}
