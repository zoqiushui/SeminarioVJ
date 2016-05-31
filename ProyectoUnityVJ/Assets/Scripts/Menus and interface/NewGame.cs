using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {



	void Awake ()
    {
        if (K.names.Count==0)
        {
            K.names.Add("Noah");
            K.names.Add("Alejandro");
            K.names.Add("Ezequiel");
            K.names.Add("Micheal");
            K.names.Add("Jackson");
            K.names.Add("Jacob");
            K.names.Add("Cristian");
            K.names.Add("Mauricio");
            K.names.Add("Martin");
            K.names.Add("Marcos");
            K.names.Add("Joseph");
            K.names.Add("Walter");
            K.names.Add("Pablo");
            K.names.Add("Rex");
            K.names.Add("David");
            K.names.Add("Oliver");
            K.names.Add("Gabriel");
            K.names.Add("Samuel");
            K.names.Add("John");
            K.names.Add("Luck");
            K.names.Add("Henry");
            K.names.Add("Isaac");
            K.names.Add("Owen");
            K.names.Add("Nathan");
            K.names.Add("Caleb");
            K.names.Add("Jack");
            K.names.Add("Jason");
            K.names.Add("Noah");
            K.names.Add("Julian");
            K.names.Add("Bruce");
            K.names.Add("Alfred");
        }
	
	}

    public void StartCampaign()
    {
        
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Resources", 0);
        PlayerPrefs.SetInt("CurrentLife", 100);
        PlayerPrefs.SetInt("MaxLife", 100);
    }


}
