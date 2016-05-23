using UnityEngine;
using System.Collections;

public class MiniGun : Weapon
{
    public GameObject particleEffect;
    public GameObject bulletPref;
    public float forceImpact;
    public float maxDistance;
    public Transform shootPoint;
  //  private Vector3 direction;
    private RaycastHit hit;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        isCrosshair = false;
        particleEffect.SetActive(false);
        _ammoTimer = ammoTimer;
    }

    // Update is called once per frame
    void Update()
    {
        /*    if (Input.GetMouseButton(shootButtom)) particleEffect.SetActive(true);
            else if (particleEffect.activeInHierarchy) particleEffect.SetActive(false);*/

        CheckAmmoBar();
        ShootDownButtom();

        if (canShoot && visualAmmo.fillAmount > 0 && !ammoEmpty)   Shoot();
        else
            particleEffect.SetActive(false);
        
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = _ammoTimer / ammoTimer;
        visualAmmo.fillAmount = calc_ammo;
        ReloadAmmo();

        if (visualAmmo.fillAmount == 0) ammoEmpty = true;
    }

    private void ReloadAmmo()
    {
        if (_ammoTimer < ammoTimer && ammoEmpty) _ammoTimer += Time.deltaTime / reloadSpeed;

        if (visualAmmo.fillAmount == 1)
        {
            ammoEmpty = false;
            _ammoTimer = ammoTimer;
        }
    }
    private void ammoInput()
    {
        _ammoTimer -= Time.deltaTime;
        if (_ammoTimer < 0)
        {
            ammoEmpty = true;
        }
    }
    public override void Shoot()
    {
        ammoEmpty = false;
        canShoot = false;
        particleEffect.SetActive(true);
        base.Shoot();
        _refManager.soundManagerReference.PlaySound(K.SOUND_MACHINE_GUN);
       // direction = shootPoint.TransformDirection(Vector3.forward);
        Instantiate(bulletPref, shootPoint.position + shootPoint.forward, shootPoint.rotation);

        ammoInput();

        /*
        Debug.DrawRay(shootPoint.position, direction * maxDistance, Color.blue);

        if (Physics.Raycast(shootPoint.position, direction, out hit,maxDistance))
        {
            if(hit.rigidbody!=null)
                hit.rigidbody.AddForceAtPosition(direction * forceImpact, hit.point);
        }*/
    }
}
