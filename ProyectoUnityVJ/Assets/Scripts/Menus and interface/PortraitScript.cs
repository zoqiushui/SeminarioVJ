using UnityEngine;
using System.Collections;

public class PortraitScript : MonoBehaviour {

    public bool playerPortrait;


    //public string currentPilotName;
    private int currentColorSkin;
    private int currentFace;
    private int currentHair;
    private int currentColorHair;
    private int currentAccesory;
    private int currentFaceHair;

    //public int currentFlag;
    


    //Referencias a sus hijos
    public GameObject gameObjectHead;
    public GameObject gameObjectFace;
    public GameObject gameObjectHair;
    public GameObject gameObjectFaceHair;
    public GameObject gameObjectAccesory;






    void Start()
    {
        Cursor.visible = true;
    }

    void Awake()
    {
        if(playerPortrait==true)
        {
            currentColorSkin = PlayerPrefs.GetInt("ColorSkin");
            currentFace = PlayerPrefs.GetInt("Face");
            currentHair = PlayerPrefs.GetInt("Hair");
            currentColorHair = PlayerPrefs.GetInt("ColorHair");
            currentAccesory = PlayerPrefs.GetInt("Accesory");
            currentFaceHair = PlayerPrefs.GetInt("FaceHair");

        }
        else
        {
            currentColorSkin = Random.Range(0, K.arrayColorSkin.Length);
            currentFace = Random.Range(0, K.spritesFace.Length);
            currentHair = Random.Range(0, K.spritesHair.Length);
            currentColorHair = Random.Range(0, K.arrayColorHair.Length);
            currentAccesory = Random.Range(0, K.spritesAccesory.Length);
            currentFaceHair = Random.Range(0, K.spritesFacialHair.Length);
        }

        

        UpdatePortrait();

    }

 

    public void UpdatePortrait()
    {
        gameObjectHead.GetComponent<SpriteRenderer>().color = K.arrayColorSkin[currentColorSkin];

        gameObjectFace.GetComponent<SpriteRenderer>().sprite = K.spritesFace[currentFace];

        gameObjectHair.GetComponent<SpriteRenderer>().sprite = K.spritesHair[currentHair];

        gameObjectFaceHair.GetComponent<SpriteRenderer>().sprite = K.spritesFacialHair[currentFaceHair];

        gameObjectAccesory.GetComponent<SpriteRenderer>().sprite = K.spritesAccesory[currentAccesory];

        gameObjectFaceHair.GetComponent<SpriteRenderer>().color = K.arrayColorHair[currentColorHair];

        gameObjectHair.GetComponent<SpriteRenderer>().color = K.arrayColorHair[currentColorHair];

        //gameObjectFlag.GetComponent<SpriteRenderer>().sprite = spritesFlag[currentFlag];

    }
}
