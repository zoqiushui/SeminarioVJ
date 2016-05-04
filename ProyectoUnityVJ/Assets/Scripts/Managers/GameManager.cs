using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text youWin;
    public Text youLose;

    private VehicleController _playerReference;
    private List<IAController> _enemiesReferences, _destroyedEnemies;

    private void Awake()
    {
        if (instance == null) instance = this;
        _playerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<VehicleController>();
        _enemiesReferences = new List<IAController>();
        _enemiesReferences.AddRange(GameObject.Find("VEHICLES").GetComponentsInChildren<IAController>());
    }

    private void Update()
    {
        if (Mathf.FloorToInt(_playerReference.lapCount) == K.MAX_LAPS)
        {
            GameOver("You Win");
        }
        foreach (var enemy in _enemiesReferences)
        {
            if (Mathf.FloorToInt(enemy.lapCount) == K.MAX_LAPS)
            {
                GameOver("You Lose");
            }
        }
        if (_enemiesReferences.Count == 0)
        {
            GameOver("You Win");
        }
    }

    public void RemoveEnemy(IAController ene)
    {
        _enemiesReferences.Remove(ene);
    }

    private void GameOver(string s)
    {
        switch (s)
        {
            case "You Win": youWin.gameObject.SetActive(true);
                break;
            case "You Lose": youLose.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        Time.timeScale = 0;
    }
}
