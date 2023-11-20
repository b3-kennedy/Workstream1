using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpCar : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject carPrefab;  

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
            SwitchToCar(other.gameObject);
            
        }
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
        currentCar.SetActive(false);
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
