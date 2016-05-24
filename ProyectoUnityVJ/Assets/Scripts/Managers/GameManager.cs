using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : Manager
{
    public Text youWin;
    public Text raceFinishedText;
    public Text youLoseText;
    public Button restartButton;
    public Vehicle playerReference { get; private set; }
    private List<Vehicle> _enemiesReferences;
    private IngameUIManager _ingameUIManagerReference;

    private void Awake()
    {
        //if (instance == null) instance = this;
        playerReference = GameObject.FindGameObjectWithTag(K.TAG_PLAYER).GetComponent<Vehicle>();
        _enemiesReferences = new List<Vehicle>();
        _enemiesReferences.AddRange(GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentsInChildren<IAController>());
    }

    private void Start()
    {
        _ingameUIManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<IngameUIManager>();
    }

    private void Update()
    {
        if (Mathf.FloorToInt(playerReference.lapCount) == K.MAX_LAPS)
        {
            playerReference.NotifyObserver(K.OBS_MESSAGE_FINISHED);
            ((JeepController)playerReference).EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("Race Finished");
        }
        foreach (var enemy in _enemiesReferences)
        {
            if (Mathf.FloorToInt(enemy.lapCount) == K.MAX_LAPS)
            {
                //GameOver("You Lose");
                enemy.NotifyObserver(K.OBS_MESSAGE_FINISHED);
                enemy.enabled = false;
            }
        }
        if (_enemiesReferences.Count == 0)
        {
            ((JeepController)playerReference).EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("You Win");
        }

        if (playerReference.gameObject.GetComponent<VehicleData>().currentLife <= 0)
        {
            print(playerReference.gameObject.GetComponent<VehicleData>().currentLife);
            ((JeepController)playerReference).EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("You Lose");
        }
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

    public override void Notify(Vehicle caller, string msg)
    {
        switch (msg)
        {
            case K.OBS_MESSAGE_DESTROYED:
                if (caller is IAController && !_enemiesReferences.Contains(caller))
                {
                    _enemiesReferences.Remove(caller);
                }
                break;

            default:
                break;
        }

    }
}
