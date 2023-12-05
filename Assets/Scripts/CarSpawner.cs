using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject carPrefab2;
    public GameObject carPrefab3;
    public GameObject car;


    [SerializeField] public List<CarObject> cars = new List<CarObject>();
    public float spawnHeight = -70f;
    public float moveUpDuration = 0.25f;

    private int totalCount = 1;

    public int randomNumber;

    void Start()
    {
        for (var i = 0; i < 8; i++)
         // for (var i = 0; i < 1; i++)

            {
                StartCoroutine(SpawnCar(i));
        }
    }

    IEnumerator SpawnCar(int i)
    {
        Vector3 start = new Vector3(60 - 12 * i, 0, -90);
        Vector3 target = new Vector3(60 - 12 * i, 0, -75);


        randomNumber = Random.Range(1,11);
      
        if (randomNumber <= 5)
        {
             car = Instantiate(carPrefab, target, Quaternion.identity);

        }
        else if (randomNumber <=8)
        {
             car = Instantiate(carPrefab2, target, Quaternion.identity);

        }
        else 
        {

             car = Instantiate(carPrefab3, target, Quaternion.identity);

        }


        //GameObject car = Instantiate(carPrefab,target, Quaternion.identity);
        //car.transform.position = start;
        car.transform.position = target;
        car.name = "Car" + (totalCount);
        //car.transform.SetParent(transform);
        totalCount += 1;

        CarObject carObj = new CarObject(i, Color.blue, car.name, car);
        cars.Add(carObj);
        float elapsedTime = 0f;
        int moveInSpeed = 10;


        /* while (car.transform.position.z < target.z && elapsedTime<1.5f)
         {

             Vector3 movement = transform.forward * moveInSpeed * Time.deltaTime;
             car.transform.Translate(movement, Space.World);


             elapsedTime += Time.deltaTime;
             yield return null;
         }*/
        yield return null;
    }





    IEnumerator waiter(CarObject pickedCar)
    {
        yield return new WaitForSeconds(3);
        Debug.Log(pickedCar.carIndex);
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
    public CarObject GetCarObject(GameObject gameObj)
    {
        // Debug.Log("getting CarObject: "+gameObj.ToString());

        CarObject carObject = cars.Find(car => car.carObject == gameObj);


        return carObject;
    }


}
