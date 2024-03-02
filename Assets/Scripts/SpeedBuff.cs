using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/SpeedBuff")]
public class SpeedBuff : PowerUp
{

    public float SpeedPlus = 30;
    public override void Apply(GameObject go)
    {
        if (go.GetComponentInParent<NewCarMovement>())
        {
            
            Debug.Log("power up effect activated");
            go.transform.GetComponentInParent<NewCarMovement>().speed += SpeedPlus;
            
        }

    }

   

    public override void Cancel(GameObject gameObject)
    {
        gameObject.GetComponentInParent<NewCarMovement>().speed -= SpeedPlus;
        Debug.Log("power up effect ended");
    }
}
