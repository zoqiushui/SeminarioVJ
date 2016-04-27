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
    private Image _lockOn;
    private Ray ray;
    private RaycastHit hit;


    // Use this for initialization
    void Start ()
    {
        lockOn.SetActive(false);
        _lockOn = lockOn.GetComponent<Image>();
        shootButtom = InputKey;
        cooldown = CooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        OneShoot();

        if(Input.GetMouseButtonDown(shootButtom) && canShoot)
        {
            lockOn.SetActive(true);
            Vector3 temp = _mainCam.WorldToScreenPoint(Input.mousePosition);
            _lockOn.rectTransform.position = temp;

        }

        if (canShoot)
        {
            ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                _pointAttack = hit.point;
                Shoot();
            }
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        GameObject rock = (GameObject)GameObject.Instantiate(rocket, launchPoint.position, Quaternion.identity);
        rock.GetComponent<Rocket>().SetTarget(_pointAttack);
        lockOn.SetActive(false);

    }
}
