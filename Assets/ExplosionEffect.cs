using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private float destroyTime = 2.5f;
    private AudioSource crashAudio;

    void Start()
    {
        crashAudio = GetComponent<AudioSource>();
        //audioSource = GetComponent<AudioSource>();
        //audioSource.Play();
        Debug.Log("explosion effect instantiated");
        gameObject.name = "explosion1";
        crashAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
