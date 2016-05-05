using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{

    protected float cooldown;
    protected short shootButtom;
    protected bool canShoot;
    private float _timeCoolDown;
    public bool isCrosshair;
    public Sprite crosshair;
    
    public virtual void ShootDownButtom()
    {
        _timeCoolDown += Time.deltaTime;
        

        if (Input.GetMouseButton(shootButtom) && _timeCoolDown > cooldown && !canShoot)
        {
            _timeCoolDown = 0;
            canShoot = true;
        }
    }

    public virtual void OneShoot()
    {
        _timeCoolDown += Time.deltaTime;

        if (Input.GetMouseButtonUp(shootButtom) && _timeCoolDown > cooldown && !canShoot)
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
