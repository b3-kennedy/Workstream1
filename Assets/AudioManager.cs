using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource gameMusic;

    public AudioClip[] musicOptoins;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMusic(int index)
    {
        gameMusic.clip = musicOptoins[index];
        gameMusic.Play();
    }

    public void ToggleMute()
    {
        gameMusic.mute = !gameMusic.mute;
    }
}
