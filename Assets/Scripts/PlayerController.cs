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

    public  PlayerControls controls;

    public int playerIndex ;

    void Start()
    {
        originalPosition = transform.position;
        string objectName = gameObject.name.Split('r')[1];
        playerIndex = int.Parse(objectName) - 1;
        Debug.Log(gameObject.name);
    

    }

    void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Move.performed += OnMovementPerformed;
        controls.Player.Move.canceled += OnMovementCanceled;
        controls.Player.Switch.performed +=ctx => SwitchToPlayer();
        // controls.Player.Switch.canceled += OnMovementCanceled;
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
           
        }
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        //else
        //{

        //    float carHorizontal = movementInput.x;
        //    float carVertical = movementInput.y;

        //    currentCar.transform.Translate(new Vector3(carHorizontal, 0f, carVertical) * moveSpeed * Time.deltaTime);
        //}
        //if (currentCar != null && currentCar.GetComponent<CarObject>().isParked)
        //{
        //    Debug.Log("switch here for player");
        //    carParked = true;
        //    SwitchToPlayer();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Cars")  && other.gameObject.CompareTag("freeCar"))
        {
            SwitchToCar(other.gameObject);
            other.gameObject.tag= "pickedUpCar";
            Debug.Log("collision with : " + other.gameObject.name);
            
        }
    }

private void SwitchToPlayer()
{

    transform.position = originalPosition;
    gameObject.SetActive(true);
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
            carMovement.EnableInput(playerIndex, gameObject , originalPosition);
            
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
