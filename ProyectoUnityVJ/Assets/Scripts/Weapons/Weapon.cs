using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public float cooldown;
    public short shootButtom;
    protected bool canShoot;
    private float _timeCoolDown;
    public bool isCrosshair;
    public Sprite crosshair;

    public Image visualAmmo;
    protected float maxAmmo;
    protected float currentAmmo;
    protected bool ammoEmpty;
    public float missileCountAmmo;
    public float reloadSpeed;
    public float ammoTimer;
    protected float _ammoTimer;
    protected SoundManager _soundManagerReference;

    protected bool _isShooting;

    protected virtual void Start()
    {
        _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
    }

    public virtual void ShootDownButtom()
    {
        _timeCoolDown += Time.deltaTime;
        

        if (Input.GetMouseButton(shootButtom) && _timeCoolDown > cooldown && !canShoot && !ammoEmpty)
        {
            _timeCoolDown = 0;
            canShoot = true;
        }
    }
    protected void Update()
    {
        if (Input.GetMouseButton(shootButtom)) _isShooting = true;
        else _isShooting = false;
    }

    public virtual void OneShoot()
    {
        _timeCoolDown += Time.deltaTime;

        if (Input.GetMouseButtonUp(shootButtom) && _timeCoolDown > cooldown && !canShoot && !ammoEmpty)
        {
            _timeCoolDown = 0;
            canShoot = true;
        }
    }

    public virtual void Shoot()
    {
        canShoot = false;
      //  print("Disparo");
    }

}
