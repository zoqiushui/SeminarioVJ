using UnityEngine;
using System.Collections;

public class Molotov : MonoBehaviour {
    
    public float damage;
    public float fallSpeed;
    public GameObject fire;
    private Rigidbody _rb;
    private RaycastHit hit;
    public LayerMask maskGround;
	// Use this for initialization
	void Start ()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
      //  transform.position += -transform.up * fallSpeed * Time.deltaTime;
	}
    void FixedUpdate()
    {
        _rb.AddForce(-transform.up , ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == K.LAYER_GROUND)
            Instantiate(fire, col.contacts[0].point + transform.up, fire.transform.localRotation);
        else
        {
            print("abajo");
            Physics.Raycast(col.contacts[0].point, -transform.up, out hit, maskGround);
            Instantiate(fire, hit.point + transform.up, fire.transform.localRotation);
        }
        Destroy(this.gameObject);
    }
}
