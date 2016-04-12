using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{

    public float detonTime;
    protected bool touched;
    protected bool deton;

    // Use this for initialization
    public virtual void Update()
    {
        if (touched)
        {

            detonTime -= Time.deltaTime;
            if (detonTime <= 0)
                Detonator();
        }
    }

    // Update is called once per frame
   

    public virtual void Detonator()
    {
        touched = false;
    }
}
