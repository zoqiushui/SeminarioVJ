using UnityEngine;
using System.Collections;

public class TraitNumber : MonoBehaviour {

    public Pilot pilot;

    public enum TraitTypes
    {
        BonusMaxSpeed,
        BonusTurbo,
        BonusAcceleration,
        BonusAmmo,
        BonusAmmoReload,
        BonusLessMineDamage
    }


    public TraitTypes trait;

	void Start ()
    {
        switch (trait)
        {
            case TraitTypes.BonusMaxSpeed:
                this.gameObject.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BonusMaxSpeed").ToString();
                break;
            case TraitTypes.BonusTurbo:
                this.gameObject.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BonusTurbo").ToString();
                break;
            case TraitTypes.BonusAcceleration:
                this.gameObject.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BonusAcceleration").ToString();
                break;
            case TraitTypes.BonusAmmo:
                this.gameObject.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BonusAmmo").ToString();
                break;
            case TraitTypes.BonusAmmoReload:
                this.gameObject.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BonusAmmoReload").ToString();
                break;
            case TraitTypes.BonusLessMineDamage:
                this.gameObject.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("BonusLessMineDamage").ToString();
                break;
        }
        

	}
	
	
}
