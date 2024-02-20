
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointParkingSpot : TimedEvent
{
    public Transform parkingSpot;
    public GameObject doublePointsIndicator;

    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, RandomEventController.Instance.parkingSpots.Count);
        parkingSpot = RandomEventController.Instance.parkingSpots[num].transform;

        transform.SetParent(parkingSpot);
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        base.Timer();
    }
}
