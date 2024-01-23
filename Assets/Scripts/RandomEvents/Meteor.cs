using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float explosionRadius;
    public float explosionForce;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(-transform.up * 200, ForceMode.Impulse);
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in cols)
        {

            if (col.GetComponent<Rigidbody>() && col.gameObject.layer != 3)
            {
                Vector3 dir = col.transform.position - transform.position;

                Debug.Log(col.gameObject);
                col.GetComponent<Rigidbody>().AddForce(dir * explosionForce);
            }

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("pickedUpCar"))
        {
            other.collider.GetComponent<CarMovements>().ReduceLifeOnDamage(1000);
        }

        if(other.collider.gameObject.layer == 7)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        Explode();
    }
}
