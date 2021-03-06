﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RockedLauncherMK2 : Weapon
{
    public float angleView;
    public float maxDistance;
    public float damage;
    public Transform launchPoint;
    public GameObject rocket;
    private Vector3 _pointAttack;
    public Camera _mainCam;
    
    public GameObject lockOn;
    private RawImage _lockOn;
    private Ray ray;
    private RaycastHit hit;

    protected override void Start ()
    {
        base.Start();
        isCrosshair = true;
       // lockOn.SetActive(false);
       // _lockOn = lockOn.GetComponent<RawImage>();
        currentAmmo = maxAmmo = visualAmmo.fillAmount;
    }


    //currentAmmo >= maxAmmo / missileCountAmmo: chequea mínimo requerido para lanzar misil.
    protected override void Update()
    {
        if (GameManager.disableShoot == false)
        {
            CheckAmmoBar();
            OneShoot();
            if (canShoot)
            {
                ray = _mainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    _pointAttack = hit.point;
           //         Debug.Log(currentAmmo % 3  + " asdfa " + maxAmmo / missileCountAmmo);
                    if (visualAmmo.fillAmount > 0 && currentAmmo >= maxAmmo / missileCountAmmo) Shoot();

                }
            }
        }
    }
    public override void Shoot()
    {
        base.Shoot();
        _soundManagerReference.PlaySound(K.SOUND_MISIL_LAUNCH);
        GameObject rock = (GameObject)GameObject.Instantiate(rocket, launchPoint.position, launchPoint.rotation);
        rock.GetComponent<Rocket>().SetTarget(_pointAttack,damage); //cambio
     //   lockOn.SetActive(false);
        currentAmmo -= maxAmmo / missileCountAmmo;
    }

    private void CheckAmmoBar()
    {
        visualAmmo.GetComponentInParent<Canvas>().transform.LookAt(Camera.main.transform.position);
        float calc_ammo = currentAmmo / maxAmmo;
        visualAmmo.fillAmount = calc_ammo;
        ReloadAmmo();

    //    if (visualAmmo.fillAmount == 0) ammoEmpty = true;
    }

    private void ReloadAmmo()
    {
        if (currentAmmo < maxAmmo) currentAmmo += Time.deltaTime * reloadSpeed;

        if (visualAmmo.fillAmount == 1)
        {
            ammoEmpty = false;
            currentAmmo = maxAmmo;
        }
    }
}
