
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointParkingSpot : TimedEvent
{
    public Transform parkingSpot;
    public GameObject doublePointsIndicator;
    public Material goldMat;
    Material normalMat;

    private void Awake()
    {
        ended.AddListener(TimerEnded);
    }

    // Start is called before the first frame update
    void Start()
    {
        

        int num = Random.Range(0, RandomEventController.Instance.parkingSpots.Count);
        parkingSpot = RandomEventController.Instance.parkingSpots[num].transform;

        normalMat = parkingSpot.GetChild(1).GetComponent<MeshRenderer>().material;

        parkingSpot.GetChild(1).GetComponent<MeshRenderer>().material = goldMat;

        parkingSpot.GetComponentInChildren<ParkingSpot>().doublePoints = true;
    }

    void TimerEnded()
    {
        parkingSpot.GetComponentInChildren<ParkingSpot>().doublePoints = false;
        parkingSpot.GetChild(1).GetComponent<MeshRenderer>().material = normalMat;


    }

    // Update is called once per frame
    void Update()
    {
        base.Timer();
    }
}
