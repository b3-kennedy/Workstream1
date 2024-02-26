using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] positions;

    float  timer=13;

    public GameObject[] powerups;
   
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = Random.Range(3, 10);
            
            SpawnPowerUp();
        }
    }
    void SpawnPowerUp()
    {
        int index = Random.Range(0, powerups.Length);
        int pos = Random.Range(0, positions.Length);

        Instantiate(powerups[index], positions[pos].gameObject.transform,false);
    }
}
