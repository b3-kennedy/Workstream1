using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEvent : StrikeEvent
{

    public float lightningEffectDuration;
    LineRenderer lr;


    // Start is called before the first frame update
    void Start()
    {

        lr = GetComponent<LineRenderer>();

        int num = Random.Range(0, RandomEventController.Instance.drivableCars.Count);
        RandomEventController.Instance.drivableCars[num].AddComponent<LightningEffect>();
        RandomEventController.Instance.drivableCars[num].GetComponent<LightningEffect>().destroyTime = lightningEffectDuration;

        lr.SetPosition(1, RandomEventController.Instance.drivableCars[num].transform.position);

        Destroy(gameObject, 0.3f);
    }


}
