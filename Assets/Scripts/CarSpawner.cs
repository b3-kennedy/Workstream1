using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;

  
    
    [SerializeField]  public List<CarObject> cars = new List<CarObject>();
    public float spawnHeight = -70f; 
    public float moveUpDuration = 0.25f;

    private int totalCount =1;

    void Start()
    {
        for (var i = 0; i < 8; i++)
        {
            StartCoroutine(SpawnCar(i));
        }
    }

     IEnumerator SpawnCar(int i)
    {
        // GameObject car = Instantiate(carPrefab, new Vector3(60 - 14 * i, 0, -80), Quaternion.identity);
        GameObject car = Instantiate(carPrefab, new Vector3(60 - 14 * i, 0, -70), Quaternion.identity);
        car.transform.position = new Vector3(60 - 14 * i, 0, -80);
        car.name = "Car" + (totalCount);
        totalCount +=1;
        
        CarObject carObj = new CarObject(i, Color.blue, car.name, car);
        cars.Add(carObj);
         float elapsedTime = 0f;
         int moveInSpeed = 10;
        // Vector3 start = car.transform.position;
        Vector3 target = new Vector3(60 - 14 * i, 0, -70);

        while (car.transform.position.z <-70)
        {
            // car.transform.position = Vector3.Lerp(start,target, elapsedTime / moveUpDuration);
            Vector3 movement = transform.forward * moveInSpeed* Time.deltaTime;
            car.transform.Translate(movement, Space.World);







            //sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
           // car.GetComponent<Rigidbody>().AddForce(transform.forward * moveInSpeed, ForceMode.Acceleration);












            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // car.transform.position = target;
    }





    IEnumerator waiter(CarObject pickedCar)
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(SpawnCar(pickedCar.carIndex));
    }

    public void OnCarPickedUp(CarObject pickedCar)
    {
        if (cars.Contains(pickedCar))
            {
                
                cars.Remove(pickedCar);
                

                StartCoroutine(waiter(pickedCar));
            
            }
    }
   public CarObject GetCarObject(GameObject gameObj )
    {
        // Debug.Log("getting CarObject: "+gameObj.ToString());
        
        CarObject carObject = cars.Find(car => car.carObject == gameObj);
        
  
            return carObject; 
    }
    
    
}
