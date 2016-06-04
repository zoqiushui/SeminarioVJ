    using UnityEngine;
using System.Collections;

public class Burner : Weapon
{
    public float damage = 0.1f;
    public GameObject flames;

    private bool activeFeed;
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        isCrosshair = false;
        flames.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.disableShoot == false)
        {
            if (Input.GetMouseButton(shootButtom))
            {
                activeFeed = true;
                flames.SetActive(true);
            }
            if (Input.GetMouseButtonUp(shootButtom))
            {
                activeFeed = false;
                flames.SetActive(false);
            }
            ShootDownButtom();
        }

	}
    void OnTriggerStay(Collider cols)
    {
        if (activeFeed)
        {
            if (canShoot && cols.gameObject.layer == K.LAYER_IA)
            {
                Shoot();
                cols.gameObject.GetComponent<IAVehicle>().Damage(damage);
            }
        }
    }
    /*
    GameObject granade = (GameObject)GameObject.Instantiate(granadePrefab, transform.position + transform.forward * 3, Quaternion.identity);
    granade.transform.forward = transform.forward;
			granade.GetComponent<Rigidbody>().AddForce(transform.forward* 100, ForceMode.Force);*/
}
