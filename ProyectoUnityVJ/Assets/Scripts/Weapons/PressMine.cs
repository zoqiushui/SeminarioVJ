﻿using System.Collections;
using UnityEngine;

public class PressMine : Trap
{
    public float expPower;
    public float expRadius;
    public LayerMask layersDamege;

    

    public override void Update()
    {
        base.Update();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != K.LAYER_GROUND)
        {
            if (col.gameObject != null && !touched)
                touched = true;
        }
    }

    public override void Detonator()
    {
        print("detonate");

        base.Detonator();
        Explosion();


    }

    public void Explosion()
    {
        print("explote");
        var cols = Physics.OverlapSphere(transform.position, expRadius, layersDamege);
        for (int i = 0; i < cols.Length; i++)
        {
            Vector3 direction = cols[i].transform.position - transform.position;
            float dist = direction.magnitude;
            direction.Normalize();
            print(cols[i].gameObject);
            if (cols[i].GetComponent<Rigidbody>() != null)
            {
                print("impact");
                print(cols[i].gameObject);
                cols[i].GetComponent<Rigidbody>().AddForce(direction * expPower * (1 - (dist / expRadius)),ForceMode.Impulse);
            }// colis[i].rigidbody.AddForce(direction * expPower * (1 - (dist / expRadius)));
        }
        GameObject.Destroy(this.gameObject);
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.up, expRadius);
    }

}
