using UnityEngine;
using System.Collections;

public class MiniGun : Weapon
{

    public float CooldownTime;
    public short InputKey;
    public GameObject particleEffect;
    public float forceImpact;
    public float maxDistance;
    private Vector3 direction;
    private RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        shootButtom = InputKey;
        cooldown = CooldownTime;
        particleEffect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(shootButtom))
            particleEffect.SetActive(true);
        else if (particleEffect.active)
            particleEffect.SetActive(false);

        ShootDownButtom();

        if (canShoot) Shoot();
	}

    public override void Shoot()
    {

        canShoot = false;
        base.Shoot();
        direction = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position, direction * maxDistance, Color.blue);

        if (Physics.Raycast(transform.position, direction, out hit,maxDistance))
        {
            hit.rigidbody.AddForceAtPosition(direction * forceImpact, hit.point);
        }
    }
}
