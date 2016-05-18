using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VehicleData : MonoBehaviour
{
    private float maxLife = 100f;
    public float currentLife;
    public Image visualHealth;
	void Start ()
    {
        maxLife = 100;
        currentLife = maxLife;
	}

    void Update()
    {
        CheckHealthBar();
    }
    public void Damage(float damageTaken)
    {
        currentLife -= damageTaken;
        if (currentLife <= 0)
            print("Car Destroy"); 
    }
    private void CheckHealthBar()
    {
        float calc_health = currentLife / maxLife;
        visualHealth.fillAmount = calc_health;

        if (currentLife >= 70) visualHealth.color = Color.green;
        else if (currentLife >= 40) visualHealth.color = Color.yellow;
        else if (currentLife >= 0) visualHealth.color = Color.red;
    }

}
