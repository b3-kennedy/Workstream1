using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    
    [SerializeField]  public List<CarObject> cars = new List<CarObject>();
    public float spawnHeight = -70f; 
    public float moveUpDuration = 0.25f;

    void Start()
    {
        for (var i = 0; i < 8; i++)
        {
            StartCoroutine(SpawnCar(i));
        }
    }

     IEnumerator SpawnCar(int i)
    {
        GameObject car = Instantiate(carPrefab, new Vector3(60 - 14 * i, 0, -70), Quaternion.identity);
        car.name = "Car" + (cars.Count);
        
        CarObject carObj = new CarObject(i, Color.blue, car.name, car);
        cars.Add(carObj);
         float elapsedTime = 0f;
        // Vector3 start = car.transform.position;
        // Vector3 target = new Vector3(60 - 14 * i, 0, 100);

        while (elapsedTime < moveUpDuration)
        {
            // car.transform.position = Vector3.Lerp(start,target, elapsedTime / moveUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // // Ensure the car is at the target position
        // car.transform.position = target;
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
    }

    public void OnCarPickedUp(CarObject pickedCar)
    {
        if (cars.Contains(pickedCar))
            {
                
                cars.Remove(pickedCar);

                StartCoroutine(waiter());
                // StartCoroutine(SpawnCar(pickedCar.carIndex));
          
            }
    }
   public CarObject GetCarObject(GameObject gameObj )
    {
        Debug.Log("getting CarObject: "+gameObj.ToString());
        
        CarObject carObject = cars.Find(car => car.carObject == gameObj);
        
        //     Debug.Log("Found CarObject: " + carObject.ToString());
            return carObject; 
    }
    
}
