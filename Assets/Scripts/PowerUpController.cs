using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpController : MonoBehaviour
{
    public PowerUp powerUp;

    bool activated = false;
    float timer = 7;

    GameObject carGO = null;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<NewCarMovement>() && !activated && other.GetComponent<CarMovements>().currentDriver)
        {   
            
            carGO = other.gameObject;
            powerUp.Apply(carGO);
            activated = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;

        }


    }
    private void Update()
    {
        if (timer > 0 && activated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
                timer = 20;
                if(carGO != null)
                {
                     powerUp.Cancel(carGO);
                }
                Destroy(gameObject);

            }

        }
    }
}
