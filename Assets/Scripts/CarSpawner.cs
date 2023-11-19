using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
     public GameObject carPrefab;

     private List<GameObject> cars = new List<GameObject>();

    void Start()
    {
        for (var i = 0; i < 8; i++)
        {
            SpawnCar(i);
        }
    }

   void SpawnCar(int i){
        GameObject car = Instantiate(carPrefab,new Vector3( 68 - 14*i ,0 ,-70), Quaternion.identity);
        car.name = "Car-" + i;
        cars.Add(car);
   }

   public void onCarPickedUp(int index)
   {
    SpawnCar(index);
   }
}
