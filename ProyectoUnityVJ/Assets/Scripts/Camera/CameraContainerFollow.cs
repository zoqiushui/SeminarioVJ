using UnityEngine;
using System.Collections;

public class CameraContainerFollow : MonoBehaviour
{
    public Transform target = null;
    private float _height;
    private float _distanceHeight;
    private Rigidbody _rbTarget;
    public float rotationDamping = 3f;
    public float distance = 7;

    /*private float _minDistance;
    private float _maxDistance;*/

    void Awake()
    {
        if (!target) return;
        _rbTarget = target.GetComponent<Rigidbody>();
        _height = transform.localPosition.y;
        _distanceHeight = _height - target.position.y;
        // _minDistance = Vector3.Distance(transform.position, target.transform.position /*+ Vector3.up * 2f*/);
        //_maxDistance = _minDistance - 1f;*/
        // _crosshairFixedZPostion = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
    }

    void Update()
    {
        float speed = (_rbTarget.transform.InverseTransformDirection(_rbTarget.velocity).z) * K.KPH_TO_MPS_MULTIPLIER;
        float speedFactor = Mathf.Clamp01(speed / target.GetComponent<Vehicle>().topSpeed);

        //float currentDistance = Mathf.Lerp(_minDistance, _maxDistance, speedFactor);

        //Calcula los angulos de rotación actuales
        float targetRotationAngle = target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;

        // Rotación de camara en marcha atrás.
        //     if (speed < -2) targetRotationAngle = target.eulerAngles.y + 180;

        //Damp de la rotación en el eje Y.
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDamping * Time.deltaTime);
        //Convierte el angulo a rotación.
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        //Altura de la camara.
        Vector3 newTargetPosition = target.position + new Vector3(0, _distanceHeight, 0);

        transform.position = newTargetPosition;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.LookAt(target.position + Vector3.up * 3);
    }
}
