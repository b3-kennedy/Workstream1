using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomEventController : MonoBehaviour
{

    public static RandomEventController Instance;


    public float timeToStart;


    public float minTimeBetweenEvents;
    public float maxTimeBetweenEvents;

    float betweenEventTime;
    float betweenEventTimer;



    float startTimer;

    public List<RandomEvent> events;

    public List<GameObject> parkingSpots;

    public List<GameObject> drivableCars;

    public Transform parkingSpaces1;
    public Transform parkingSpaces2;

    bool startEvents;


    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < parkingSpaces1.childCount; i++)
        {
            parkingSpots.Add(parkingSpaces1.GetChild(i).gameObject);
        }

        for (int i = 0; i < parkingSpaces2.childCount; i++)
        {
            parkingSpots.Add(parkingSpaces2.GetChild(i).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PreEventTime();
        if (startEvents)
        {
            StartEvents();
        }

    }

    void PickEvent()
    {
        Debug.Log("spawned event");
        int num = Random.Range(0, events.Count);
        Instantiate(events[num]);
    }

    void StartEvents()
    {
        betweenEventTimer += Time.deltaTime;
        if(betweenEventTimer >= betweenEventTime)
        {
            PickEvent();
            betweenEventTime = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
            Debug.Log("Next Event In: " + betweenEventTime.ToString());
            betweenEventTimer = 0;
        }
    }

    void PreEventTime()
    {
        if (startTimer < timeToStart)
        {
            
            startTimer += Time.deltaTime;
            if (startTimer >= timeToStart)
            {
                betweenEventTime = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
                startEvents = true;
                Debug.Log("Events Starting");
                Debug.Log("Next Event In: " + betweenEventTime.ToString());
            }
        }
    }
}
