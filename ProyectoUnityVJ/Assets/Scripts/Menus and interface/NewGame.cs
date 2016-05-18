using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {



	void Awake ()
    {

	
	}

    public void StartCampaign()
    {
        
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Resources", 0);
    }

}
