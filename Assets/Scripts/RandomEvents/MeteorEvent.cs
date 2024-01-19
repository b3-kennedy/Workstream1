using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorEvent : StrikeEvent
{

    public GameObject meteor;

    [Header("Bounds")]
    public Transform topLeft;
    public Transform topRight;
    public Transform bottomLeft;
    public Transform bottomRight;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(meteor, new Vector3(Random.Range(topLeft.position.x, topRight.position.x), 200, Random.Range(topLeft.position.z, bottomLeft.position.z)), Quaternion.identity);
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
