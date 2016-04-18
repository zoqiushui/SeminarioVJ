using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IAController : MonoBehaviour
{
    public Node node;
    public Canvas hpBarCanvas;
    public RawImage hpBarImage;

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
        aux.x = _currentHp / _maxHp;
        hpBarImage.transform.localScale = aux;
        if (Vector3.Distance(transform.position,node.transform.position) < 2)
        {
            node = node.GetNextPosition();
        }
        transform.forward = Vector3.Slerp(transform.forward, node.transform.position - transform.position, 5 * Time.deltaTime);
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
