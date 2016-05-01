using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
    public static IngameUIManager instance;

    public RawImage speedpmeterNeedleImage;
    public Text lapsText;

    private float _playerSpeed;
    private Vector3 _playerSpeedometerRotation;
    private int _playerLaps;

    private void Awake()
    {
        if (instance == null) instance = this;
    }   

    private void Update()
    {
        lapsText.text = "Laps " + _playerLaps + "/MAX";
        _playerSpeedometerRotation.z = (_playerSpeed * K.SPEEDOMETER_MAX_ANGLE) + K.SPEEDOMETER_MIN_ANGLE;
        speedpmeterNeedleImage.transform.eulerAngles = _playerSpeedometerRotation;
    }

    /// <summary>
    /// Recibe la velocidad actual del jugador dividido la velocidad maxima que puede mostrar el velocimetro.
    /// </summary>
    /// <param name="speed">Velocidad actual / Velocidad maxima del velocimetro</param>
    public void GetPlayerSpeed(float speed)
    {
        _playerSpeed = speed;
    }

    public void GetPlayerLapCount(int laps)
    {
        _playerLaps = laps;
    }
}
