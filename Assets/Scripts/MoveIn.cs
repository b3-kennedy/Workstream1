// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public GameObject carPre;  // Drag your "CarPrefab" onto this field in the Inspector

//     private GameObject currentCar;
//     private bool isControllingCar = false;

//     public  PlayerControls controls;
//     private Vector2 movementInput;

//     void OnEnable()
//     {
//         controls = new PlayerControls();
//         controls.Enable();

//         controls.Player.Move.performed += OnMovementPerformed;
//         controls.Player.Move.canceled += OnMovementCanceled;
//     }

//     void OnDisable()
//     {
//         controls.Disable();

//         controls.Player.Move.performed -= OnMovementPerformed;
//         controls.Player.Move.canceled -= OnMovementCanceled;
//     }

//     void Update()
//     {
//         if (!isControllingCar)
//         {
//             // Handle player movement when not controlling a car
//             Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
//             transform.Translate(movement * moveSpeed * Time.deltaTime);
//         }
//         else
//         {
//             // Handle car controls when controlling a car
//             // Adjust this part based on your car control logic
//             float carHorizontal = movementInput.x;
//             float carVertical = movementInput.y;
//             currentCar.transform.Translate(new Vector3(carHorizontal, 0f, carVertical) * moveSpeed * Time.deltaTime);
//         }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Car") && !isControllingCar)
//         {
//             // Get close to a car and switch control to that car
//             SwitchToCar(other.gameObject);
//         }
//     }

//     private void SwitchToCar(GameObject car)
//     {
//         // Disable the player's capsule and enable the car
//         gameObject.SetActive(false);

//         // Instantiate the carPrefab if not already instantiated
//         if (currentCar == null)
//         {
//             currentCar = Instantiate(carPrefab, transform.position, transform.rotation);
//         }

//         // Position the car at the player's position and activate it
//         currentCar.transform.position = transform.position;
//         currentCar.SetActive(true);

//         isControllingCar = true;
//     }

//     public void ExitCar()
//     {
//         // Disable the car and enable the player's capsule
//         currentCar.SetActive(false);
//         gameObject.SetActive(true);

//         isControllingCar = false;
//     }

//     private void OnMovementPerformed(InputAction.CallbackContext context)
//     {
//         movementInput = context.ReadValue<Vector2>();
//     }

//     private void OnMovementCanceled(InputAction.CallbackContext context)
//     {
//         movementInput = Vector2.zero;
//     }
// }
