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

    public PlayerControls controls;

    public int playerIndex;

    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    public TMP_Text scoreTextMesh;
    Rigidbody rigidbody;
    CarMovements carMovement;


    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        originalPosition = transform.position;
        string objectName = gameObject.name.Split('r')[1];
        playerIndex = int.Parse(objectName) - 1;

        audioSource = GetComponent<AudioSource>();


    }

    private void UpdateScore()
    {
        score += carMovement.parkingScoreEarned;

        scoreTextMesh.text = score.ToString();
        ;
    }

    void SlamDoor()
    {

        audioSource.PlayOneShot(audioSource.clip, volume);


    }

    void OnEnable()
    {
        controls = new PlayerControls();
        //left
        if (playerIndex == 0)
        {
            controls.Enable();
            controls.Player.Move.performed += OnMovementPerformed;
            controls.Player.Move.canceled += OnMovementCanceled;
        }
        //right
        else if (playerIndex == 1)
        {
            controls.Enable();

            controls.Player.Move2.performed += OnMovementPerformed;
            controls.Player.Move2.canceled += OnMovementCanceled;
        }
        //left
        else if (playerIndex == 2)
        {
            controls.Enable();

            controls.Player2.Move.performed += OnMovementPerformed;
            controls.Player2.Move.canceled += OnMovementCanceled;
        }
        //right
        else if (playerIndex == 3)
        {
            controls.Enable();

            controls.Player2.Move2.performed += OnMovementPerformed;
            controls.Player2.Move2.canceled += OnMovementCanceled;
        }
        else if (playerIndex == 4)
        {
            controls.Player3.Move.performed += OnMovementPerformed;
            controls.Player3.Move.canceled += OnMovementCanceled;
        }
        else if (playerIndex == 5)
        {
            controls.Player3.Move2.performed += OnMovementPerformed;
            controls.Player3.Move2.canceled += OnMovementCanceled;
        }
        else if (playerIndex == 6)
        {
            controls.Player4.Move.performed += OnMovementPerformed;
            controls.Player4.Move.canceled += OnMovementCanceled;
        }
        else if (playerIndex == 7)
        {
            controls.Player4.Move2.performed += OnMovementPerformed;
            controls.Player4.Move2.canceled += OnMovementCanceled;
        }

    }

    void OnDisable()
    {

        if (playerIndex == 0)
        {
            controls.Disable();
            controls.Player.Move.performed -= OnMovementPerformed;
            controls.Player.Move.canceled -= OnMovementCanceled;
        }
        else if (playerIndex == 1)
        {
            controls.Player.Move2.performed -= OnMovementPerformed;
            controls.Player.Move2.canceled -= OnMovementCanceled;
        }
        //left
        else if (playerIndex == 2)
        {
            controls.Player2.Move.performed -= OnMovementPerformed;
            controls.Player2.Move.canceled -= OnMovementCanceled;
        }
        //right
        else if (playerIndex == 3)
        {
            controls.Player2.Move2.performed -= OnMovementPerformed;
            controls.Player2.Move2.canceled -= OnMovementCanceled;
        }
        else if (playerIndex == 4)
        {
            controls.Player3.Move.performed -= OnMovementPerformed;
            controls.Player3.Move.canceled -= OnMovementCanceled;
        }
        else if (playerIndex == 5)
        {
            controls.Player3.Move2.performed -= OnMovementPerformed;
            controls.Player3.Move2.canceled -= OnMovementCanceled;
        }
        else if (playerIndex == 6)
        {
            controls.Player4.Move.performed -= OnMovementPerformed;
            controls.Player4.Move.canceled -= OnMovementCanceled;
        }
        else if (playerIndex == 7)
        {
            controls.Player4.Move2.performed -= OnMovementPerformed;
            controls.Player4.Move2.canceled -= OnMovementCanceled;
        }


    }


    void Update()
    {
        scoreTextMesh.text = "" + score;
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        //rigidbody.velocity =  movement;
         transform.Translate(movement * moveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Cars") && other.gameObject.CompareTag("freeCar"))
        {
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
            carMovement.OnCarParked += UpdateScore;

        }
    }

    public void ExitCar()
    {
        gameObject.SetActive(true);

      
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
}
