using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IAController : Vehicle
{
    public GameObject hpBarContainer;
    public RawImage hpBarImage;
    public GameObject remains;

    private float _maxHp, _currentHp, _speed;
    private Checkpoint _nextCheckpoint;
    private Vector3 _aux, _nextDestinationPoint;

    private void Start()
    {
        _maxHp = K.IA_MAX_HP;
        _currentHp = _maxHp;
        _aux = hpBarImage.transform.localScale;
        _speed = K.IA_MAX_SPEED;
        lapCount = 1;
        _nextCheckpoint = CheckpointManager.instance.checkpointsList[0];
        positionWeight = -Vector3.Distance(transform.position, _nextCheckpoint.transform.position);
        CalculateNextPoint(_nextCheckpoint);
    }

    private void Update()
    {
        positionWeight = Vector3.Distance(transform.position, _nextCheckpoint.transform.position);

        UpdateHpBar();

        if (Vector3.Distance(transform.position, _nextDestinationPoint) < 20)
        {
            lapCount += CheckpointManager.instance.checkpointValue;
            CalculateNextCheckpoint(_nextCheckpoint);
            CalculateNextPoint(_nextCheckpoint);
        }
        transform.forward = Vector3.Slerp(transform.forward, _nextDestinationPoint - transform.position, 5 * Time.deltaTime);
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void CalculateNextCheckpoint(Checkpoint chk)
    {
        _nextCheckpoint = chk.nextCheckpoint;
    }

    private void UpdateHpBar()
    {
        hpBarContainer.transform.LookAt(Camera.main.transform.position);
        _aux.x = _currentHp / _maxHp;
        hpBarImage.transform.localScale = _aux;
    }

    private void CalculateNextPoint(Checkpoint chk)
    {
        _nextDestinationPoint = chk.GetRandomPositionFromNode();
        /*_nextDestinationPoint = Vector3.zero;
        Ray ray = new Ray(new Vector3(chk.transform.position.x - Random.Range(chk.transform.localScale.x / 2, -(chk.transform.localScale.x / 2)), chk.transform.position.y * 200, chk.transform.position.z - Random.Range(chk.transform.localScale.z / 2, -(chk.transform.localScale.z / 2))), -Vector3.up);
        var raycastHits = Physics.RaycastAll(ray, Mathf.Infinity);
        foreach (var item in raycastHits)
        {
            if (item.collider.gameObject.layer == K.LAYER_GROUND)
            {
                chk = chk.nextCheckpoint;
                _nextDestinationPoint = item.point + Vector3.up;
                break;
            }
        }
        print(_nextDestinationPoint);
        if (_nextDestinationPoint == Vector3.zero)
        {
            CalculateNextPoint(chk);
        }*/
    }

    public void Damage(float d)
    {
        print("entre");
        _currentHp -= d;

        if (_currentHp <= 0)
        {
            GameManager.instance.RemoveEnemy(this);
            Destroy(this.gameObject);
            Instantiate(remains, transform.position, transform.rotation);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _nextDestinationPoint);
    }
}
