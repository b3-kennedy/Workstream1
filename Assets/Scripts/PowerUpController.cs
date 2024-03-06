using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpController : MonoBehaviour
{
    public PowerUp powerUp;

    bool activated = false;
    float timer = 7;

    GameObject carGO = null;

    public AudioSource powerUpCollectAudio;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<NewCarMovement>() && !activated && other.GetComponent<CarMovements>().currentDriver && 
            !other.GetComponent<CarMovements>().isShielded)
        {   
            
            carGO = other.gameObject;
            powerUp.Apply(carGO);
            activated = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;


            powerUpCollectAudio.Play();
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
