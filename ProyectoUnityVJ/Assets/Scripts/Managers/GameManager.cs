using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;
    public Text youWin;
    public Text raceFinishedText;
    public Text youLoseText;
    public Button restartButton;
    public VehicleController playerReference { get; private set; }

    private List<IAController> _enemiesReferences, _destroyedEnemies;
    private ReferencesManager _refManager;

    private void Awake()
    {
        //if (instance == null) instance = this;
        playerReference = GameObject.FindGameObjectWithTag(K.TAG_PLAYER).GetComponent<VehicleController>();
        _enemiesReferences = new List<IAController>();
        _enemiesReferences.AddRange(GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentsInChildren<IAController>());
    }

    private void Start()
    {
        _refManager = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<ReferencesManager>();
    }

    private void Update()
    {
        if (Mathf.FloorToInt(playerReference.lapCount) == K.MAX_LAPS)
        {
            //GameOver("You Win");
            _refManager.ingameUIManagerReference.AddEndRacer(playerReference.vehicleName);
            playerReference.EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("Race Finished");
        }
        foreach (var enemy in _enemiesReferences)
        {
            if (Mathf.FloorToInt(enemy.lapCount) == K.MAX_LAPS)
            {
                //GameOver("You Lose");
                _refManager.ingameUIManagerReference.AddEndRacer(enemy.vehicleName);
                enemy.enabled = false;
            }
        }
        if (_enemiesReferences.Count == 0)
        {
            playerReference.EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("You Win");
        }

        if (playerReference.gameObject.GetComponent<VehicleData>().currentLife <= 0)
        {
            print(playerReference.gameObject.GetComponent<VehicleData>().currentLife);
            playerReference.EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("You Lose");
        }
    }

    public void RemoveEnemy(IAController ene)
    {
        _refManager.ingameUIManagerReference.AddDestroyedEnemy(ene.vehicleName);
        _enemiesReferences.Remove(ene);
    }

    private void GameOver(string s)
    {
        switch (s)
        {
            case "You Win":
                {
                    youWin.gameObject.SetActive(true);
                    PlayerPrefs.SetInt("Resources", 20);
                    SaveDamageInfo();
                }
                break;
            case "Race Finished":
                {
                    raceFinishedText.gameObject.SetActive(true);
                    PlayerPrefs.SetInt("Resources", 10);
                    SaveDamageInfo();
                }
                break;
            case "You Lose":
                {
                    DeletePlayer();
                    //SceneManager.LoadScene(1);
                } 
                break;
            default:
                break;
        }
        restartButton.gameObject.SetActive(true);
    }

    void DeletePlayer()
    {
        PlayerPrefs.SetString("PilotName", "");
        PlayerPrefs.SetInt("Face", 0);
        PlayerPrefs.SetInt("Hair", 0);
        PlayerPrefs.SetInt("FaceHair", 0);
        PlayerPrefs.SetInt("Accesory", 0);
        PlayerPrefs.SetInt("HairColor", 0);
        PlayerPrefs.SetInt("SkinColor", 0);
    }

    void SaveDamageInfo()
    {
        PlayerPrefs.SetInt("MaxLife", (int) playerReference.gameObject.GetComponent<VehicleData>().maxLife);
        PlayerPrefs.SetInt("CurrentLife", (int)playerReference.gameObject.GetComponent<VehicleData>().currentLife);
    }
}
