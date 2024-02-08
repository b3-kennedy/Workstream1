using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewCarMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector3 move;
    Rigidbody rb;
    public float speed;
    public float rotSpeed;
    public float breakPower;
    float brake;
    CarMovements movements;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movements = GetComponent<CarMovements>();
    }

    // Update is called once per frame
    void Update()
    {

        if (movements.currentDriver != null)
        {
            if (movements.isInputEnabled && movements.currentDriver.GetComponent<PlayerController>().pad != null)
            {


                Vector2 stickL = movements.currentDriver.GetComponent<PlayerController>().pad.leftStick.ReadValue();
                Vector2 stickR = movements.currentDriver.GetComponent<PlayerController>().pad.rightStick.ReadValue();
                

                if (GetComponent<PlayerInput>().currentControlScheme == "GamePadLeft")
                {
                    if (movements.currentDriver.GetComponent<PlayerController>().pad.dpad.down.isPressed)
                    {
                        movements.DisableInput();
                    }

                    for (int i = 0; i < Gamepad.all.Count; i++)
                    {
                        if (Gamepad.all[i] == movements.currentDriver.GetComponent<PlayerController>().pad)
                        {

                            brake = movements.currentDriver.GetComponent<PlayerController>().pad.leftTrigger.ReadValue();

                            move = new Vector3(stickL.x, 0, stickL.y);
                            horizontal = stickL.x;
                            vertical = stickL.y;

                        }
                    }
                }
                else if (GetComponent<PlayerInput>().currentControlScheme == "GamePadRight")
                {

                    

                    for (int i = 0; i < Gamepad.all.Count; i++)
                    {
                        if (Gamepad.all[i] == movements.currentDriver.GetComponent<PlayerController>().pad)
                        {
                            if (movements.currentDriver.GetComponent<PlayerController>().pad.aButton.isPressed)
                            {
                                movements.DisableInput();
                            }


                            brake = movements.currentDriver.GetComponent<PlayerController>().pad.rightTrigger.ReadValue();

                            move = new Vector3(stickR.x, 0, stickR.y);
                            horizontal = stickR.x;
                            vertical = stickR.y;
                            


                        }
                    }
                }
            }

            //horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");
            //move.x = horizontal;
            //move.z = vertical;

            movements.moveInput = rb.velocity.magnitude;

            float rot = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

            if (move != Vector3.zero)
            {
                Vector3 newAngle = new Vector3(0, rot, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z), Time.deltaTime * rotSpeed);
            }

            
            

        }
    }

    private void FixedUpdate()
    {
        if(movements.currentDriver != null) 
        {
            if(brake == 1)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * breakPower);
            }
            else
            {
                rb.AddForce(move * speed, ForceMode.Acceleration);
            }
            
        }
        

        
    }
}
