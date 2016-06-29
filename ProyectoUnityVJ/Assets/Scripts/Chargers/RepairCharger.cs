using UnityEngine;
using System.Collections;

public class RepairCharger : MonoBehaviour {

    void OnTriggerEnter(Collider c)
    {

        //Debug.Log("Repair");
        if (c.GetComponentInParent<BuggyData>() != null)
        {
            //Debug.Log("Repair");
            print(c.GetComponentInParent<BuggyData>().currentLife);
            //print(c.GetComponent<BuggyData>().currentLife);
            c.GetComponentInParent<BuggyData>().currentLife = c.GetComponentInParent<BuggyData>().currentLife + c.GetComponentInParent<BuggyData>().maxLife / 4;
            c.GetComponentInParent<BuggyData>().currentLife = Mathf.Clamp(c.GetComponentInParent<BuggyData>().currentLife,0, c.GetComponentInParent<BuggyData>().maxLife);
            /*if (c.GetComponentInParent<BuggyData>().currentLife + c.GetComponentInParent<BuggyData>().maxLife / 4 > c.GetComponentInParent<BuggyData>().maxLife)
            {
                c.GetComponentInParent<BuggyData>().currentLife = c.GetComponentInParent<BuggyData>().maxLife;
            }
            else
            {
                c.GetComponentInParent<BuggyData>().currentLife = c.GetComponentInParent<BuggyData>().currentLife + c.GetComponentInParent<BuggyData>().maxLife / 4;
            }*/
            print(c.GetComponentInParent<BuggyData>().currentLife);
            c.GetComponentInParent<BuggyData>().CheckHealthBar(true);
        }
    }
    
}
