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

    public TMP_Text scoreTextMesh;
    Rigidbody rigidbody;
    CarMovements carMovement;


    public Gamepad pad;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        originalPosition = transform.position;
        string objectName = gameObject.name.Split('r')[1];
        //playerIndex = int.Parse(objectName) - 1;
        audioSource = GetComponent<AudioSource>();


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


        ////left
        //if (playerIndex == 0)
        //{
        //    //controls.Enable();
        //    controls.Player.Move.performed += OnMovementPerformed;
        //    controls.Player.Move.canceled += OnMovementCanceled;
        //}
        ////right
        //else if (playerIndex == 1)
        //{
            
        //    //controls.Enable();

        //    controls.Player.Move.performed += OnMovementPerformed;
        //    controls.Player.Move.canceled += OnMovementCanceled;
        //}
        ////left
        //else if (playerIndex == 2)
        //{
        //    //controls.Enable();

        //    controls.Player.Move.performed += OnMovementPerformed;
        //    controls.Player.Move.canceled += OnMovementCanceled;
        //}
        ////right
        //else if (playerIndex == 3)
        //{
        //    //controls.Enable();

        //    controls.Player.Move.performed += OnMovementPerformed;
        //    controls.Player.Move.canceled += OnMovementCanceled;
        //}
        //else if (playerIndex == 4)
        //{
        //    //controls.Enable();

        //    controls.Player.Move.performed += OnMovementPerformed;
        //    controls.Player3.Move.canceled += OnMovementCanceled;
        //}
        //else if (playerIndex == 5)
        //{
        //    //controls.Enable();

        //    controls.Player3.Move2.performed += OnMovementPerformed;
        //    controls.Player3.Move2.canceled += OnMovementCanceled;
        //}
        //else if (playerIndex == 6)
        //{
        //    //controls.Enable();

        //    controls.Player4.Move.performed += OnMovementPerformed;
        //    controls.Player4.Move.canceled += OnMovementCanceled;
        //}
        //else if (playerIndex == 7)
        //{
        //    //controls.Enable();

        //    controls.Player4.Move2.performed += OnMovementPerformed;
        //    controls.Player4.Move2.canceled += OnMovementCanceled;
        //}

    }

    void OnDisable()
    {

        controls.Player.Move.performed -= OnMovementPerformed;
        controls.Player.Move.canceled -= OnMovementCanceled;


        //if (playerIndex == 0)
        //{
        //    controls.Disable();
        //    controls.Player.Move.performed -= OnMovementPerformed;
        //    controls.Player.Move.canceled -= OnMovementCanceled;
        //}
        //else if (playerIndex == 1)
        //{
        //    controls.Player.Move2.performed -= OnMovementPerformed;
        //    controls.Player.Move2.canceled -= OnMovementCanceled;
        //}
        ////left
        //else if (playerIndex == 2)
        //{
        //    controls.Player2.Move.performed -= OnMovementPerformed;
        //    controls.Player2.Move.canceled -= OnMovementCanceled;
        //}
        ////right
        //else if (playerIndex == 3)
        //{
        //    controls.Player2.Move2.performed -= OnMovementPerformed;
        //    controls.Player2.Move2.canceled -= OnMovementCanceled;
        //}
        //else if (playerIndex == 4)
        //{
        //    controls.Player3.Move.performed -= OnMovementPerformed;
        //    controls.Player3.Move.canceled -= OnMovementCanceled;
        //}
        //else if (playerIndex == 5)
        //{
        //    controls.Player3.Move2.performed -= OnMovementPerformed;
        //    controls.Player3.Move2.canceled -= OnMovementCanceled;
        //}
        //else if (playerIndex == 6)
        //{
        //    controls.Player4.Move.performed -= OnMovementPerformed;
        //    controls.Player4.Move.canceled -= OnMovementCanceled;
        //}
        //else if (playerIndex == 7)
        //{
        //    controls.Player4.Move2.performed -= OnMovementPerformed;
        //    controls.Player4.Move2.canceled -= OnMovementCanceled;
        //}


    }


    void Update()
    {

        Vector2 stickL = pad.leftStick.ReadValue();
        Vector2 stickR = pad.rightStick.ReadValue();

        if (GetComponent<PlayerInput>().currentControlScheme == "GamePadLeft" && new Vector2(stickL.x,stickL.y) != Vector2.zero)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i] == pad)
                {
                    //scoreTextMesh.text = "" + score;
                    Vector3 movement = new Vector3(stickL.x, 0f, stickL.y);
                    //rigidbody.velocity =  movement;
                    transform.Translate(movement * moveSpeed * Time.deltaTime);


                }
            }
        }
        else if(GetComponent<PlayerInput>().currentControlScheme == "GamePadRight" && new Vector2(stickR.x, stickR.y) != Vector2.zero)
        {
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i] == pad)
                {
                    //scoreTextMesh.text = "" + score;
                    Vector3 movement = new Vector3(stickR.x, 0f, stickR.y);
                    //rigidbody.velocity =  movement;
                    transform.Translate(movement * moveSpeed * Time.deltaTime);


                }
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Cars") && other.gameObject.CompareTag("freeCar"))
        {

            if (Camera.main.GetComponent<MultipleTargetCamera>())
            {
                for (int i = 0; i < Camera.main.GetComponent<MultipleTargetCamera>().targets.Count; i++)
                {
                    if (Camera.main.GetComponent<MultipleTargetCamera>().targets[i].gameObject == gameObject)
                    {
                        Camera.main.GetComponent<MultipleTargetCamera>().targets[i] = other.transform;
                    }
                }
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




        // Enable car input
        carMovement = currentCar.GetComponent<CarMovements>();

        if (carMovement != null)
        {
            carMovement.EnableInput(playerIndex, gameObject, originalPosition);
            carMovement.OnSwitch();
            carMovement.OnCarParked += UpdateScore;

        }
    }

    public void ExitCar()
    {
        gameObject.SetActive(true);

      
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
