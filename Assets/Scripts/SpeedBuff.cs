using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/SpeedBuff")]
public class SpeedBuff : PowerUp
{

    public float SpeedPlus = 30;

    public GameObject text;
    public override void Apply(GameObject go)
    {
        if (go.GetComponentInParent<NewCarMovement>())
        {
            
            Debug.Log("power up effect activated");
            go.transform.GetComponentInParent<NewCarMovement>().speed += SpeedPlus;
            //var txt = Instantiate(text, new Vector3(go.transform.position.x, 2, go.transform.position.z), Quaternion.Euler(90, 0, 0), go.transform);
            //go.GetComponent<TextMeshPro>().text = "+Speed";
          
            
        }

    }

   

    public override void Cancel(GameObject gameObject)
    {
        gameObject.GetComponentInParent<NewCarMovement>().speed -= SpeedPlus;
        Debug.Log("power up effect ended");
    }
}
