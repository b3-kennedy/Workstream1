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
    public float maxSpeed;
    float brake;
    CarMovements movements;
    string controlScheme;
    [HideInInspector] public FollowPlayer playerNumberText;

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
            var playerController = movements.currentDriver.GetComponent<PlayerController>();
            controlScheme = GetComponent<PlayerInput>().currentControlScheme;

            if (movements.isInputEnabled && playerController.pad != null)
            {


                Vector2 stickL = playerController.pad.leftStick.ReadValue();
                Vector2 stickR = playerController.pad.rightStick.ReadValue();
                

                if (controlScheme == "GamePadLeft")
                {

                    brake = playerController.pad.leftTrigger.ReadValue();

                    if (playerController.pad.dpad.down.isPressed)
                    {
                        movements.DisableInput();
                    }

                    move = new Vector3(stickL.x, 0, stickL.y);
                    horizontal = stickL.x;
                    vertical = stickL.y;
                }
                else if (controlScheme == "GamePadRight")
                {

                    brake = playerController.pad.rightTrigger.ReadValue();

                    if (playerController.pad.aButton.isPressed)
                    {
                        movements.DisableInput();
                    }

                    move = new Vector3(stickR.x, 0, stickR.y);
                    horizontal = stickR.x;
                    vertical = stickR.y;
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
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if(movements.currentDriver != null) 
        {

            if(move == Vector3.zero)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 0.5f);
            }
            
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
