using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour
{
    public bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;

    private Vector3 OriginalPos;
    private Quaternion OriginalRot;

    void Start()
    {
        Shaking = false;
    }

    void Update()
    {
        if (ShakeIntensity > 0)
        {
            //transform.localPosition = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
            transform.localRotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);

            ShakeIntensity -= ShakeDecay;
        }
        else if (Shaking)
        {
            Shaking = false;
        }
    }

    public void DoShake()
    {
        //OriginalPos = Vector3.zero;//transform.position;
        OriginalRot = Quaternion.Euler(0, 0, 0);

        ShakeIntensity = 0.15f;
        ShakeDecay = 0.02f;
        Shaking = true;
    }
}