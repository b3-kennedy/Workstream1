using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public PlayerType inputType;
    private GameObject currentCar;
    private bool isControllingCar = false;

    private Vector2 movementInput;
    private Vector3 originalPosition; // Store the original position of the player

    public bool carParked = false;
    public int score = 0;

    public  PlayerControls controls;

    public int playerIndex ;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _driveAction;
    private InputAction _switchAction;
    private bool d_Pressed;
    private bool d_Released;

    void Start()
    {
        originalPosition = transform.position;
      //  string objectName = gameObject.name.Split('r')[1];
      //  playerIndex = int.Parse(objectName) - 1;
        Debug.Log(gameObject.name);
    

    }

    private void SetupInputReference()
    {
        _playerInput = this.GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _switchAction = _playerInput.actions["Switch"];
        _driveAction = _playerInput.actions["Drive"];

        _moveAction.performed += OnMovementPerformed;
        _moveAction.canceled += OnMovementCanceled;
       // _driveAction.performed += ctx => d_Pressed = true;
       // _driveAction.canceled += ctx => d_Released = true;
        _switchAction.performed += ctx => SwitchToPlayer();
    }

    void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();

        SetupInputReference();
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
            carMovement.EnableInput( gameObject , originalPosition);
            
        }
    }

    public void ExitCar()
    {
        gameObject.SetActive(true);

        isControllingCar = false;
    }

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
     
    }

    public void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
    public void OnSwitch(InputAction.CallbackContext context)
    {
        if (context.performed) { print("Attack"); }
            
    }
}
