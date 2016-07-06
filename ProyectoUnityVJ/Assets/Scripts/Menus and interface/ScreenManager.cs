using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {

    public Text resourcesText;
    public Text pilotName;
    public Text repairAmount;
    private int resourcesCurrent;

    public GameObject portrait;
    public GameObject newPilotButton;
    public GameObject youNeedPilot;

    public void Awake()
    {
        resourcesCurrent = PlayerPrefs.GetInt("Resources");
        resourcesText.text= "Resources: " + PlayerPrefs.GetInt("Resources");
        if (K.pilotIsAlive==true)
        {
            pilotName.text = PlayerPrefs.GetString("PilotName");
        }
        else
        {
            pilotName.text = "";
        }
        

        repairAmount.text = "Vehicle's state: "+PlayerPrefs.GetInt("CurrentLife")+"/"+PlayerPrefs.GetInt("MaxLife");


        if (K.pilotIsAlive==false)
        {
            portrait.SetActive(false);
            newPilotButton.SetActive(true);
        }

    }

    public void Repair()
    {
        if (PlayerPrefs.GetInt("CurrentLife") != PlayerPrefs.GetInt("MaxLife") && PlayerPrefs.GetInt("Resources")>=10)
        {
            resourcesCurrent -= 10;
            PlayerPrefs.SetInt("Resources", resourcesCurrent);
            resourcesText.text = "Resources: " + resourcesCurrent;

            PlayerPrefs.SetInt("CurrentLife", PlayerPrefs.GetInt("MaxLife"));
            repairAmount.text = "Vehicle's state: " + PlayerPrefs.GetInt("CurrentLife") + "/" + PlayerPrefs.GetInt("MaxLife");
        }
    }

	public void SearchForRace()
    {
        if (K.pilotIsAlive==true)
        {
            SceneManager.LoadScene((int)SCENES_NUMBER.LoadingScene);
        }
        else
        {
            youNeedPilot.SetActive(true);
        }
        
    }

    public void NewPilot()
    {
        SceneManager.LoadScene((int)SCENES_NUMBER.PilotCreation);
    }
}
