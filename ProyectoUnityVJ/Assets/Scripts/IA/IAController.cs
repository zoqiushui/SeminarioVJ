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
        lapCount = 0;
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

    /// <summary>
    /// Tomo el proximo checkpoint y calculo un punto aleatorio dentro del mismo, si hay un obstaculo vuelvo a calcular. 
    /// </summary>
    /// <param name="chk">Proximo Checkpoint</param>
    private void CalculateNextPoint(Checkpoint chk)
    {
        //_nextDestinationPoint = chk.GetRandomPositionFromNode();
        _nextDestinationPoint = Vector3.zero;
        var randomPoint = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        randomPoint = chk.transform.TransformPoint(randomPoint * 0.5f);
        randomPoint.y = 200;
        Ray ray = new Ray(randomPoint, -Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,Mathf.Infinity))
        {
            if (hit.collider.gameObject.layer == K.LAYER_GROUND)
            {
                _nextDestinationPoint = hit.point + Vector3.up;
                return;
            }
        }
        CalculateNextPoint(chk);        
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
