using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpCar : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject carPrefab;  

    private GameObject currentCar;
    private bool isControllingCar = false;

    private Vector2 movementInput;
    private Vector3 originalPosition; // Store the original position of the player

    public bool carParked = false;
    public int score = 0;

    public  PlayerControls controls;

    void Start()
    {
        // Store the original position of the player when the scene starts
        originalPosition = transform.position;
        }

    void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Move.performed += OnMovementPerformed;
        controls.Player.Move.canceled += OnMovementCanceled;
    }

    void OnDisable()
    {
        controls.Disable();

        controls.Player.Move.performed -= OnMovementPerformed;
        controls.Player.Move.canceled -= OnMovementCanceled;
    }

    void Update()
    {
        if (!isControllingCar)
        {
            Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
        else
        {
            
            float carHorizontal = movementInput.x;
            float carVertical = movementInput.y;
            Debug.Log(carHorizontal+" "+carVertical);
            currentCar.transform.Translate(new Vector3(carHorizontal, 0f, carVertical) * moveSpeed * Time.deltaTime);
        }
        if (currentCar != null && currentCar.GetComponent<CarObject>().isParked)
        {
            carParked = true;
            SwitchToPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cars") && !isControllingCar)
        {
            SwitchToCar(other.gameObject);
            
        }
    }

private void SwitchToPlayer()
{
    Debug.Log("switch to meee");
    transform.position = originalPosition;
    gameObject.SetActive(true);
    // currentCar = car;
    // currentCar.SetActive(false);
    ExitCar();
}

    private void SwitchToCar(GameObject car)
    {
      
        gameObject.SetActive(false);

    
        currentCar = car;
        currentCar.SetActive(true);



        isControllingCar = true;

                // Enable car input
        CarMovements carMovement = currentCar.GetComponent<CarMovements>();
        if (carMovement != null)
        {
            carMovement.EnableInput();
        }
    }

    public void ExitCar()
    {
        // currentCar.SetActive(false);
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
