using UnityEngine;
using System.Collections;

public class SpinningCar : MonoBehaviour {

	

	void Update ()
    {
        transform.Rotate(0, 45 *  Time.deltaTime, 0);
	}
}
