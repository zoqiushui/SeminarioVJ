using UnityEngine;
using System.Collections;

public class MiniGun : Weapon
{
    public short type;
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

        if (GameManager.disableShoot == false && type == 0)
        {
            CheckAmmoBar();
            ShootDownButtom();

            if (canShoot && visualAmmo.fillAmount > 0 && _isShooting) Shoot();
            else
                particleEffect.SetActive(false);
            

        }
        else
        if (canShoot && type == 1)
            Shoot();
        else
            particleEffect.SetActive(false);

        base.Update();

        if (!_isShooting) ReloadAmmo();
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = _ammoTimer / ammoTimer;
        visualAmmo.fillAmount = calc_ammo;

    //    if (visualAmmo.fillAmount == 0) ammoEmpty = true;
    }

    private void ReloadAmmo()
    {
        if (visualAmmo == null) return;
        if (visualAmmo.fillAmount == 1)
        {
            ammoEmpty = false;
            _ammoTimer = ammoTimer;
        }
        else _ammoTimer += Time.deltaTime * reloadSpeed;
    }
    private void ammoInput()
    {
        _ammoTimer -= Time.deltaTime;
      /*  if (_ammoTimer < 0)
        {
            ammoEmpty = true;
        }*/
    }

    public override void Shoot()
    {
        ammoEmpty = false;
        canShoot = false;
        particleEffect.SetActive(true);
        base.Shoot();
       // direction = shootPoint.TransformDirection(Vector3.forward);
        Instantiate(bulletPref, shootPoint.position + shootPoint.forward, shootPoint.rotation);
        if (type == 0)
        {
            //_soundManagerReference.PlaySound(K.SOUND_MACHINE_GUN);
            ammoInput();
        }

        /*
        Debug.DrawRay(shootPoint.position, direction * maxDistance, Color.blue);

        if (Physics.Raycast(shootPoint.position, direction, out hit,maxDistance))
        {
            if(hit.rigidbody!=null)
                hit.rigidbody.AddForceAtPosition(direction * forceImpact, hit.point);
        }*/
    }
}
