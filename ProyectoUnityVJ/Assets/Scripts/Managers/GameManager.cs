using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text youWin;
    public Text raceFinishedText;
    public Button restartButton;
    public VehicleController playerReference { get; private set; }

    private List<IAController> _enemiesReferences, _destroyedEnemies;

    private void Awake()
    {
        if (instance == null) instance = this;
        playerReference = GameObject.FindGameObjectWithTag(K.TAG_PLAYER).GetComponent<VehicleController>();
        _enemiesReferences = new List<IAController>();
        _enemiesReferences.AddRange(GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentsInChildren<IAController>());
    }

    private void Update()
    {
        if (Mathf.FloorToInt(playerReference.lapCount) == K.MAX_LAPS)
        {
            //GameOver("You Win");
            IngameUIManager.instance.AddEndRacer(playerReference.vehicleName);
            GameOver("Race Finished");
        }
        foreach (var enemy in _enemiesReferences)
        {
            if (Mathf.FloorToInt(enemy.lapCount) == K.MAX_LAPS)
            {
                //GameOver("You Lose");
                IngameUIManager.instance.AddEndRacer(enemy.vehicleName);
            }
        }
        if (_enemiesReferences.Count == 0)
        {
            GameOver("You Win");
        }
    }

    public void RemoveEnemy(IAController ene)
    {
        IngameUIManager.instance.AddDestroyedEnemy(ene.vehicleName);
        _enemiesReferences.Remove(ene);
    }

    private void GameOver(string s)
    {
        switch (s)
        {
            case "You Win": youWin.gameObject.SetActive(true);
                break;
            case "Race Finished": raceFinishedText.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        restartButton.gameObject.SetActive(true);
    }
}
