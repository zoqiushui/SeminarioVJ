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
    private float steer = 0;
    private float throttle = 0;
    public int maximumTurn = 15;
    public int minimumTurn = 10;
    public Vector3 dragMultiplier;
    private bool handbrake;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass.localPosition;
        handbrake = false;
        dragMultiplier = new Vector3(2, 5, 0);
    }
	
	void Update ()
    {
        UpdateTiresPosition();
        UIText();
        currentSpeed = _rb.velocity.magnitude * 3.6f;
        GetInput();
    }
    void FixedUpdate()
    {
        //Transforma una dirección de world space a local space.
        var relativeVelocity = transform.InverseTransformDirection(_rb.velocity);

        //Maniobrabilidad
        ApplySteering(relativeVelocity);

        //ADDFORCE de Deslizamiento calculando dirección.
        UpdateDrag(relativeVelocity);
    }

    private void GetInput()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        float finalAngle = steer * K.JEEP_MAX_STEERING_ANGLE;
        wheelColliders[0].steerAngle = finalAngle;
        wheelColliders[1].steerAngle = finalAngle;
        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].motorTorque = throttle * maxTorque;

        if (currentSpeed > maxSpeed && throttle > 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
        }
        else if (currentSpeed > maxReverseSpeed && throttle < 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 0;
        }

        if (throttle == 0) _rb.drag = _rb.velocity.magnitude / 100f;
        else _rb.drag = 0f;
        CheckHandbrake();
    }

    private void CheckHandbrake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].brakeTorque = 1000;
            dragMultiplier.z += 10 * Time.deltaTime;
            handbrake = true;
        }
        else
        {
            dragMultiplier.z = 0;
            handbrake = false;
        }
    }
    private void UpdateDrag(Vector3 relativeVelocity)
    {
        Vector3 relativeDrag = new Vector3(-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), -relativeVelocity.y * Mathf.Abs(relativeVelocity.y),
                                            -relativeVelocity.z * Mathf.Abs(relativeVelocity.z));

        var drag = Vector3.Scale(dragMultiplier, relativeDrag);

        if (!handbrake) drag.x *= maxSpeed / relativeVelocity.magnitude;

        if (Mathf.Abs(relativeVelocity.x) < 5 && !handbrake) drag.x = -relativeVelocity.x * dragMultiplier.x;
        _rb.AddForce(transform.TransformDirection(drag) * _rb.mass * Time.deltaTime);
    }
    private void ApplySteering(Vector3 relativeVelocity)
    {
        float turnRadius = 3f / Mathf.Sin((90 - (steer * 30)) * Mathf.Deg2Rad);
        float minMaxTurn = EvaluateSpeedToTurn(_rb.velocity.magnitude);
        float turnSpeed = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
        transform.RotateAround(transform.position + transform.right * turnRadius * steer, transform.up,
                                turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steer);
    }
    private float EvaluateSpeedToTurn(float speed)
    {
        if (speed > maxSpeed / 2) return minimumTurn;
        var speedIndex = 1 - (speed / (maxSpeed / 2));
        return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
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
