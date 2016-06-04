using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {

    public Text resourcesText;
    public Text pilotName;
    public Text repairAmount;
    private int resourcesCurrent;

    public void Awake()
    {
        resourcesCurrent = PlayerPrefs.GetInt("Resources");
        resourcesText.text= "Resources: " + PlayerPrefs.GetInt("Resources");
        pilotName.text = "Pilot's name: " + PlayerPrefs.GetString("PilotName");

        repairAmount.text = "Vehicle's state: "+PlayerPrefs.GetInt("CurrentLife")+"/"+PlayerPrefs.GetInt("MaxLife");
    }

    public void Repair()
    {
        if (PlayerPrefs.GetInt("CurrentLife") == PlayerPrefs.GetInt("MaxLife"))
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
        SceneManager.LoadScene(3);
    }
}
