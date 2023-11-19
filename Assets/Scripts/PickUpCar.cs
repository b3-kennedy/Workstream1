using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpCar : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject carPrefab;  // Drag your "CarPrefab" onto this field in the Inspector

    private GameObject currentCar;
    private bool isControllingCar = false;

    public  PlayerControls controls;
    private Vector2 movementInput;

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
            // Handle player movement when not controlling a car
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cars") && !isControllingCar)
        {
            // Debug.Log("collided with car"+other.gameObject.name);
            // Get close to a car and switch control to that car
            SwitchToCar(other.gameObject);
            
        }
    }

    private void SwitchToCar(GameObject car)
    {
        // Disable the player's capsule and enable the car
        gameObject.SetActive(false);

    
        currentCar = car;
        currentCar.SetActive(true);

        // Position the car at the player's position and activate it
        // currentCar.transform.position = transform.position;
        // currentCar.SetActive(true);

        isControllingCar = true;

                // Enable car input
        CarMovements carController = currentCar.GetComponent<CarMovements>();
        if (carController != null)
        {
            carController.EnableInput();
        }
    }

    public void ExitCar()
    {
        // Disable the car and enable the player's capsule
        currentCar.SetActive(false);
        gameObject.SetActive(true);

        isControllingCar = false;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        //  if (isControllingCar && currentCar != null)
        // {
        //     float carHorizontal = context.ReadValue<Vector2>().x;
        //     float carVertical = context.ReadValue<Vector2>().y;
        //     currentCar.transform.Translate(new Vector3(carHorizontal, 0f, carVertical) * Time.deltaTime);
        // }
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
}
