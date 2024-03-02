using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private GameObject currentCar;

    private Vector2 movementInput;
    Vector3 movement;
    private Vector3 originalPosition; 

    public bool carParked = false;
    public int score = 0;

    public Player1Input controls;

    public int playerIndex;

    public enum ControllerSide { LEFT, RIGHT };
    public ControllerSide controllerSide;

    int deviceIndex;
    public string controlScheme;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    bool dash;
    bool canDash = true;
    float dashTimer;
    public float dashCooldown;

    public float dashForce;
    public TMP_Text scoreTextMesh;
    Rigidbody rb;
    
    PlayerInput playerInput;

    public CapsuleCollider triggerCollider;
    public CapsuleCollider normalCollider;

    public bool inCar;

    [HideInInspector] public FollowPlayer playerNumberText;


    public Gamepad pad;
    private bool Oncar = false;

    [Header("Car Controls")]
    float brake;
    Vector3 carMove;
    float horizontal;
    float vertical;
    CarMovements carMovement;
    NewCarMovement newCarMovement;
    Rigidbody carRb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();


        originalPosition = transform.position;
        string objectName = gameObject.name.Split('r')[1];
        //playerIndex = int.Parse(objectName) - 1;
        audioSource = GetComponent<AudioSource>();

        playerInput = GetComponent<PlayerInput>();


        //deviceIndex = context.control.device.device.deviceId;



        //Debug.Log(pad);


    }

    private void UpdateScore()
    {
        score += carMovement.parkingScoreEarned;

        scoreTextMesh.text = score.ToString();

    }

    void SlamDoor()
    {

        audioSource.PlayOneShot(audioSource.clip, volume);


    }

    public void OnSpawn() 
    {

        GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme);

        controls = new Player1Input();
        //controls.devices = new[] {pad};

        Debug.Log(GetComponent<PlayerInput>().currentControlScheme);
        

        controls.Enable();

        controls.Player.Move.performed += OnMovementPerformed;
        controls.Player.Move.canceled += OnMovementCanceled;

        
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {
        controls.Disable();
        //controls.Player.Move.performed -= OnMovementPerformed;
        //controls.Player.Move.canceled -= OnMovementCanceled;


    }


    void Update()
    {

        if(pad != null)
        {
            Vector2 stickL = pad.leftStick.ReadValue();
            Vector2 stickR = pad.rightStick.ReadValue();

            if (playerInput.currentControlScheme == "GamePadLeft")
            {

                if (!inCar)
                {
                    dash = pad.leftShoulder.isPressed;
                    //scoreTextMesh.text = "" + score;
                    movement = new Vector3(stickL.x, 0f, stickL.y);
                    //rigidbody.velocity =  movement;
                    transform.Translate(movement * moveSpeed * Time.deltaTime);
                }
                else
                {
                    brake = pad.leftTrigger.ReadValue();

                    if (pad.dpad.down.isPressed)
                    {
                        carMovement.DisableInput();
                    }

                    carMove = new Vector3(stickL.x, 0, stickL.y);


                    //carSmoke.Play();
                    //carSmoke2.Play();

                    horizontal = stickL.x;
                    vertical = stickL.y;

                    carMovement.moveInput = carRb.velocity.magnitude;

                    float rot = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

                    if (carMove != Vector3.zero)
                    {
                        Vector3 newAngle = new Vector3(0, rot, 0);
                        currentCar.transform.rotation = Quaternion.Lerp(currentCar.transform.rotation, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z), 
                            Time.deltaTime * newCarMovement.rotSpeed);
                    }
                }

            }
            else if (playerInput.currentControlScheme == "GamePadRight")
            {
                if (!inCar)
                {
                    dash = pad.rightShoulder.isPressed;
                    //scoreTextMesh.text = "" + score;
                    movement = new Vector3(stickR.x, 0f, stickR.y);
                    //rigidbody.velocity =  movement;
                    transform.Translate(movement * moveSpeed * Time.deltaTime);
                }
                else
                {
                    brake = pad.rightTrigger.ReadValue();

                    if (pad.buttonSouth.isPressed)
                    {
                        carMovement.DisableInput();
                    }

                    carMove = new Vector3(stickR.x, 0, stickR.y);


                    //carSmoke.Play();
                    //carSmoke2.Play();

                    horizontal = stickR.x;
                    vertical = stickR.y;

                    carMovement.moveInput = carRb.velocity.magnitude;

                    float rot = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

                    if (carMove != Vector3.zero)
                    {
                        Vector3 newAngle = new Vector3(0, rot, 0);
                        currentCar.transform.rotation = Quaternion.Lerp(currentCar.transform.rotation, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z),
                            Time.deltaTime * newCarMovement.rotSpeed);
                    }
                }

            }

        }


        if (!canDash)
        {
            dashTimer += Time.deltaTime;
            if(dashTimer >= dashCooldown)
            {
                canDash = true;
                dashTimer = 0;
            }
        }


    }

    private void FixedUpdate()
    {
        if (dash && canDash)
        {
            rb.AddForce(movement * dashForce, ForceMode.Impulse);
            canDash = false;
        }

        if (inCar)
        {
            currentCar.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(currentCar.GetComponent<Rigidbody>().velocity, currentCar.GetComponent<NewCarMovement>().maxSpeed);

                if (carMove == Vector3.zero)
                {
                    currentCar.GetComponent<Rigidbody>().velocity = Vector3.Lerp(currentCar.GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 0.5f);

                    //carSmoke.Stop();
                    //carSmoke2.Stop();


                }

                if (brake == 1)
                {
                    currentCar.GetComponent<Rigidbody>().velocity = Vector3.Lerp(currentCar.GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * currentCar.GetComponent<NewCarMovement>().breakPower);
                }
                else
                {
                    currentCar.GetComponent<Rigidbody>().AddForce(carMove * currentCar.GetComponent<NewCarMovement>().speed, ForceMode.Acceleration);
                }

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Cars") && other.gameObject.CompareTag("freeCar"))
        {

            if (Camera.main.GetComponent<MultipleTargetCamera>())
            {
                Debug.Log("cam");
                for (int i = 0; i < Camera.main.GetComponent<MultipleTargetCamera>().targets.Count; i++)
                {
                    if (Camera.main.GetComponent<MultipleTargetCamera>().targets[i].gameObject == gameObject)
                    {
                        Camera.main.GetComponent<MultipleTargetCamera>().targets[i] = other.transform;
                    }
                }

                //other.GetComponent<NewCarMovement>().playerController = this;
                
            }

            SwitchToCar(other.gameObject);
            other.gameObject.tag = "pickedUpCar";

        }
    }



    private void SwitchToCar(GameObject car)
    {
        SlamDoor();

        normalCollider.enabled = false;
        triggerCollider.enabled = false;

        inCar = true;

        transform.localPosition = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        
        

        currentCar = car;
        currentCar.SetActive(true);

        playerNumberText.target = car.transform;
        car.GetComponent<NewCarMovement>().SetupCar();
        car.GetComponent<NewCarMovement>().playerNumberText = playerNumberText;
        car.GetComponent<NewCarMovement>().playerController = this;
        car.GetComponent<NewCarMovement>().controlScheme = controlScheme;

        carRb = currentCar.GetComponent<Rigidbody>();
        newCarMovement = currentCar.GetComponent<NewCarMovement>();

        // Enable car input
        carMovement = currentCar.GetComponent<CarMovements>();
        if (carMovement != null)
        {
            Oncar = true;
            carMovement.EnableInput(playerIndex, gameObject, originalPosition);
            carMovement.OnSwitch();
            carMovement.OnCarParked += UpdateScore;

        }
    }

    public void ExitCar()
    {
        gameObject.SetActive(true);
        Oncar = false;
      
    }

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (Gamepad.all[i] == pad)
            {
                movementInput = context.ReadValue<Vector2>();
            }
        }
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
}
