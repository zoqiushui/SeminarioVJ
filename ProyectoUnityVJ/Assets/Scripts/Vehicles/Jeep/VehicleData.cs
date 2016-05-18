using UnityEngine;
using System.Collections;

public class VehicleData : MonoBehaviour
{
    public float maxLife;
    private float _currentLife;

	void Start ()
    {
        _currentLife = maxLife;
	}

    public void Damage(float damageTaken)
    {
        _currentLife -= damageTaken;
        if (_currentLife <= 0)
            print("Car Destroy"); 
    }
}
