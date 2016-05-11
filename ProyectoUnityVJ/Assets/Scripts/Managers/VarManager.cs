using UnityEngine;
using System.Collections;

public class VarManager : MonoBehaviour {


    public int country;
    public string pilotName;

	// Use this for initialization
	void Awake ()
    {

        DontDestroyOnLoad(transform.gameObject);

    }

    public void UpdateVars(int flag, string name)
    {

        country = flag;
        pilotName = name;

    }

}
