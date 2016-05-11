using System.Collections;
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

    public float CooldownTime;
    public short InputKey;
    public float angleView;
    public float maxDistance;
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
    void Start ()
    {
        isCrosshair = false;
        lockOn.SetActive(false);
        _lockOn = lockOn.GetComponent<Image>();
        targets = new List<GameObject>();
        shootButtom = InputKey;
        cooldown = CooldownTime;
        currentAmmo = maxAmmo = 100;
    }

    void Update()
    {
        CheckAmmoBar();
        ShootDownButtom();

        if (canShoot) LockEnemy();
        if (_enemyFound && targets.Count > 1)
        {
            SearchClose(targets);
        }
        else if (_enemyFound)
            _finalTarget = targets[0];

        if (Input.GetMouseButtonUp(InputKey) && _finalTarget != null && canShoot)
        {
            if (visualAmmo.fillAmount > 0 && !ammoEmpty) Shoot();
        }

        if (_finalTarget != null && _enemyFound)    LockTarget();

        if (!Input.GetMouseButton(InputKey) && lockOn.activeSelf) lockOn.SetActive(false);


    }

    private void LockTarget()
    {
        lockOn.SetActive(true);
        Vector3 temp = _mainCam.WorldToScreenPoint(_finalTarget.transform.position);
        if (temp.z < 10)
        {
            _finalTarget = null;
            if (visualAmmo.fillAmount > 0 && !ammoEmpty) Shoot();
        }
        else
        {
            temp.z = 0;
            _lockOn.rectTransform.position = temp;
        }
    }

    private void LockEnemy()
    {

        //float distance = Mathf.Infinity;
        foreach (var posibilities in GameObject.FindGameObjectsWithTag("Target"))
        {
			Debug.Log (posibilities.gameObject);
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
                        targets.Add(posibilities);
                        print(posibilities);
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
            }
        }
    }

    public override void Shoot()
    {
        base.Shoot();

        currentAmmo -= maxAmmo / missileCountAmmo;

        _enemyFound = false;
        if (_finalTarget != null)
        {
            GameObject rock = (GameObject)GameObject.Instantiate(rocket, launchPoint.position, Quaternion.identity);
            rock.GetComponent<Rocket>().SetTarget(_finalTarget);
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
