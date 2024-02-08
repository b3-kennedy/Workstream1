using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    [HideInInspector] public float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<NewCarMovement>())
        {
            GetComponent<NewCarMovement>().speed *= 5;
        }

        Destroy(this, destroyTime);
    }

    private void OnDestroy()
    {
        if (GetComponent<NewCarMovement>())
        {
            GetComponent<NewCarMovement>().speed /= 5;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
