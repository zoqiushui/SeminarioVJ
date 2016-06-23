using UnityEngine;
using System.Collections;

public class Pilot : MonoBehaviour {

    public int Country;
    public string PilotName;
    public int ColorSkin;
    public int Face;
    public int Hair;
    public int ColorHair;
    public int Accesory;
    public int FaceHair;


    //Traits y bonus
    public int BonusMaxSpeed = 0;
    public int BonusTurbo = 0;
    public int BonusAcceleration = 0;
    public int BonusAmmo = 0;
    public int BonusAmmoReload = 0;
    public int BonusLessMineDamage = 0;




    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {

        UpdateStats();

    }

    public void UpdateStats ()
    {
        Country=PlayerPrefs.GetInt("Country");
        PilotName=PlayerPrefs.GetString("PilotName");
        ColorSkin=PlayerPrefs.GetInt("ColorSkin");
        Face=PlayerPrefs.GetInt("Face");
        Hair=PlayerPrefs.GetInt("Hair");
        ColorHair=PlayerPrefs.GetInt("ColorHair");
        Accesory=PlayerPrefs.GetInt("Accesory");
        FaceHair=PlayerPrefs.GetInt("FaceHair");

        LoadTraits();
    }

    public void LoadTraits()
    {
        BonusMaxSpeed = PlayerPrefs.GetInt("BonusMaxSpeed");
        BonusTurbo = PlayerPrefs.GetInt("BonusTurbo");
        BonusAcceleration = PlayerPrefs.GetInt("BonusAcceleration");
        BonusAmmo = PlayerPrefs.GetInt("BonusAmmo");
        BonusAmmoReload = PlayerPrefs.GetInt("BonusAmmoReload");
        BonusLessMineDamage = PlayerPrefs.GetInt("BonusLessMineDamage");

    }
	
	void Update ()
    {
	
	}
}
