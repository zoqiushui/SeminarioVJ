using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



public class CustomizeCharacter : MonoBehaviour
{

    public InputField pilotNameField;
    public string currentPilotName;
    private int currentColorSkin;
    private int currentFace;
    private int currentHair;
    private int currentColorHair;
    private int currentAccesory;
    public int currentFaceHair;

    public int currentFlag;


    
    //Referencias a sus hijos
    public GameObject gameObjectHead;
    public GameObject gameObjectFace;
    public GameObject gameObjectHair;
    public GameObject gameObjectFaceHair;
    public GameObject gameObjectAccesory;

    public GameObject warning;

    public GameObject gameObjectFlag;


   

    //Names
    //List<string> names= new List<string>();



    void Start()
    {
        Cursor.visible = true;
        //ListNames();
    }

    void Awake()
    {
        currentColorSkin = 0;
        currentFace = 0;
        currentHair = 0;
        currentColorHair = 0;
        currentAccesory = 0;
        currentFaceHair = 0;
      
        UpdatePortrait();
        RandomFace();

        currentPilotName = null;
    }

    public void RandomFace()
    {
        currentColorSkin = Random.Range(0, K.arrayColorSkin.Length);
        currentFace = Random.Range(0, K.spritesFace.Length);
        currentHair = Random.Range(0, K.spritesHair.Length);
        currentColorHair = Random.Range(0, K.arrayColorHair.Length);
        currentAccesory = Random.Range(0, K.spritesAccesory.Length);
        currentFaceHair = Random.Range(0, K.spritesFacialHair.Length);
        UpdatePortrait();
    }


    public void RandomName()
    {
        int random = Random.Range(0, K.names.Count);
        currentPilotName = K.names[random];
        UpdateRandomName();
    }

    public void UpdateName()
    {
        currentPilotName = pilotNameField.text;
        print("Pilot's name: " + currentPilotName);
    }

    void UpdateRandomName()
    {
        pilotNameField.text = currentPilotName; 
        print("Pilot's name: " + currentPilotName);
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

        gameObjectFlag.GetComponent<SpriteRenderer>().sprite = K.spritesFlag[currentFlag];

    }

    public void NextColorSkin()
    {
        if (currentColorSkin != K.arrayColorSkin.Length - 1)
        {
            currentColorSkin++;
        }
        else
        {
            currentColorSkin = 0;
        }

        gameObjectHead.GetComponent<SpriteRenderer>().color = K.arrayColorSkin[currentColorSkin];

    }

    public void NextColorHair()
    {
        if (currentColorHair != K.arrayColorHair.Length - 1)
        {
            currentColorHair++;
        }
        else
        {
            currentColorHair = 0;
        }

        gameObjectHair.GetComponent<SpriteRenderer>().color = K.arrayColorHair[currentColorHair];
        gameObjectFaceHair.GetComponent<SpriteRenderer>().color = K.arrayColorHair[currentColorHair];

    }


    public void NextFace()
    {
        if (currentFace != K.spritesFace.Length - 1)
        {
            currentFace++;
        }
        else
        {
            currentFace = 0;
        }

        gameObjectFace.GetComponent<SpriteRenderer>().sprite = K.spritesFace[currentFace];
    }

    public void NextHair()
    {
        if (currentHair != K.spritesHair.Length - 1)
        {
            currentHair++;
        }
        else
        {
            currentHair = 0;
        }

        gameObjectHair.GetComponent<SpriteRenderer>().sprite = K.spritesHair[currentHair];
    }

    public void NextFacialHair()
    {
        if (currentFaceHair != K.spritesFacialHair.Length - 1)
        {
            currentFaceHair++;
        }
        else
        {
            currentFaceHair = 0;
        }

        gameObjectFaceHair.GetComponent<SpriteRenderer>().sprite = K.spritesFacialHair[currentFaceHair];
    }

    public void NextAccesory()
    {
        if (currentAccesory != K.spritesAccesory.Length - 1)
        {
            currentAccesory++;
        }
        else
        {
            currentAccesory = 0;
        }

        gameObjectAccesory.GetComponent<SpriteRenderer>().sprite = K.spritesAccesory[currentAccesory];
    }

    public void NextFlag()
    {
        if (currentFlag != K.spritesFlag.Length - 1)
        {
            currentFlag++;
        }
        else
        {
            currentFlag = 0;
        }

        gameObjectFlag.GetComponent<SpriteRenderer>().sprite = K.spritesFlag[currentFlag];
    }


    public void PrevFlag()
    {
        if (currentFlag != 0)
        {
            currentFlag--;
        }
        else
        {
            currentFlag = K.spritesFlag.Length - 1;
        }

        gameObjectFlag.GetComponent<SpriteRenderer>().sprite = K.spritesFlag[currentFlag];
    }


    public void Done()
    {
        if (currentPilotName != null && currentPilotName != "")
        {
            PlayerPrefs.SetInt("Country", currentFlag);
            PlayerPrefs.SetString("PilotName", currentPilotName);
            PlayerPrefs.SetInt("ColorSkin", currentColorSkin);
            PlayerPrefs.SetInt("Face", currentFace);
            PlayerPrefs.SetInt("Hair", currentHair);
            PlayerPrefs.SetInt("ColorHair", currentColorHair);
            PlayerPrefs.SetInt("Accesory", currentAccesory);
            PlayerPrefs.SetInt("FaceHair", currentFaceHair);

            SceneManager.LoadScene(2);

        }
        else
        {
            warning.SetActive(true);
        }
    }

   
}
