using UnityEngine;
using System.Collections;

public class MolotovLauncher : Weapon
{
    public float angleView;
    public GameObject bomb;
    public Transform launchPoint;
    public Camera mainCamera;

    // Use this for initialization
    void Start ()
    {
        isCrosshair = true;
        currentAmmo = maxAmmo = 100;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckAmmoBar();
        OneShoot();
        if (canShoot && !ammoEmpty) Shoot();

    }

    /// <summary>
    /// 
    /// </summary>
    public override void Shoot()
    {
        base.Shoot();
        SoundManager.instance.PlaySound(K.SOUND_MOLOTOV_LAUNCH);
        GameObject granade = (GameObject)GameObject.Instantiate(bomb, launchPoint.position + launchPoint.forward * 2, Quaternion.identity);
        granade.transform.forward = launchPoint.forward;
        granade.GetComponent<Rigidbody>().AddForce(transform.forward * 500, ForceMode.Impulse);
        currentAmmo -= maxAmmo / missileCountAmmo;
        canShoot = false;
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
