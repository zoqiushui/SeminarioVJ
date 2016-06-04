using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VehicleData : MonoBehaviour
{
    public float maxLife = 100f;
    public float currentLife;
    public Image visualHealth;
    public GameObject DamagePortrait;

	void Start ()
    {
        maxLife = 100;
        //currentLife = maxLife;

        currentLife = PlayerPrefs.GetInt("CurrentLife") > 0 ? PlayerPrefs.GetInt("CurrentLife") : maxLife;
    }

    void Update()
    {
        //CheckHealthBar();
    }
    public void Damage(float damageTaken)
    {
        currentLife -= damageTaken;
        CheckHealthBar();
        if (currentLife <= 0)
            print("Car Destroy"); 
    }
    private void CheckHealthBar()
    {
        float calc_health = currentLife / maxLife;
        visualHealth.fillAmount = calc_health;

        if (currentLife >= 80)
        {
            visualHealth.color = Color.green;
            
        }
        else if (currentLife >= 50)
        {
            visualHealth.color = Color.yellow;
            DamagePortrait.GetComponent<SpriteRenderer>().sprite = K.spritesDamage[0];
        }
        else if (currentLife >= 30)
        {
            visualHealth.color = Color.red;
            DamagePortrait.GetComponent<SpriteRenderer>().sprite = K.spritesDamage[1];
        }

        else if (currentLife >= 0)
        {
            visualHealth.color = Color.red;
            DamagePortrait.GetComponent<SpriteRenderer>().sprite = K.spritesDamage[2];
        }
    }

}
