using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;



public class CustomizeCharacter : MonoBehaviour {

    public InputField pilotNameField;
    public string currentPilotName;
    private int currentColorSkin;
    private int currentFace;
    private int currentHair;
    private int currentColorHair;
    private int currentAccesory;
    public int currentFaceHair;

    public int currentFlag;


    //Colores de pelo
    //private Color[] arrayColorHair = new Color[] { new Color(28f, 20f, 12f), new Color(255, 230, 124), new Color(111, 67, 11) };
    private Color[] arrayColorHair = new Color[] { new Color(0.10980392156f, 0.07843137254f, 0.04705882352f), new Color(1, 0.90196078431f, 0.4862745098f), new Color(0.30196078431f, 0.26666666666f, 0.20784313725f) };


    //Colores de piel
    //private Color[] arrayColorSkin = new Color[] { new Color(242f, 232f, 217f), new Color(221, 183, 125), new Color(124, 90, 40) };
    private Color[] arrayColorSkin = new Color[] { new Color(0.94901960784f, 0.90980392156f, 0.85098039215f), new Color(0.86666666666f, 0.71764705882f, 0.49019607843f), new Color(0.43529411764f, 0.26274509803f, 0.0431372549f) };

    //Referencias a sus hijos
    public GameObject gameObjectHead;
    public GameObject gameObjectFace;
    public GameObject gameObjectHair;
    public GameObject gameObjectFaceHair;
    public GameObject gameObjectAccesory;

    public GameObject warning;

    public GameObject gameObjectFlag;

    public VarManager varManager;

    //Sprites
    Sprite[] spritesFace;
    Sprite[] spritesHair;
    Sprite[] spritesAccesory;
    Sprite[] spritesFacialHair;
    Sprite[] spritesFlag;

    //Statics


   

    void Start ()
    {
    }

    void Awake()
    {


        currentColorSkin = 0;
        currentFace = 0;
        currentHair = 0;
        currentColorHair = 0;
        currentAccesory = 0;
        currentFaceHair = 0;

        spritesFace = Resources.LoadAll<Sprite>("Sprites/Face");
        spritesHair = Resources.LoadAll<Sprite>("Sprites/Hair");
        spritesAccesory = Resources.LoadAll<Sprite>("Sprites/Accesory");
        spritesFacialHair = Resources.LoadAll<Sprite>("Sprites/FacialHair");
        spritesFlag = Resources.LoadAll<Sprite>("Sprites/Flags");



        UpdatePortrait();

        currentPilotName = null;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void UpdateName()
    {
        currentPilotName = pilotNameField.text;
        Debug.Log("Pilot's name: " + currentPilotName);
    }

    public void UpdatePortrait()
    {
        gameObjectHead.GetComponent<SpriteRenderer>().color = arrayColorSkin[currentColorSkin];

        gameObjectFace.GetComponent<SpriteRenderer>().sprite = spritesFace[currentFace];

        gameObjectHair.GetComponent<SpriteRenderer>().sprite = spritesHair[currentHair];

        gameObjectFaceHair.GetComponent<SpriteRenderer>().sprite = spritesFacialHair[currentFaceHair];

        gameObjectAccesory.GetComponent<SpriteRenderer>().sprite = spritesAccesory[currentAccesory];

        gameObjectFaceHair.GetComponent<SpriteRenderer>().color = arrayColorHair[currentColorHair];

        gameObjectHair.GetComponent<SpriteRenderer>().color = arrayColorHair[currentColorHair];

        gameObjectFlag.GetComponent<SpriteRenderer>().sprite = spritesFlag[currentFlag];

    }

    public void NextColorSkin()
    {
        if(currentColorSkin!= arrayColorSkin.Length-1)
        {
            currentColorSkin++;
        }
        else
        {
            currentColorSkin = 0;
        }

        gameObjectHead.GetComponent<SpriteRenderer>().color = arrayColorSkin[currentColorSkin];

    }

    public void NextColorHair()
    {
        if (currentColorHair != arrayColorHair.Length - 1)
        {
            currentColorHair++;
        }
        else
        {
            currentColorHair = 0;
        }

        gameObjectHair.GetComponent<SpriteRenderer>().color = arrayColorHair[currentColorHair];
        gameObjectFaceHair.GetComponent<SpriteRenderer>().color = arrayColorHair[currentColorHair];

    }


    public void NextFace()
    {
        if (currentFace != spritesFace.Length - 1)
        {
            currentFace++;
        }
        else
        {
            currentFace = 0;
        }

        gameObjectFace.GetComponent<SpriteRenderer>().sprite = spritesFace[currentFace];
    }

    public void NextHair()
    {
        if (currentHair != spritesHair.Length - 1)
        {
            currentHair++;
        }
        else
        {
            currentHair = 0;
        }

        gameObjectHair.GetComponent<SpriteRenderer>().sprite = spritesHair[currentHair];
    }

    public void NextFacialHair()
    {
        if (currentFaceHair != spritesFacialHair.Length - 1)
        {
            currentFaceHair++;
        }
        else
        {
            currentFaceHair = 0;
        }

        gameObjectFaceHair.GetComponent<SpriteRenderer>().sprite = spritesFacialHair[currentFaceHair];
    }

    public void NextAccesory()
    {
        if (currentAccesory != spritesAccesory.Length - 1)
        {
            currentAccesory++;
        }
        else
        {
            currentAccesory = 0;
        }

        gameObjectAccesory.GetComponent<SpriteRenderer>().sprite = spritesAccesory[currentAccesory];
    }

    public void NextFlag()
    {
        if (currentFlag != spritesFlag.Length - 1)
        {
            currentFlag++;
        }
        else
        {
            currentFlag = 0;
        }

        gameObjectFlag.GetComponent<SpriteRenderer>().sprite = spritesFlag[currentFlag];
    }


    public void PrevFlag()
    {
        if (currentFlag != 0)
        {
            currentFlag--;
        }
        else
        {
            currentFlag = spritesFlag.Length-1;
        }

        gameObjectFlag.GetComponent<SpriteRenderer>().sprite = spritesFlag[currentFlag];
    }


    public void Done()
    {
        if (currentPilotName != null)
        {
            //varManager.pilotName = currentPilotName;
            //varManager.country = currentFace;

            varManager.UpdateVars(currentFlag, currentPilotName);

            SceneManager.LoadScene(1);

        }
        else
        {
            warning.SetActive(true);
        }
    }

}
