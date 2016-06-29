using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour {

    public List<GameObject> weaponsSet1;
    public List<GameObject> weaponsSet2;

    public GameObject crosshair;
    private RawImage _lockOn;
    private Ray ray;
    private RaycastHit hit;
    public Camera _mainCam;
    public float distance;
    private bool _activeCrosshair;
    public short activeSet;
    private Vector3 _frezze;


    // Use this for initialization
    void Start ()
    {
        crosshair.SetActive(true);
        _lockOn = crosshair.GetComponent<RawImage>();

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
            Desactivate(weaponsSet2);
            Activate(weaponsSet1);

        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) && activeSet != 1)
        {
            activeSet = 1;
            Desactivate(weaponsSet1);
            Activate(weaponsSet2);
        }
        /*
        if (activeSet == 0)
        {
            _frezze = _mainCam.WorldToScreenPoint(weaponsSet1[0].transform.position + -weaponsSet1[0].transform.forward * distance);
            _lockOn.rectTransform.position = _frezze;
            AimCollision();

        }
        else if (activeSet == 1)
        {
            _lockOn.transform.position = Input.mousePosition;
            AimCollision();
        }*/
        _lockOn.transform.position = Input.mousePosition;
        AimCollision();
    }

    private void AimCollision()
    {
        ray = _mainCam.ScreenPointToRay(_lockOn.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 << K.LAYER_IA))
        {
            if (hit.collider.gameObject.layer == K.LAYER_IA) _lockOn.gameObject.GetComponent<CanvasRenderer>().SetColor(Color.red);
            else _lockOn.gameObject.GetComponent<CanvasRenderer>().SetColor(Color.white);
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
