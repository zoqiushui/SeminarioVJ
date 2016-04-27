    using UnityEngine;
using System.Collections;

public class Burner : Weapon
{
    public float CooldownTime;
    public short InputKey;
    public float damage = 0.1f;
    public GameObject flames;

    private bool activeFeed;
    // Use this for initialization
    void Start ()
    {
        cooldown = CooldownTime;
        shootButtom = InputKey;
        flames.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(shootButtom))
        {
            activeFeed = true;
            flames.SetActive(true);
        }
        if (Input.GetMouseButtonUp(shootButtom))
        {
            activeFeed = false;
            flames.SetActive(false);
        }
        ShootDownButtom();

	}
    void OnTriggerStay(Collider cols)
    {
        if (activeFeed)
        {
            if (canShoot && cols.gameObject.layer == K.LAYER_IA)
            {
                print("asdasd");
                Shoot();
                cols.gameObject.GetComponent<IAController>().Damage(damage);
            }
        }
    }
}
