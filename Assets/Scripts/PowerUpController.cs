using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PowerUp powerUp;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        //powerUp.Apply(other.gameObject);

        //Destroy(gameObject);
    }
}
