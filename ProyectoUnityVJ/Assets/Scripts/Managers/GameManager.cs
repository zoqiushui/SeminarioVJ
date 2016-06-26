using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{
    public Text youWin;
    public Text raceFinishedText;
    public Text youLoseText;
    public Button restartButton;
    public Vehicle playerReference { get; private set; }
    private List<Vehicle> _enemiesReferences;
    private IngameUIManager _ingameUIManagerReference;

    private bool paused = false;
    public GameObject pauseCanvas;

    public static bool disableShoot = false;
    private bool _oneTime;
    private void Awake()
    {
        //if (instance == null) instance = this;
        playerReference = GameObject.FindGameObjectWithTag(K.TAG_PLAYER).GetComponent<Vehicle>();
        _enemiesReferences = new List<Vehicle>();
        _enemiesReferences.AddRange(GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentsInChildren<IAVehicle>());
        disableShoot = false;
        _oneTime = false;
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
            ((BuggyController)playerReference).EndRaceHandbrake();
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
            ((BuggyController)playerReference).EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("You Win");
        }

        if (playerReference.gameObject.GetComponent<BuggyData>().currentLife <= 0)
        {
            ((BuggyController)playerReference).EndRaceHandbrake();
            playerReference.enabled = false;
            GameOver("You Lose");
        }

        PauseInput();
    }

    private void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = true;
                disableShoot = false;
            }
            else
            {
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0f;
                Cursor.lockState = CursorLockMode.None;
                playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = false;
                disableShoot = true;
            }

            pauseCanvas.SetActive(!paused);
            Cursor.visible = !paused;
            paused = !paused;
        }
        
        if (disableShoot)
        {
            if (!_oneTime)
            {
                playerReference.gameObject.GetComponent<InputControllerPlayer>().enabled = false;
                playerReference.enabled = false;
                foreach (var enemy in _enemiesReferences)
                {
                    enemy.gameObject.GetComponent<InputControllerIA>().enabled = false;
                    enemy.enabled = false;
                }
                _oneTime = true;
            }
        }
        else
        {
           if (_oneTime)
           {
               playerReference.gameObject.GetComponent<InputControllerPlayer>().enabled = true;
               playerReference.enabled = true;
               foreach (var enemy in _enemiesReferences)
               {
                   enemy.gameObject.GetComponent<InputControllerIA>().enabled = true;
                   enemy.enabled = true;
               }
               _oneTime = false;
           }
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        pauseCanvas.SetActive(!paused);
        Cursor.visible = !paused;
        paused = !paused;
        playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = true;
        disableShoot = false;
    }
    private void GameOver(string s)
    {
        print(s);
        switch (s)
        {
            case "You Win":
                {
                    youWin.gameObject.SetActive(true);
                    int currentResources=PlayerPrefs.GetInt("Resources");
                    PlayerPrefs.SetInt("Resources", currentResources+20);
                    SaveDamageInfo();
                    BasicSettings();
                }
                break;
            case "Race Finished":
                {
                    int currentResources = PlayerPrefs.GetInt("Resources");
                    raceFinishedText.gameObject.SetActive(true);
                    PlayerPrefs.SetInt("Resources", currentResources + 10);
                    SaveDamageInfo();
                    BasicSettings();
                }
                break;
            case "You Lose":
                {
                    DeletePlayer();
                    //SceneManager.LoadScene(2);
                    BasicSettings();
                } 
                break;
            default:
                break;
        }
        restartButton.gameObject.SetActive(true);
    }

    private void BasicSettings()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = false;
        playerReference.gameObject.GetComponent<InputControllerPlayer>().enabled = false;
        playerReference.enabled = false;
        playerReference.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
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
                if (caller is IAVehicle && _enemiesReferences.Contains(caller))
                {
                    _enemiesReferences.Remove(caller);
                    print(caller.gameObject.name);
                }
                break;

            default:
                break;
        }

    }
}
