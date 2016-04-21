using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IAController : MonoBehaviour
{
    public Node node;
    public GameObject hpBarContainer;
    public RawImage hpBarImage;
	public GameObject remains;

    private float _maxHp, _currentHp, _speed;
    private Vector3 aux;
    private void Start()
    {
        _maxHp = K.IA_MAX_HP;
        _currentHp = _maxHp;
        aux = hpBarImage.transform.localScale;
        _speed = K.IA_MAX_SPEED;
    }
    private void Update()
    {
        hpBarContainer.transform.LookAt(Camera.main.transform.position);
        aux.x = _currentHp / _maxHp;
        hpBarImage.transform.localScale = aux;
        if (Vector3.Distance(transform.position,node.transform.position) < 20)
        {
            node = node.GetNextPosition();
        }
        transform.forward = Vector3.Slerp(transform.forward, node.transform.position - transform.position, 5 * Time.deltaTime);
		if (_speed < 100) {
			//_speed *= 1.005f;
		}
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

	public void Damage(float d)
	{
		print ("entre");
		_currentHp -= d;

		if (_currentHp <= 0) 
		{
			Instantiate (remains, transform.position, transform.rotation);
			Destroy (this.gameObject);
		}
	}
}
