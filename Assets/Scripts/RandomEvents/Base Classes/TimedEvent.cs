using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : RandomEvent
{
    public float activeTime;
    float timer;
    public bool active = true;


    public virtual void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= activeTime)
        {
            active = false;
            Destroy(gameObject);
            timer = 0;
        }
    }
}
