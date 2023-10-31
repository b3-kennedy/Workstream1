using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Control : MonoBehaviour
{


    public float moveInput;
    public float turnInput;


    public Rigidbody sphereRB;

    public float fwdspeed;
    public float revSpeed;
    public float turnSpeed;

    public bool disable = false;
    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disable)
        {
            moveInput = Input.GetAxisRaw("Vertical1");
            // moveInput = Input.GetAxis("Fire1");
            turnInput = Input.GetAxisRaw("Horizontal1");
            moveInput *= moveInput > 0 ? fwdspeed : revSpeed;




            transform.position = sphereRB.transform.position;


            float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical1");
            transform.Rotate(0, newRotation, 0, Space.World);
        }
        else
        {

        }

        

    }




    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }
}
