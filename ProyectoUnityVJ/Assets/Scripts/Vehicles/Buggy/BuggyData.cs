using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class BuggyData : VehicleData
{

    public Image visualHealth;
    public GameObject DamagePortrait;
    public GameObject glassDamage;
    private List<RectTransform> _crackedGlass;
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        currentLife = PlayerPrefs.GetInt("CurrentLife") > 0 ? PlayerPrefs.GetInt("CurrentLife") : maxLife;

        _crackedGlass = glassDamage.GetComponentsInChildren<RectTransform>().ToList();
        _crackedGlass.RemoveAt(0);

        CheckHealthBar(false);
    }
	
	// Update is called once per frame
	protected override void Update () {
	    
	}

    public override void Damage(float damageTaken)
    {
        base.Damage(damageTaken);
        if(!_alive)
        {
            K.pilotIsAlive = false;
        }
           
            
    }

    public override void CheckHealthBar(bool hasCured)
    {
        float calc_health = currentLife / maxLife;
        visualHealth.fillAmount = calc_health;

        if(currentLife != maxLife)
        {
            var index = Random.Range(0, _crackedGlass.Count - 1);
            _crackedGlass[index].GetComponent<RawImage>().enabled = true;
        }

        if (hasCured)
        {
            foreach (var glass in _crackedGlass) glass.GetComponent<RawImage>().enabled = false;
        }

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
