using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour
{
    public Vehicle vehiclePlayer;
    private List<Vehicle> vehiclesIAs = new List<Vehicle>();
    private List<Vehicle> positionTable = new List<Vehicle>();

    void Awake()
    {
    }
	// Use this for initialization
	void Start ()
    {
        var temp = GameObject.FindGameObjectsWithTag("VehicleIA");
        for (int i = 0; i < temp.Length; i++)
        {
            vehiclesIAs.Add(temp[i].GetComponent<Vehicle>());
        }
        StartCoroutine(CalculateChanges());
	}

    IEnumerator CalculateChanges()
    {
        yield return new WaitForSeconds(5f);
        //print("calculating");
        positionTable.Clear();
        positionTable = new List<Vehicle>();
        GetComponentInParent<IngameUIManager>().GiveListData(positionTable);
        if (positionTable.Count != 0)
        {
            if (ChechFirst())
                print("player is first");
            else if (ChechLast())
                print("player is last");
            else
                CheckEveryOne();
        }
       yield return StartCoroutine(CalculateChanges());
    }

    private bool ChechFirst()
    {
        if (positionTable[0].gameObject == vehiclePlayer.gameObject)
        {
            //print("player is first");
            for (int i = 1; i < positionTable.Count; i++)
            {
                if (Vector3.Distance(vehiclePlayer.transform.position, positionTable[i].transform.position) < 20f)
                    positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("normal");
                else positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("high");
            }
            return true;
        }
        else return false;
    }

    private bool ChechLast()
    {
        if (positionTable[positionTable.Count-1] == vehiclePlayer)
        {
            print("player is last");
            for (int i = 0; i < positionTable.Count-1; i++)
            {
                if (Vector3.Distance(vehiclePlayer.transform.position, positionTable[i].transform.position) < 40f)
                    positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("normal");
                else positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("low");
            }
            return true;
        }
        else return false;
    }

    void CheckEveryOne()
    {
        int playerIndex = positionTable.IndexOf(vehiclePlayer);

        for (int i = 0; i < positionTable.Count; i++)
        {
            if (i != playerIndex)
            {
                if (i < playerIndex)
                {
                    if (Vector3.Distance(vehiclePlayer.transform.position, positionTable[i].transform.position) < 40f)
                        positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("normal");
                    else positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("low");
                }
                if (i > playerIndex)
                {
                    if (Vector3.Distance(vehiclePlayer.transform.position, positionTable[i].transform.position) < 20f)
                        positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("normal");
                    else positionTable[i].GetComponentInParent<IAVehicle>().ChangeGear("high");
                }
            }
        }
    }
}
