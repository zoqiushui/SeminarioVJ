﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{

    public float CooldownTime;
    public short InputKey;
    public float angleView;
    public float maxDistance;
    public Transform launchPoint;
    public Transform myTransf;
    public GameObject rocket;
    private GameObject _finalTarget;
    public List<GameObject> targets;
    private bool _enemyFound;
    public Camera _mainCam;

    // Use this for initialization
    void Start ()
    {
        targets = new List<GameObject>();
        shootButtom = InputKey;
        cooldown = CooldownTime;
    }

    void Update()
    {
        ShootDownButtom();
        if (canShoot) LockEnemy();
        if (_enemyFound && targets.Count > 1)
        {
            SearchClose(targets);
        }
        else if (_enemyFound)
            _finalTarget = targets[0];

        if (Input.GetMouseButtonUp(InputKey) && _enemyFound && canShoot)
        {
            print("Disparo");
            Launch();
        }


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
                    if (hit.collider.gameObject.tag == "Target")
                    {
                        targets.Add(posibilities);
                        if(!_enemyFound) _enemyFound = true;
                        print("Encontro");
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
            float dist = (_mainCam.WorldToScreenPoint(targ.transform.position) - Input.mousePosition).sqrMagnitude;

            if (dist < distance)
            {
                distance = dist;
                _finalTarget = targ;
                print(_finalTarget);
            }
        }
    }

    public void Launch()
    {
        print("Launch");
        _enemyFound = false;
        canShoot = false;
        if (_finalTarget != null)
        {
            GameObject rock = (GameObject)GameObject.Instantiate(rocket, launchPoint.position, Quaternion.identity);
            rock.GetComponent<Rocket>().SetTarget(_finalTarget);
        }
        _finalTarget = null;
        targets.Clear();

    }
}