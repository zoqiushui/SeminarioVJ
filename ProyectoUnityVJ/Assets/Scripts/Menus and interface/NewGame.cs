using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {



	void Awake ()
    {
       
	
	}

    public void StartCampaign()
    {
        
        SceneManager.LoadScene((int)SCENES_NUMBER.PilotCreation);
        PlayerPrefs.SetInt("Resources", 0);
        PlayerPrefs.SetInt("CurrentLife", 100);
        PlayerPrefs.SetInt("MaxLife", 100);

        PlayerPrefs.SetString("PilotName", "");
        PlayerPrefs.SetInt("Face", 0);
        PlayerPrefs.SetInt("Hair", 0);
        PlayerPrefs.SetInt("FaceHair", 0);
        PlayerPrefs.SetInt("Accesory", 0);
        PlayerPrefs.SetInt("HairColor", 0);
        PlayerPrefs.SetInt("SkinColor", 0);

        //Delete traits
        PlayerPrefs.SetInt("BonusMaxSpeed", 0);
        PlayerPrefs.SetInt("BonusTurbo", 0);
        PlayerPrefs.SetInt("BonusAcceleration", 0);
        PlayerPrefs.SetInt("BonusAmmo", 0);
        PlayerPrefs.SetInt("BonusAmmoReload", 0);
        PlayerPrefs.SetInt("BonusLessMineDamage", 0);

    }


}
