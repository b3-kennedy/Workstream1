using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignIndex : MonoBehaviour
{
    [HideInInspector] public int index;

    public void SetIndex(int i)
    {
        Debug.Log("1");
        index = i;
    }

    public void OnCheckBoxChange()
    {
        ActivatedEvents.Instance.events[index].active = !ActivatedEvents.Instance.events[index].active;
    }
}
