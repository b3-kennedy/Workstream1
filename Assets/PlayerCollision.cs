using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && transform.parent.CompareTag("pickedUpCar"))
        {
            collision.gameObject.GetComponent<OnCollidedWith>().Collided();
            Vector3 dir = (transform.position - collision.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-dir * (10 * GetComponent<Rigidbody>().velocity.magnitude) , ForceMode.Impulse);
        }
    }
}
