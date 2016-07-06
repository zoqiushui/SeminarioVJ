using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

    public GameObject continueButton;
    private bool _noContinue;

	void Awake ()
    {
       if(PlayerPrefs.GetInt("SavedData")!=1)
        {
            Color alphaColor=continueButton.GetComponent<Image>().color;
            alphaColor.a = 0.1f;
            continueButton.GetComponent<Image>().color = alphaColor;
            _noContinue = true;
        }
	
	}

    public void StartCampaign()
    {

        //PlayerPrefs.SetInt("SavedData", 1);

        //Blank new
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

        //Load new Scene
        SceneManager.LoadScene((int)SCENES_NUMBER.PilotCreation);

    }

    public void Continue()
    {
        if(_noContinue==false)
        {
            SceneManager.LoadScene((int)SCENES_NUMBER.HUB);
        }

    }

    public void Exit()
    {
        Application.Quit();
        print("Exit");
    }


}
