using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/SpeedBuff")]
public class SpeedBuff : PowerUp
{

    public float SpeedPlus = 30;

    public GameObject text;
    public override void Apply(GameObject go)
    {
        if (go.GetComponent<NewCarMovement>())
        {
            
            Debug.Log("power up effect activated");
            go.transform.GetComponent<NewCarMovement>().speed += SpeedPlus;
           
          
            
        }

    }

   

    public override void Cancel(GameObject gameObject)
    {
        gameObject.GetComponent<NewCarMovement>().speed -= SpeedPlus;
        Debug.Log("power up effect ended");
    }
}