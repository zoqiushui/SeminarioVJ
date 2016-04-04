using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestController : MonoBehaviour
{
    public float maxTorque = 50f;
    public Transform centerOfMass;
    public Text speedText; // PARA TEST
    public GameObject trailRenderModel;
    public WheelCollider[] wheelColliders;
    public Transform[] tiresCar;
    private Rigidbody _rb;
    private float currentSpeed;
    public float maxSpeed;
    private float minSpeed;
    public float maxReverseSpeed;
    public float brakeTorque;
    private float acceleration;
    public float decelerationRate = 10.0f;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass.localPosition;
        minSpeed = 0f;
    }
	
	void Update ()
    {
        UpdateTiresPosition();
        UIText();
        currentSpeed = _rb.velocity.magnitude * 3.6f;
	}
    void FixedUpdate()
    {
        float acce = 0f;

        if (Input.GetAxis("Vertical") > 0)
        {
            if (currentSpeed < maxSpeed) acceleration = Input.GetAxis("Vertical");
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (currentSpeed > minSpeed)
            {
                acceleration = Input.GetAxis("Vertical");
            }
            else
            {
                acceleration = Input.GetAxis("Vertical");
            }
        }
        else if(Input.GetAxis("Vertical") == 0)
        {
            if (currentSpeed > minSpeed && acceleration > 0)
            {
               /* acceleration = acceleration - (decelerationRate * Time.deltaTime);
                acceleration = Mathf.Clamp(acceleration, minSpeed, maxSpeed);*/
            }
        }
        Debug.Log(acceleration);
        acce = acceleration;
        float steer = Input.GetAxis("Horizontal");
        float finalAngle = steer * 45f;
        wheelColliders[0].steerAngle = finalAngle;
        wheelColliders[1].steerAngle = finalAngle;
        for (int i = 0; i < wheelColliders.Length; i++) wheelColliders[i].motorTorque = acce * maxTorque;
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
        speedText.text = "Speed: " + currentSpeed;
    }
}
