using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    
    private AudioSource crashAudio;

    void Start()
    {
        
        Debug.Log("explosion effect instantiated");
        gameObject.name = "explosion";
        crashAudio = GetComponent<AudioSource>();
        crashAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
