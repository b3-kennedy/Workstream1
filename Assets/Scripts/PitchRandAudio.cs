using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandAudio : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randPitch(AudioSource audioSource)
    {
        float rand = Random.Range(0.8f, 1.2f);
        audioSource.pitch = rand;
        //if (audioSource != null && rand != audioSource.pitch) 
        //{ 
        //    audioSource.pitch = rand;
        //}
        //else
        //{
        //    randPitch(audioSource);
        //}
    }
}