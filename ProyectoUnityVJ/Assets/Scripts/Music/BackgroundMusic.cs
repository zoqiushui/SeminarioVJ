using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundMusic : MonoBehaviour
{
    public List<AudioClip> backgroundMusics;
    private AudioSource _channel;
    private int _count;
    void Awake()
    {
        _channel = GetComponent<AudioSource>();
    }
	void Start ()
    {
        _count = 0;
        _channel.clip = backgroundMusics[_count];
        _channel.Play();
	}
	
	void Update ()
    {
        if (!_channel.isPlaying)
        {
            _count++;
            _channel.clip = backgroundMusics[_count];
            _channel.Play();
        }
        if (_count == backgroundMusics.Count - 1) _count = 0;
	}
}
