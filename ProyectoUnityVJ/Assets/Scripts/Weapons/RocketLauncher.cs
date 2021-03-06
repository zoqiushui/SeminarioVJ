﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// La informacion del lock on target.
/// </summary>
/*[System.Serializable]
public class LifeGuiData
{ 
    private Rect position;
    public Texture2D lockTexture;
}*/

public class RocketLauncher : Weapon
{
    public float angleView;
    public float maxDistance;
    public float damage;
    public Transform launchPoint;
    public Transform myTransf;
    public GameObject rocket;
    private GameObject _finalTarget;
    private List<GameObject> targets;
    private bool _enemyFound;
    public Camera _mainCam;

    public GameObject lockOn;
    private Image _lockOn;
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        isCrosshair = false;
        lockOn.SetActive(false);
        _lockOn = lockOn.GetComponent<Image>();
        targets = new List<GameObject>();
        currentAmmo = maxAmmo = visualAmmo.fillAmount;
    }

    protected override void Update()
    {
       // if (GameManager.disableShoot == false)
        //{
            base.Update();
            CheckAmmoBar();
            ShootDownButtom();

            if (canShoot) LockEnemy();
            if (_enemyFound)
            {
                SearchClose(targets);
            }


          //  if (_finalTarget != null && _enemyFound) LockTarget();

            if (Input.GetMouseButtonUp(shootButtom) && _finalTarget != null && canShoot)
            {
                print("shootear");
                if (visualAmmo.fillAmount > 0 && !ammoEmpty && currentAmmo >= maxAmmo / missileCountAmmo) Shoot();
            }


            if (!Input.GetMouseButton(shootButtom) && lockOn.activeSelf) lockOn.SetActive(false);
        // }


    }

    private void LockTarget()
    {
        print("posicionando mira");
        lockOn.SetActive(true);
        Vector3 temp = _mainCam.WorldToScreenPoint(_finalTarget.transform.position);
        if (temp.z < 10)
        {
            _finalTarget = null;
//            if (visualAmmo.fillAmount > 0 && !ammoEmpty && currentAmmo >= maxAmmo / missileCountAmmo) Shoot();
        }
        else
        {
            _lockOn.rectTransform.position = temp;
        }
        //_lockOn.rectTransform.position = temp;
    }

    private void LockEnemy()
    {
        //float distance = Mathf.Infinity;
        foreach (var posibilities in GameObject.FindGameObjectsWithTag("Target"))
        {
          //float dist = (posibilities.transform.position - transform.position).sqrMagnitude;
            Vector3 direction = posibilities.transform.position - myTransf.position;
            float angle = Vector3.Angle(myTransf.forward, direction);
            

            if (angle < angleView * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction.normalized, out hit, maxDistance))
                {
                    Vector3 temp = _mainCam.WorldToScreenPoint(hit.transform.position);
                    if (hit.collider.gameObject.tag == "Target" && temp.z > 8)
                    {
                        targets.Add(posibilities.transform.parent.transform.parent.gameObject);
                        print("posible objetivo: " + posibilities.transform.parent.transform.parent.gameObject);
                        if(!_enemyFound) _enemyFound = true;
                    }
                }
            }
        }
    }
    

    private void SearchClose(List<GameObject> t)
    {
        float distance = Mathf.Infinity;
        foreach (var targ in t)
        {
            Vector3 post = _mainCam.WorldToScreenPoint(targ.transform.position);
            post.z = 0;

            float dist = (post - Input.mousePosition).sqrMagnitude;

            if (dist < distance)
            {
                distance = dist;
                _finalTarget = targ;
                print(_finalTarget);
            }
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        print("disparo");
        currentAmmo -= maxAmmo / missileCountAmmo;

        _enemyFound = false;
        if (_finalTarget != null)
        {
            _soundManagerReference.PlaySound(K.SOUND_MISIL_LAUNCH);
            GameObject rock = (GameObject)GameObject.Instantiate(rocket, launchPoint.position, Quaternion.identity);
            rock.GetComponent<Rocket>().SetTarget(_finalTarget,damage);
        }

        _finalTarget = null;
        targets.Clear();
        lockOn.SetActive(false);

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
