using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {

    public Text resourcesText;
    public Text pilotName;

    public void Awake()
    {
        resourcesText.text= "Resources: " + PlayerPrefs.GetInt("Resources");
        pilotName.text = "Pilot's name: " + PlayerPrefs.GetString("PilotName");
    }

	public void SearchForRace()
    {
        SceneManager.LoadScene(3);
    }
}
