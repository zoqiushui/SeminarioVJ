using UnityEngine;
using System.Collections;

public class MolotovBomb : MonoBehaviour
{
    public GameObject bomb;
    public GameObject fire;

	// Use this for initialization
	void Start ()
    {
        bomb.SetActive(true);
        fire.SetActive(false);
	
	}

    public void StartBurn(Vector3 posi)
    {
        transform.position = posi;
        bomb.SetActive(false);
        fire.SetActive(true);
    }
	
}
