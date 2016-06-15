using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuggyData : VehicleData
{

    public Image visualHealth;
    public GameObject DamagePortrait;


    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        currentLife = PlayerPrefs.GetInt("CurrentLife") > 0 ? PlayerPrefs.GetInt("CurrentLife") : maxLife;
    }
	
	// Update is called once per frame
	protected override void Update () {
	
	}

    public override void Damage(float damageTaken)
    {
        base.Damage(damageTaken);
    }

    protected override void CheckHealthBar()
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
