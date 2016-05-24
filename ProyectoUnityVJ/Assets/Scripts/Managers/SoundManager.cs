using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    public Camera mainCamera;
    public int channelCount;
    private List<AudioSource> channels;
    public const int NO_CHANNEL = -1;

    void Awake()
    {

        /*if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Component.Destroy(this);
            Debug.LogError("SoundManager ERROR: hay dos instancias de la clase en la escena");
        }*/
    }
    // Use this for initialization
    void Start()
    {

        channels = new List<AudioSource>();

        for (int i = 0; i < channelCount; i++)
        {
            //GameObject.FindGameObjectWithTag("MainCamera")
            channels.Add(mainCamera.gameObject.AddComponent<AudioSource>());
        }

    }

    private int FindEmptyChannel()
    {
        for (int i = 0; i < channels.Count; i++)
        {
            if (channels[i].isPlaying == false)
            {
                return i;
            }
        }

        return NO_CHANNEL;
    }


    public void PlaySound(int id)
    {
        PlaySound(id, 1, false);
    }

    public void PlaySound(int id, float vol)
    {
        PlaySound(id, vol, false);
    }

    public void PlaySound(int id, bool loop)
    {
        PlaySound(id, 1, loop);
    }


    public void PlaySound(int id, float vol, bool loop)
    {

        int empty = FindEmptyChannel();

        if (empty == NO_CHANNEL)
        {
            Debug.LogWarning("SoundManager: No hay canales disponibles. Pruebe aumentar la variable channelCount.");
            return;
        }

        if (id > clips.Length)
        {
            Debug.LogError("SoundManager:  ERROR el id: " + id + " no existe en la definicion de los clips. Revise la variable clips.");
            return;
        }

        channels[empty].clip = clips[id];
        channels[empty].volume = vol;
        channels[empty].loop = loop;

        channels[empty].Play();
    }

    public void Stop()
    {
        for (int i = 0; i < channels.Count; i++)
        {
            channels[i].Stop();
        }
    }

    public void Stop(int channelID)
    {
        channels[channelID].Stop();
    }
}
