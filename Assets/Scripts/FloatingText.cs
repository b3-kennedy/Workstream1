using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float destroyTime = 2.5f;
    private Vector3 changeScale = new Vector3(-0.1f, 0, 0);

    public AudioSource audioSource;
        void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Destroy(gameObject,destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMesh>().fontSize -= 1;
    }
}
