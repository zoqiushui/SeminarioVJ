using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VehicleData : MonoBehaviour
{
    public float maxLife = 100f;
    public float currentLife;
    public Image visualHealth;
    public GameObject DamagePortrait;
    public GameObject remainsCar;
    public GameObject explosion;
    private SoundManager _soundManagerReference;
    private bool _alive;
    public ParticleSystem whiteSmoke;
    public ParticleSystem blackSmoke;
    public ParticleSystem fire;

    void Start ()
    {
        maxLife = 100;
        //currentLife = maxLife;
        _alive = true;
        _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
        currentLife = PlayerPrefs.GetInt("CurrentLife") > 0 ? PlayerPrefs.GetInt("CurrentLife") : maxLife;
    }

    void Update()
    {
        //CheckHealthBar();
    }
    public void Damage(float damageTaken)
    {
        if (_alive)
        {
            currentLife -= damageTaken;
            CheckHealthBar();
            if (currentLife <= 0)
            {
                _alive = false;
                Instantiate(explosion, transform.position + transform.up, transform.rotation);
                print("Car Destroy");
                _soundManagerReference.PlaySound(K.SOUND_CAR_DESTROY);
                Instantiate(remainsCar, transform.position + transform.up, transform.rotation);
            }
        }
    }
    private void CheckHealthBar()
    {
        float calc_health = currentLife / maxLife;
        visualHealth.fillAmount = calc_health;

        if (currentLife >= 80)
        {
            visualHealth.color = Color.green;
            whiteSmoke.Stop();
            blackSmoke.Stop();
            fire.Stop();
            
        }
        else if (currentLife >= 50)
        {
            visualHealth.color = Color.yellow;
            DamagePortrait.GetComponent<SpriteRenderer>().sprite = K.spritesDamage[0];
            whiteSmoke.Play();
            blackSmoke.Stop();
            fire.Stop();
        }
        else if (currentLife >= 30)
        {
            visualHealth.color = Color.red;
            DamagePortrait.GetComponent<SpriteRenderer>().sprite = K.spritesDamage[1];
            whiteSmoke.Stop();
            blackSmoke.Play();
            fire.Stop();
        }

        else if (currentLife >= 0)
        {
            visualHealth.color = Color.red;
            DamagePortrait.GetComponent<SpriteRenderer>().sprite = K.spritesDamage[2];
            whiteSmoke.Stop();
            blackSmoke.Play();
            fire.Play();
        }
    }

}
