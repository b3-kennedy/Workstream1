using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCar : MonoBehaviour
{
    public bool move = false;

    public GameObject car;
    
    void Update()
    {
        if(move && car)
        {
            gameObject.transform.position = car.transform.position;
        }
        if(car.GetComponent<CarMovements>().parked ) {
            Destroy(gameObject);
        }
    }
}
