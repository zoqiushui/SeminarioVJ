using System.Collections;
using UnityEngine;

public class PressMine : Trap
{
    public float expPower;
    public float expRadius;
    public float expDamage;
    public LayerMask layersDamege;
    public GameObject feedback;
    public GameObject feedLight;
    private float coolLig = 0.5f;
    private float currentCool;

    protected SoundManager _soundManagerReference;

    private void Start()
    {
        _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
    }

    public override void Update()
    {
        base.Update();
        if (currentCool < coolLig)
        {
            feedLight.SetActive(false);
            currentCool += Time.deltaTime;
        }
        else
        {
            feedLight.SetActive(true);
            currentCool = 0;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_PLAYER || col.gameObject.layer == K.LAYER_IA)
        {
            if (col.gameObject != null && !touched)
                touched = true;
        }
    }

    public override void Detonator()
    {
       // print("detonate");

        base.Detonator();
        Explosion();


    }

    public void Explosion()
    {
       // print("explote");
       
        var cols = Physics.OverlapSphere(transform.position, expRadius, layersDamege);
        for (int i = 0; i < cols.Length; i++)
        {
       //     Vector3 direction = cols[i].transform.position - transform.position;
       //   float dist = direction.magnitude;
       //   direction.Normalize();
           // print(cols[i].gameObject);
            if (cols[i].GetComponent<Rigidbody>() != null)
            {
               // print("impact");
                print(cols[i].gameObject);
                cols[i].GetComponent<Rigidbody>().AddExplosionForce(expPower, transform.position,expRadius,0.5f,ForceMode.Impulse);
                if (cols[i].gameObject.layer == K.LAYER_PLAYER)
                    cols[i].gameObject.GetComponent<VehicleData>().Damage(expDamage);

                if (cols[i].gameObject.layer == K.LAYER_IA)
                    cols[i].gameObject.GetComponent<IAVehicle>().Damage(expDamage);


            }
        }
        Instantiate(feedback, transform.position + transform.up, Quaternion.identity);
        _soundManagerReference.PlaySound(K.SOUND_MINE_EXPLOSION);
        GameObject.Destroy(this.gameObject);
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.up, expRadius);
    }

}
