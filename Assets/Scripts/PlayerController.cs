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
    CarMovements carMovement;
    PlayerInput playerInput;

    [HideInInspector] public FollowPlayer playerNumberText;


    public Gamepad pad;
    private bool Oncar = false;


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
        controls.devices = new[] {pad};

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

            
                dash = pad.leftShoulder.isPressed;
                //scoreTextMesh.text = "" + score;
                movement = new Vector3(stickL.x, 0f, stickL.y);
                //rigidbody.velocity =  movement;
                transform.Translate(movement * moveSpeed * Time.deltaTime);
            }
            else if (playerInput.currentControlScheme == "GamePadRight")
            {
               
                dash = pad.rightShoulder.isPressed;
                //scoreTextMesh.text = "" + score;
                movement = new Vector3(stickR.x, 0f, stickR.y);
                //rigidbody.velocity =  movement;
                transform.Translate(movement * moveSpeed * Time.deltaTime);
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

        gameObject.SetActive(false);


        currentCar = car;
        currentCar.SetActive(true);

        playerNumberText.target = car.transform;
        car.GetComponent<NewCarMovement>().SetupCar();
        car.GetComponent<NewCarMovement>().playerNumberText = playerNumberText;
        car.GetComponent<NewCarMovement>().playerController = this;
        car.GetComponent<NewCarMovement>().controlScheme = controlScheme;



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
