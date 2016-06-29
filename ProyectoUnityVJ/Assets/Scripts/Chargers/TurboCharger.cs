using UnityEngine;
using System.Collections;

public class TurboCharger : MonoBehaviour {


    void OnTriggerEnter(Collider c)
    {

        //Debug.Log("Repair");
        if (c.GetComponentInParent<BuggyController>() != null)
        {
            Debug.Log("TURBO!");
            //print(c.GetComponent<BuggyData>().currentLife);
            c.GetComponentInParent<BuggyController>()._canRechargeNitro = true;
            c.GetComponentInParent<BuggyController>().RechargeNitro();

        }
    }
}
