using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    protected float cooldown;
    protected short shootButtom;
    protected bool canShoot;
    private float _timeCoolDown;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public virtual void ShootDownButtom()
    {
        _timeCoolDown += Time.deltaTime;
        

        if (Input.GetMouseButton(shootButtom) && _timeCoolDown > cooldown)
        {
            _timeCoolDown = 0;
            canShoot = true;
        }
    }

    public virtual void OneShoot()
    {
        _timeCoolDown += Time.deltaTime;

        if (Input.GetMouseButtonUp(shootButtom) && _timeCoolDown > cooldown)
        {
            _timeCoolDown = 0;
            canShoot = true;
        }
    }

    public virtual void Shoot()
    {
      //  print("Disparo");
    }

}
