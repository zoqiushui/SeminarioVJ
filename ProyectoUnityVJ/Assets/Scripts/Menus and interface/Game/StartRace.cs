using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class StartRace : MonoBehaviour
{
    private Text _countTxt;
    public int countMax;
    public string lastString;
    private Animator _animator;
    public AudioClip sCountDown;
    public AudioClip sFinalCountDown;
    public AudioClip sStartRobot;
    private AudioSource _channel;
    public AudioSource robotChannel;
    private float count;
	void Start ()
    {
        _countTxt = GetComponentInChildren<Text>();
        GameManager.disableShoot = true;
        _animator = GetComponent<Animator>();
        _channel = GetComponent<AudioSource>();
	}
    IEnumerator StartCounter()
    {
        for (int countDown = countMax; countDown >= 0; countDown--)
        {
          //  print(countDown);
            if (countDown == 0)
            {
                _countTxt.text = lastString;
                GameManager.disableShoot = false;
                _animator.SetBool("exitDrone", true);
                _channel.clip = sFinalCountDown;
                _channel.Play();
                robotChannel.Play();
                break;
            }
            _countTxt.text = countDown.ToString();
            _channel.clip = sCountDown;
            _channel.Play();
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnEventStart()
    {
        if (count == 0) StartCoroutine(StartCounter());
        else return;
        count++;
    }

    private void OnEventEnd()
    {
        Destroy(gameObject);
    }


}
