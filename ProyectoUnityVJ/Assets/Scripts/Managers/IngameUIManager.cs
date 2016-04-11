using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
    public static IngameUIManager instance;

    public RawImage speedpmeterNeedleImage;

    private float _playerSpeed;
    private Vector3 _playerSpeedometerRotation;

    private void Awake()
    {
        if (instance == null) instance = this;
    }   

    private void Update()
    {
        _playerSpeedometerRotation.z = (_playerSpeed * K.SPEEDOMETER_MAX_ANGLE) + K.SPEEDOMETER_MIN_ANGLE;
        speedpmeterNeedleImage.transform.eulerAngles = _playerSpeedometerRotation;
    }

    /// <summary>
    /// Recibe la velocidad actual del jugador dividido la velocidad maxima que puede mostrar el velocimetro.
    /// </summary>
    /// <param name="speed">Velocidad actual / Velocidad maxima del velocimetro</param>
    public void SetPlayerSpeed(float speed)
    {
        _playerSpeed = speed;
    }
}
