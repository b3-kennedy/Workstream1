using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(gameObject);
        //Debug.Log(transform.parent);
        if (collision.gameObject.CompareTag("Player") && transform.parent.CompareTag("pickedUpCar") && !transform.parent.GetComponent<CarMovements>().parked)
        {
            collision.gameObject.GetComponent<OnCollidedWith>().Collided();
            Vector3 dir = (transform.position - collision.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-dir * (10 * GetComponent<Rigidbody>().velocity.magnitude) , ForceMode.Impulse);
        }
    }
}
