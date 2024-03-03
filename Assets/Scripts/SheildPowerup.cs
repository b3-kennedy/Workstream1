using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUp/Shield")]
public class SheildPowerup : PowerUp
{
    public override void Apply(GameObject gameObject)
    {
        if (gameObject.GetComponent<CarMovements>()) {

            gameObject.GetComponent<CarMovements>().isShielded = true;
            Debug.Log("shield activated");
        }
    }

    public override void Cancel(GameObject gameObject)
    {
        if (gameObject.GetComponent<CarMovements>())
        {
            gameObject.GetComponent<CarMovements>().isShielded = false;
            Debug.Log("shield deactivated");

        }
    }

}
