using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PostGameManager : MonoBehaviour {

    public GameObject traitSelected;

    public TraitSelected trait1;
    public TraitSelected trait2;
    public TraitSelected trait3;

    int _random1;
    int _random2;
    int _random3;

    int gray;

    // Use this for initialization
    void Awake ()
    {
        gray = 0;
        GetRandoms();

        trait1.Assign(_random1);
        trait2.Assign(_random2);
        trait3.Assign(_random3);
    }
	
	// Update is called once per frame
	void GetRandoms()
    {
        _random1 = Random.Range(0, 6);
        _random2 = Random.Range(0, 6);
        _random3 = Random.Range(0, 6);

        if(_random1==_random2||_random1==_random3|| _random2==_random3)
        {
            GetRandoms();
        }

        /*
        if (PlayerPrefs.GetInt(trait1.GetComponent<TraitSelected>().trait) == 5)
        {
            trait1.GetComponent<TraitSelected>().sprite.GetComponent<SpriteRenderer>().color = Color.gray;
            gray++;
        }
        if (PlayerPrefs.GetInt(trait2.GetComponent<TraitSelected>().trait) == 5)
        {
            trait2.GetComponent<TraitSelected>().sprite.GetComponent<SpriteRenderer>().color = Color.gray;
            gray++;
        }
        if (PlayerPrefs.GetInt(trait3.GetComponent<TraitSelected>().trait) == 5)
        {
            trait3.GetComponent<TraitSelected>().sprite.GetComponent<SpriteRenderer>().color = Color.gray;
            gray++;
        }
        */

    }

    public void UpdateSelected()
    {
        traitSelected.GetComponent<TraitSelected>().sprite.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void Deselec()
    {
        if(traitSelected!=null)
        {
            traitSelected.GetComponent<TraitSelected>().sprite.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
    }

    public void Next()
    {
        if(traitSelected!=null || gray==3)
        {
            int sum = PlayerPrefs.GetInt(traitSelected.GetComponent<TraitSelected>().trait) + 1;
            if (sum > 5)
            {
                sum = 5;
            }
            PlayerPrefs.SetInt(traitSelected.GetComponent<TraitSelected>().trait, sum);
            SceneManager.LoadScene(2);
        }
        
    }
}
