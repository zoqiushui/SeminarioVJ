using UnityEngine;
using UnityEngine.UI;

public class IAController : Vehicle
{
    public GameObject hpBarContainer;
    public RawImage hpBarImage;
    public GameObject remains;

    private float _maxHp, _currentHp, _currentSpeed;
    public float _maxSpeed;
    private Checkpoint _nextCheckpoint;
    private Vector3 _aux, _nextDestinationPoint;

    private bool _isGrounded;
    private bool _isGroundedRamp;
    public float fallForce;

    protected override void Start()
    {
        base.Start();
        _maxHp = K.IA_MAX_HP;
        _currentHp = _maxHp;
        _aux = hpBarImage.transform.localScale;
//        _maxSpeed = K.IA_MAX_SPEED;
        lapCount = 0;
        _nextCheckpoint = _checkpointMananagerReference.checkpointsList[0];
        positionWeight = -Vector3.Distance(transform.position, _nextCheckpoint.transform.position);
        CalculateNextPoint(_nextCheckpoint);
        _currentSpeed = 1;
    }

    private void Update()
    {
        positionWeight = Vector3.Distance(transform.position, _nextCheckpoint.transform.position);
        UpdateHpBar();
        if (Vector3.Distance(transform.position, _nextDestinationPoint) < 15)
        {
            lapCount += _checkpointMananagerReference.checkpointValue;
            CalculateNextCheckpoint(_nextCheckpoint);
            CalculateNextPoint(_nextCheckpoint);
        }
        ApplyDrive();

        CheckIfGrounded();
        FallSpeed();
    }

    public override void GetInput(float _accel, float _brake,float _handbrake, float _steer, float _nitro)
    {
    }

    private void ApplyDrive()
    {
        CalculateSpeed();
        transform.forward = Vector3.Slerp(transform.forward, _nextDestinationPoint - transform.position, K.IA_TURN_SPEED * Time.deltaTime);
        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }

    private void CalculateSpeed()
    {
        if (lapCount + (_checkpointMananagerReference.checkpointValue) < ((JeepController)_gameManagerReference.playerReference).lapCount)
        {
            _currentSpeed += 0.15f;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 1, _maxSpeed);

        }
        else if (lapCount - (_checkpointMananagerReference.checkpointValue) > ((JeepController)_gameManagerReference.playerReference).lapCount)
        {
            _currentSpeed -= 0.3f;
            _currentSpeed = Mathf.Clamp(_currentSpeed, _maxSpeed / 3, _maxSpeed);
        }
        else
        {
            _currentSpeed += 0.1f;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 1, _maxSpeed);

        }
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
            _soundManagerReference.PlaySound(K.SOUND_CAR_DESTROY);
            NotifyObserver(K.OBS_MESSAGE_DESTROYED);
            Destroy(this.gameObject);
            Instantiate(remains, transform.position, transform.rotation);

        }
    }


    protected void CheckIfGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit hit;
        _isGrounded = false;
        _isGroundedRamp = false;
        if (Physics.Raycast(ray, out hit, 1))
        {
            if (hit.collider.gameObject.layer == K.LAYER_GROUND || hit.collider.gameObject.layer == K.LAYER_RAMP)
            {
                _isGrounded = true;
                if (hit.collider.gameObject.layer == K.LAYER_RAMP) _isGroundedRamp = true;
            }
        }
    }

    protected void FallSpeed()
    {
        if (!_isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(-Vector3.up * K.IA_FALLFORCE);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _nextDestinationPoint);
    }    
}
