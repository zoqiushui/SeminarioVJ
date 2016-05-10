using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WeaponsManager : MonoBehaviour {

    public List<GameObject> weaponsSet1;
    public List<GameObject> weaponsSet2;

    private bool _activeCrosshair;
    public short activeSet;


    // Use this for initialization
    void Start ()
    {
        if (activeSet == 0)
        {
            Activate(weaponsSet1);
            Desactivate(weaponsSet2);
        }
        else if (activeSet == 1)
        {
            Activate(weaponsSet2);
            Desactivate(weaponsSet1);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && activeSet != 0)
        {
            activeSet = 0;
            Activate(weaponsSet1);
            Desactivate(weaponsSet2);
                
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) && activeSet != 1)
        {
            activeSet = 1;
            Activate(weaponsSet2);
            Desactivate(weaponsSet1);
           // weaponsSet2[1].GetComponent<RocketLauncher>().lockOn.SetActive(true);
        }

    }

    private void Activate(List<GameObject> t)
    {
        for (int i = 0; i < t.Count; i++)
        {

            t[i].SetActive(true);
        }

        Sprite crosshair = t[0].GetComponent<Weapon>().crosshair;

        //TODO:
        //1. Acceder al crosshair de la pantalla (canvas).
        //2. Si el crosshair del arma es null, desactivar crosshair de canvas.
        //3. Si el crosshair del arma no es null, activars crosshair de canvas y setearle el sprite (crosshairCanvas.sprite = crosshair;)
    }

    private void Desactivate(List<GameObject> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            t[i].SetActive(false);
            if (t[i].GetComponent<RocketLauncher>() != null) t[i].GetComponent<RocketLauncher>().lockOn.SetActive(false);
        }
    }

    private void SetCrosshair(bool hasCrosshair)
    {
        if (hasCrosshair!= _activeCrosshair)
        {

            _activeCrosshair = hasCrosshair;
        }
    }

}
