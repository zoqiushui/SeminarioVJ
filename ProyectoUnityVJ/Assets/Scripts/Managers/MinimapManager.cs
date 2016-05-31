using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinimapManager : MonoBehaviour
{
    public GameObject minimapPointPrefab;
    public Material playerMinimapTexture, enemyMinimapTexture;

    private Vehicle _playerReference;
    private GameObject _playerGo;
    private Vector3 _fixedYPosition;
    private Dictionary<IAController, GameObject> _enemiesReferences;
    private List<IAController> _dictionaryKeys;

    private void Start()
    {
        GameObject go;
        _playerReference = GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentInChildren<JeepController>();
        if (_playerReference == null) _playerReference = GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentInChildren<VehicleController>();
        _fixedYPosition = _playerReference.transform.position + Vector3.up * K.MINIMAP_HEIGHT;
        _enemiesReferences = new Dictionary<IAController, GameObject>();
        var aux = GameObject.Find(K.CONTAINER_VEHICLES_NAME).GetComponentsInChildren<IAController>();
        foreach (var item in aux)
        {
            go = (GameObject)Instantiate(minimapPointPrefab, _fixedYPosition, Quaternion.identity);
            go.GetComponent<Renderer>().material = enemyMinimapTexture;
            go.transform.parent = GameObject.Find("MinimapPointsContainers").transform;
            _enemiesReferences.Add(item, go);
        }

        go = (GameObject)Instantiate(minimapPointPrefab, _fixedYPosition, Quaternion.identity);
        go.GetComponent<Renderer>().material = playerMinimapTexture;
        go.transform.parent = GameObject.Find("MinimapPointsContainers").transform;
        _playerGo = go;
        _dictionaryKeys = new List<IAController>();
    }

    private void Update()
    {
        _fixedYPosition.x = _playerReference.transform.position.x;
        _fixedYPosition.z = _playerReference.transform.position.z;
        _playerGo.transform.position = _fixedYPosition;

        _dictionaryKeys.AddRange(_enemiesReferences.Keys);
        foreach (var key in _dictionaryKeys)
        {
            if (key == null)
            {
                Destroy(_enemiesReferences[key].gameObject);
                _enemiesReferences.Remove(key);
            }
            else
            {
                _fixedYPosition.x = key.transform.position.x;
                _fixedYPosition.z = key.transform.position.z;
                _enemiesReferences[key].transform.position = _fixedYPosition - Vector3.up;
            }
        }
        _dictionaryKeys.Clear();
    }

}


