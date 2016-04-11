using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestController : MonoBehaviour
{
    public float maxTorque;
    public Transform centerOfMass;
    public Text speedText;
    public GameObject trailRenderModel;
    public WheelCollider[] wheelColliders;
    public Transform[] tiresCar;
    private Rigidbody _rb;
    [SerializeField] private float currentSpeed;
    public float maxSpeed;
    public float maxReverseSpeed;
    public float brakeTorque;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass.localPosition;
    }
	
	void Update ()
    {
        UpdateTiresPosition();
        UIText();
        currentSpeed = _rb.velocity.magnitude * 3.6f;
	}
    void FixedUpdate()
    {
        //brakeTorque: fuerza de frenado
        var motorInput = Input.GetAxis("Vertical");

        if (currentSpeed > maxSpeed && motorInput > 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
        }
        else if (currentSpeed > maxReverseSpeed && motorInput < 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 0;
        }

        if (motorInput == 0) _rb.drag = _rb.velocity.magnitude / 100;

        float steer = Input.GetAxis("Horizontal");
        float finalAngle = steer * K.JEEP_MAX_STEERING_ANGLE;
        wheelColliders[0].steerAngle = finalAngle;
        wheelColliders[1].steerAngle = finalAngle;

        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].motorTorque = motorInput * maxTorque;

        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
            _rb.drag += 4 * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 0;
            _rb.drag = 0;
        }

    }
    private void UpdateTiresPosition()
    {
        for (int i = 0; i < tiresCar.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);

            tiresCar[i].position = pos;
            tiresCar[i].rotation = quat;
        }
    }

    protected void UIText()
    {
        //_localVelocity = transform.InverseTransformDirection(_rb.velocity);
        //speedText.text = "Speed: " + (int)currentSpeed;
        IngameUIManager.instance.SetPlayerSpeed(currentSpeed / K.SPEEDOMETER_MAX_SPEED);
    }
}
