using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IngameUIManager : Manager
{
    public RawImage speedpmeterNeedleImage;
    public Text lapsText, positionsText;
    public Color endRaceColor, enemiesColor, playerColor, destroyedColor;

    private float _playerSpeed;
    private Vector3 _playerSpeedometerRotation;
    private int _playerLaps;
    private List<Vehicle> _racerList;
    private List<string> _endRacerList, _destroyedRacers;
    private string _positionsTextString, _playerName;

    private void Start()
    {
        _racerList = new List<Vehicle>();
        _racerList.AddRange(GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentsInChildren<Vehicle>());
        _endRacerList = new List<string>();
        _destroyedRacers = new List<string>();
        _playerName = GameObject.FindGameObjectWithTag(K.TAG_PLAYER).GetComponent<Vehicle>().vehicleName;
        print(_playerName);

        //InfoIAPosition();
    }

    private void Update()
    {
        int count = 1;
        _positionsTextString = "";
        foreach (var racerSurvivedName in _endRacerList)
        {
            _positionsTextString += "<color=" + ColorTypeConverter.ToRGBHex(endRaceColor) + ">" + count + "." + " " + racerSurvivedName + "</color>\n";
            count++;
        }
        SortRacerList(_racerList);
        foreach (var racer in _racerList)
        {
            if (racer == null)
            {
                _racerList.Remove(racer);
                break;
            }
            if (!_endRacerList.Contains(racer.vehicleName) && !_destroyedRacers.Contains(racer.vehicleName))
            {
                if (racer.GetComponent<BuggyController>())
                {
                    _positionsTextString += "<color=" + ColorTypeConverter.ToRGBHex(playerColor) + ">" + count + "." + " " + racer.vehicleName + "</color>\n";

                }
                else {
                    _positionsTextString += "<color=" + ColorTypeConverter.ToRGBHex(enemiesColor) + ">" + count + "." + " " + racer.vehicleName + "</color>\n";
                }

                count++;
            }

        }
        _destroyedRacers.Reverse();
        foreach (var destroyedRacerName in _destroyedRacers)
        {
            _positionsTextString += "<color=" + ColorTypeConverter.ToRGBHex(destroyedColor) + ">" + count + "." + " " + destroyedRacerName + "</color>\n";
            count++;
        }
        _destroyedRacers.Reverse();
        positionsText.text = _positionsTextString;
        if (_playerLaps < K.MAX_LAPS) lapsText.text = "Laps " + (_playerLaps + 1) + "/" + K.MAX_LAPS;
        _playerSpeedometerRotation.z = (_playerSpeed * K.SPEEDOMETER_MAX_ANGLE) + K.SPEEDOMETER_MIN_ANGLE;
        speedpmeterNeedleImage.transform.eulerAngles = _playerSpeedometerRotation;

    }

    public int GetPLayerPosition(string playerName)
    {
        if (_endRacerList.Contains(playerName))
        {
            return _endRacerList.IndexOf(playerName);
        }
        else return -1;
    }

    /*private void InfoIAPosition()
    {
        if (_racerList != null)
        {
            List<Vehicle> _temp = _racerList;

            if (_temp[0].gameObject.layer == K.LAYER_PLAYER)
            {
                for (int i = 1; i < _temp.Count; i++)
                {
                    if (i == _temp.Count - 1)
                        _temp[i].gameObject.GetComponent<IAVehicle>().ChangeGear("high");
                    else
                        _temp[i].gameObject.GetComponent<IAVehicle>().ChangeGear("normal");
                }
            }
            else if (_temp[_temp.Count - 1].gameObject.layer == K.LAYER_PLAYER)
            {
                for (int i = 0; i < _temp.Count - 1; i++)
                {
                    if (i == 0)
                        _temp[i].gameObject.GetComponent<IAVehicle>().ChangeGear("low");
                    else
                        _temp[i].gameObject.GetComponent<IAVehicle>().ChangeGear("normal");
                }
            }
            Invoke("InfoIAPosition", 2f);
        }
    }*/

    /// <summary>
    /// Ordeno de mayor a menor el valor de la posicion de cada corredor.
    /// </summary>
    /// <param name="_rL">Lista de corredores</param>
    private void SortRacerList(List<Vehicle> _rL)
    {
        for (int i = 0; i < _rL.Count - 1; i++)
        {
            for (int j = i + 1; j < _rL.Count; j++)
            {
                if (_rL[j].lapCount > _rL[i].lapCount)
                {
                    var aux = _rL[i];
                    _rL[i] = _rL[j];
                    _rL[j] = aux;
                }
                else if (_rL[j].lapCount == _rL[i].lapCount && _rL[j].positionWeight < _rL[i].positionWeight)
                {
                    var aux = _rL[i];
                    _rL[i] = _rL[j];
                    _rL[j] = aux;
                }
            }
        }
    }

    public void OnRestartButtonClicked()
    {
        if(K.pilotIsAlive==false)
        {
            SceneManager.LoadScene(2);
            PlayerPrefs.SetString("PilotName", "");
            PlayerPrefs.SetInt("Face", 0);
            PlayerPrefs.SetInt("Hair", 0);
            PlayerPrefs.SetInt("FaceHair", 0);
            PlayerPrefs.SetInt("Accesory", 0);
            PlayerPrefs.SetInt("HairColor", 0);
            PlayerPrefs.SetInt("SkinColor", 0);

            //Delete traits
            PlayerPrefs.SetInt("BonusMaxSpeed", 0);
            PlayerPrefs.SetInt("BonusTurbo", 0);
            PlayerPrefs.SetInt("BonusAcceleration", 0);
            PlayerPrefs.SetInt("BonusAmmo", 0);
            PlayerPrefs.SetInt("BonusAmmoReload", 0);
            PlayerPrefs.SetInt("BonusLessMineDamage", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Position", GetPLayerPosition(_playerName));
            SceneManager.LoadScene(4);
        }
        
    }

    public override void Notify(Vehicle caller, string msg)
    {
        switch (msg)
        {
            case K.OBS_MESSAGE_DESTROYED:
                if (!_destroyedRacers.Contains(caller.vehicleName))
                {
                    _destroyedRacers.Add(caller.vehicleName);
                }
                break;

            case K.OBS_MESSAGE_FINISHED:
                if (!_endRacerList.Contains(caller.vehicleName))
                {
                    _endRacerList.Add(caller.vehicleName);
                }
                break;

            case K.OBS_MESSAGE_SPEED:
                var auxVelZ = caller.currentVelZ;
                var rnd = Random.Range(-.5f, .5f);
                if (auxVelZ < 0)
                {
                    auxVelZ *= -1;
                }
                if (auxVelZ * K.KPH_TO_MPS_MULTIPLIER > caller.topSpeed)
                {
                    _playerSpeed = (caller.topSpeed + rnd) / K.SPEEDOMETER_MAX_SPEED;
                }
                else
                {
                    _playerSpeed = ((auxVelZ + rnd) * K.KPH_TO_MPS_MULTIPLIER) / K.SPEEDOMETER_MAX_SPEED;

                }

                //else _playerSpeed = ((VehicleController)caller).currentSpeed / K.SPEEDOMETER_MAX_SPEED;
                break;

            case K.OBS_MESSAGE_LAPCOUNT:
                _playerLaps = Mathf.FloorToInt(caller.lapCount);
                break;

            default:
                break;
        }
    }
}
