using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedEvents : MonoBehaviour
{

    public static ActivatedEvents Instance;
    public List<EventListItem> events;

    private void Awake()
    {
        Instance = this;
    }


}
