using UnityEngine;
using System.Collections;

public class PortraitScript : MonoBehaviour {

    //public string currentPilotName;
    private int currentColorSkin;
    private int currentFace;
    private int currentHair;
    private int currentColorHair;
    private int currentAccesory;
    private int currentFaceHair;

    //public int currentFlag;


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



    //Sprites
    Sprite[] spritesFace;
    Sprite[] spritesHair;
    Sprite[] spritesAccesory;
    Sprite[] spritesFacialHair;
    Sprite[] spritesFlag;

    //Statics




    void Start()
    {
        Cursor.visible = true;
    }

    void Awake()
    {

        /*
        if (resources==0)
        {
            PlayerPrefs.SetInt("Resources", 10);
            Debug.Log(resources);
        }
        */

        currentColorSkin = PlayerPrefs.GetInt("ColorSkin");
        currentFace = PlayerPrefs.GetInt("Face"); 
        currentHair = PlayerPrefs.GetInt("Hair");
        currentColorHair = PlayerPrefs.GetInt("ColorHair");
        currentAccesory = PlayerPrefs.GetInt("Accesory");
        currentFaceHair = PlayerPrefs.GetInt("FaceHair");

        spritesFace = Resources.LoadAll<Sprite>("Sprites/Face");
        spritesHair = Resources.LoadAll<Sprite>("Sprites/Hair");
        spritesAccesory = Resources.LoadAll<Sprite>("Sprites/Accesory");
        spritesFacialHair = Resources.LoadAll<Sprite>("Sprites/FacialHair");
        spritesFlag = Resources.LoadAll<Sprite>("Sprites/Flags");

        UpdatePortrait();

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

        //gameObjectFlag.GetComponent<SpriteRenderer>().sprite = spritesFlag[currentFlag];

    }
}
