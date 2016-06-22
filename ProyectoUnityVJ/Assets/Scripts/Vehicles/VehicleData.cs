using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VehicleData : MonoBehaviour
{
    public float maxLife = 100f;
    public float currentLife;
    public GameObject remainsCar;
    public GameObject explosion;
    protected SoundManager _soundManagerReference;
    protected bool _alive;
    public ParticleSystem whiteSmoke;
    public ParticleSystem blackSmoke;
    public ParticleSystem fire;

    protected virtual void Start ()
    {
        maxLife = 100;
        //currentLife = maxLife;
        _alive = true;
        _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
        
    }

    protected virtual void Update()
    {
        //CheckHealthBar();
    }
    public virtual void Damage(float damageTaken)
    {
        if (_alive)
        {
            currentLife -= damageTaken;
            CheckHealthBar();
            if (currentLife <= 0)
            {
                GetComponent<Vehicle>().NotifyObserver(K.OBS_MESSAGE_DESTROYED);
                _alive = false;
                Instantiate(explosion, transform.position + transform.up, transform.rotation);
                _soundManagerReference.PlaySound(K.SOUND_CAR_DESTROY);
                Instantiate(remainsCar, transform.position + transform.up, transform.rotation);
            }
        }
    }
    protected virtual void CheckHealthBar()
    {
        
    }

}
