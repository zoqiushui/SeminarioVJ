using UnityEngine;
using System.Collections;

public class MiniGun : Weapon
{

    public float CooldownTime;
    public short InputKey;
    public GameObject particleEffect;
    public GameObject bulletPref;
    public float forceImpact;
    public float maxDistance;
    public Transform shootPoint;
  //  private Vector3 direction;
    private RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        isCrosshair = false;
        shootButtom = InputKey;
        cooldown = CooldownTime;
        particleEffect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(shootButtom))
            particleEffect.SetActive(true);
        else if (particleEffect.activeInHierarchy)
            particleEffect.SetActive(false);

        ShootDownButtom();

        if (canShoot) Shoot();
	}

    public override void Shoot()
    {

        canShoot = false;
        base.Shoot();
       // direction = shootPoint.TransformDirection(Vector3.forward);
        Instantiate(bulletPref, shootPoint.position + shootPoint.forward, shootPoint.rotation);
        
        /*
        Debug.DrawRay(shootPoint.position, direction * maxDistance, Color.blue);

        if (Physics.Raycast(shootPoint.position, direction, out hit,maxDistance))
        {
            if(hit.rigidbody!=null)
                hit.rigidbody.AddForceAtPosition(direction * forceImpact, hit.point);
        }*/
    }
}
