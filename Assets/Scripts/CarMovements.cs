// using UnityEngine;
// using UnityEngine.InputSystem;

// public class CarMovements : MonoBehaviour
// {
//     private PlayerControls controls;
//     private bool isInputEnabled = false;

//      public float carSpeed = 5f;
//     public float turnSpeed = 180f;

//     private void OnEnable()
//     {
//         controls = new PlayerControls();
//         controls.Enable();

//         controls.Player.Move.Started += OnMovementPerformed;
//         controls.Player.Move.canceled += OnMovementCanceled;
//     }

//     private void OnDisable()
//     {
//         controls.Disable();

//        controls.Player.Move.started -= OnMovementPerformed;
//         controls.Player.Move.canceled -= OnMovementCanceled;
//     }

//     public void EnableInput()
//     {
//         isInputEnabled = true;
//         Debug.Log("car enabled");
//     }

//     public void DisableInput()
//     {
//         isInputEnabled = false;
//     }
// private void Update() {
//    }
//     private void OnMovementPerformed(InputAction.CallbackContext context)
//     {
//          if (isInputEnabled)
//         {
//              Vector2 movementInput = context.ReadValue<Vector2>();
//         float moveInput = movementInput.y;
//         float turnInput = movementInput.x;

//         // Adjust the moveInput based on your speed values
//         float fwdspeed = (moveInput > 0) ? carSpeed : 0f;
//         float revSpeed = (moveInput < 0) ? -carSpeed : 0f;

//         // Move the car based on the input
//         float speed = (moveInput > 0) ? fwdspeed : revSpeed;
//         Vector3 movement = transform.forward * speed * Time.deltaTime;
//         transform.Translate(movement, Space.World);

//         // Rotate the car based on the turn input
//         float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
//         transform.Rotate(0f, newRotation, 0f, Space.World);

//         //////*******
//         //    Vector2 movementInput = context.ReadValue<Vector2>();
//         //     float moveInput = movementInput.y;
//         //     float turnInput = movementInput.x;

//         //      // Adjust the moveInput based on your speed values
//         //     float fwdspeed = (moveInput > 0) ? carSpeed : 0f;
//         //     float revSpeed = (moveInput < 0) ? -carSpeed : 0f;
//         //     moveInput *= (moveInput > 0) ? fwdspeed : revSpeed;

//         //     // Move the car based on the input
//         //     Vector3 movement = new Vector3(0f, 0f, moveInput);
//         //     transform.Translate(movement * Time.deltaTime);

//         //     // Rotate the car based on the turn input
//         //     float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
//         //     transform.Rotate(0f, newRotation, 0f, Space.World); 
//         }

//     }

//     private void OnMovementCanceled(InputAction.CallbackContext context)
//     {
//         // Handle when car movement is canceled (optional)
//     }
// }
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovements : MonoBehaviour
{
    private PlayerControls controls;
    private bool isInputEnabled = false;

    public float carSpeed = 20f;
    public float turnSpeed = 180f;

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Move.performed += OnMovementPerformed;
        controls.Player.Move.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        controls.Disable();

        controls.Player.Move.performed -= OnMovementPerformed;
        controls.Player.Move.canceled -= OnMovementCanceled;
    }

    public void EnableInput()
    {
        isInputEnabled = true;
    }

    public void DisableInput()
    {
        isInputEnabled = false;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        // Handle additional logic when movement is performed (if needed)
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // Handle when movement is canceled (if needed)
    }

    private void Update()
    {
        if (isInputEnabled)
        {
            Vector2 movementInput = controls.Player.Move.ReadValue<Vector2>();
            float moveInput = movementInput.y;
            float turnInput = movementInput.x;

            // Adjust the moveInput based on your speed values
            float fwdspeed = (moveInput > 0) ? carSpeed : 0f;
            float revSpeed = (moveInput < 0) ? -carSpeed : 0f;

            // Move the car based on the input
            float speed = (moveInput > 0) ? fwdspeed : revSpeed;
            Vector3 movement = transform.forward * speed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            // Rotate the car based on the turn input
            float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
            transform.Rotate(0f, newRotation, 0f, Space.World);
        }
    }
}
