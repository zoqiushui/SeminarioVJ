using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RockedLauncherMK2 : Weapon
{
    public float CooldownTime;
    public short InputKey;
    public float angleView;
    public float maxDistance;
    public Transform launchPoint;
    public GameObject rocket;
    private Vector3 _pointAttack;
    public Camera _mainCam;
    
    public GameObject lockOn;
    private RawImage _lockOn;
    private Ray ray;
    private RaycastHit hit;

    void Start ()
    {
        isCrosshair = true;
       // lockOn.SetActive(false);
       // _lockOn = lockOn.GetComponent<RawImage>();
        shootButtom = InputKey;
        cooldown = CooldownTime;
        currentAmmo = maxAmmo = 100;
    }

    void Update()
    {
        OneShoot();
        CheckAmmoBar();
     



        if (canShoot)
        {
            ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                _pointAttack = hit.point;
                if (visualAmmo.fillAmount > 0 && !ammoEmpty) Shoot();

                print("ROCKET TARGET: " + hit.collider.gameObject.name);
            }
        }

    }
    public override void Shoot()
    {
        base.Shoot();
        GameObject rock = (GameObject)GameObject.Instantiate(rocket, launchPoint.position, Quaternion.identity);
        rock.GetComponent<Rocket>().SetTarget(_pointAttack);
     //   lockOn.SetActive(false);
        currentAmmo -= maxAmmo / missileCountAmmo;
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = currentAmmo / maxAmmo;
        visualAmmo.fillAmount = calc_ammo;
        ReloadAmmo();

        if (visualAmmo.fillAmount == 0) ammoEmpty = true;
    }

    private void ReloadAmmo()
    {
        if (currentAmmo < maxAmmo && ammoEmpty) currentAmmo += Time.deltaTime * reloadSpeed;

        if (visualAmmo.fillAmount == 1)
        {
            ammoEmpty = false;
            currentAmmo = maxAmmo;
        }
    }
}
