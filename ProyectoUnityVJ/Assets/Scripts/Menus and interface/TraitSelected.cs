using UnityEngine;
using System.Collections;

public class TraitSelected : MonoBehaviour {

    public GameObject sprite;
    public GameObject traitName;
    public GameObject description;

    public PostGameManager postGameManager;

    public string trait;
    

    public enum TraitTypes
    {
        BonusMaxSpeed,
        BonusTurbo,
        BonusAcceleration,
        BonusAmmo,
        BonusAmmoReload,
        BonusLessMineDamage
    }
    //public TraitTypes trait;


    void Start()
    {
        if (postGameManager==null)
        {
            GameObject.Find("Post Game Manager");
        }
        

        //Assign();
    }

    // Use this for initialization
    public void Assign (int random)
    {
        /*
        //Asignación
        if (trait==TraitTypes.BonusMaxSpeed)
        {
            //Debug.Log("BonusMaxSpeed");
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusMaxSpeed");
            traitName.GetComponent<TextMesh>().text = "Extra Max Speed";
            description.GetComponent<TextMesh>().text = "Gotta go fast";
        }
        else if (trait == TraitTypes.BonusTurbo)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusMaxTurbo");
            traitName.GetComponent<TextMesh>().text = "Extra Max Turbo";
            description.GetComponent<TextMesh>().text = "Faster better stronger";
        }
        else if (trait == TraitTypes.BonusAcceleration)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusAccel");
        }
        else if (trait == TraitTypes.BonusAmmo)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusAmmo");
        }
        else if (trait == TraitTypes.BonusAmmoReload)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusReload");
        }
        else if (trait == TraitTypes.BonusLessMineDamage)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusMine");
        }
        else
        {
            Debug.Log("Error: Enum no asignado");
        }
        */

        //Asignación
        if (random==0)
        {
            //Debug.Log("BonusMaxSpeed");
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusMaxSpeed");
            traitName.GetComponent<TextMesh>().text = "Extra Max Speed";
            description.GetComponent<TextMesh>().text = "Gotta go fast";
            trait = "BonusMaxSpeed";
        }
        else if (random == 1)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusMaxTurbo");
            traitName.GetComponent<TextMesh>().text = "Extra Max Turbo";
            description.GetComponent<TextMesh>().text = "Faster better stronger";
            trait = "BonusTurbo";
        }
        else if (random == 2)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusAccel");
            traitName.GetComponent<TextMesh>().text = "More acceleration";
            description.GetComponent<TextMesh>().text = "Fast and blind";
            trait = "BonusAcceleration";
        }
        else if (random == 3)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusAmmo");
            traitName.GetComponent<TextMesh>().text = "Extra Ammo";
            description.GetComponent<TextMesh>().text = "Is never enough ammo";
            trait = "BonusAmmo";
        }
        else if (random == 4)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusReload");
            traitName.GetComponent<TextMesh>().text = "Reload faster";
            description.GetComponent<TextMesh>().text = "Quit with the downtime!";
            trait = "BonusAmmoReload";
        }
        else if (random == 5)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Icons/BonusMine");
            traitName.GetComponent<TextMesh>().text = "Less damaging mines";
            description.GetComponent<TextMesh>().text = "It was just a scratch";
            trait = "BonusLessMineDamage";
        }
        else
        {
            Debug.Log("Error: Enum no asignado");
        }


    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Selected()
    {
      
            postGameManager.GetComponent<PostGameManager>().Deselec();
            postGameManager.traitSelected = this.gameObject;
            postGameManager.GetComponent<PostGameManager>().UpdateSelected();
         
    }
}
