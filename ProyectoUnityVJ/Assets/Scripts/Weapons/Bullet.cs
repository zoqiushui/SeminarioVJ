using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 75;
    public float powerDamage = 5;
    public float destroyTime;
    private float _lifeTime;
    public GameObject spark;
    private Collider col;

    // Use this for initialization
    void Start()
    {
        col = this.GetComponent<Collider>();
        col.isTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > 0.020f && col.isTrigger) col.isTrigger = false;
        if (destroyTime <= _lifeTime) DestroyThis();

        transform.position += transform.forward * speed * Time.deltaTime;

    }

    void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.layer);
        if (col.gameObject.layer == K.LAYER_IA)
        {
            col.gameObject.GetComponent<IAController>().Damage(powerDamage);
            Vector3 cont = col.contacts[0].point;
            Instantiate(spark, cont + -transform.forward, Quaternion.identity);
            DestroyThis();
        }
        else if (col.gameObject.layer == K.LAYER_PLAYER)
        {
            col.gameObject.GetComponent<BuggyData>().Damage(powerDamage);
            Vector3 cont = col.contacts[0].point;
            Instantiate(spark, cont + -transform.forward, Quaternion.identity);
            DestroyThis();
        }


        else
        {
            if (col.contacts.Length != 0)
                Instantiate(spark, col.contacts[0].point, Quaternion.identity);
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
