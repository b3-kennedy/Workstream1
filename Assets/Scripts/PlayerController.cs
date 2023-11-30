using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private GameObject currentCar;
    private bool isControllingCar = false;

    private Vector2 movementInput;
    private Vector3 originalPosition; // Store the original position of the player

    public bool carParked = false;
    public int score = 0;

    public PlayerControls controls;

    public int playerIndex;

    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    public TMP_Text scoreTextMesh;


    void Start()
    {
        originalPosition = transform.position;
        string objectName = gameObject.name.Split('r')[1];
        playerIndex = int.Parse(objectName) - 1;

        audioSource = GetComponent<AudioSource>();


    }
    void SlamDoor()
    {
       
        audioSource.PlayOneShot(audioSource.clip, volume);

      
    }

    void OnEnable()
    {
        controls = new PlayerControls();
       
        if (playerIndex == 0)
        {
 controls.Enable();
            controls.Player.Move.performed += OnMovementPerformed;
            controls.Player.Move.canceled += OnMovementCanceled;
        }
        else
        {
          
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


    }


    void Update()
    {
        scoreTextMesh.text = "" + score;
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
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



        isControllingCar = true;

        // Enable car input
        CarMovements carMovement = currentCar.GetComponent<CarMovements>();
        if (carMovement != null)
        {
            carMovement.EnableInput(playerIndex, gameObject, originalPosition);

        }
    }

    public void ExitCar()
    {
        gameObject.SetActive(true);

        isControllingCar = false;
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
