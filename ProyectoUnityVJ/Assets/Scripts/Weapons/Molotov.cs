using UnityEngine;
using System.Collections;

public class Molotov : MonoBehaviour {
    
    public float fallSpeed;
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
        {
            this.GetComponentInParent<MolotovBomb>().StartBurn(col.contacts[0].point + transform.up);
        }
        else
        {
            print("abajo");
            Physics.Raycast(col.contacts[0].point, -transform.up, out hit, maskGround);
            this.GetComponentInParent<MolotovBomb>().StartBurn(hit.point + transform.up);
        }
    }
}
