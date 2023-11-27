
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using TMPro;

public class CarMovements : MonoBehaviour
{
    private PlayerControls controls;
    private CarSpawner carSpawner;
    private bool isInputEnabled = false;

    public float carSpeed = 100f;
    //public float turnSpeed = 180f;
    public float parkingSpaceRadius = 1.0f;

    public Material[] driverMaterials = new Material[8];
    private Material currentMaterial;

    public TextMeshPro uiScore;

    private CarObject thisCar;






    public Rigidbody sphereRB;
    public float moveInput;
    public float turnInput;

    public float fwdspeed;
    public float revSpeed;
    public float turnSpeed;



    public LayerMask parkingSpaceLayer;
    private void Start()
    {
        carSpawner = FindObjectOfType<CarSpawner>();
        thisCar = carSpawner.GetCarObject(gameObject);


        sphereRB.transform.parent = null;


    }

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();

        // controls.Player.Move.performed += OnMovementPerformed;
        // controls.Player.Move.canceled += OnMovementCanceled;



       // controls.Player.Drive.performed += true;
    }

    private void OnDisable()
    {
        controls.Disable();

        // controls.Player.Move.performed -= OnMovementPerformed;
        // controls.Player.Move.canceled -= OnMovementCanceled;
    }
public void SetColor(Material currentMaterial, int driverIndex)
    {
        currentMaterial = driverMaterials[driverIndex];
        ApplyMaterialToChild("body/top",currentMaterial);
        ApplyMaterialToChild("body/body",currentMaterial);
        
    }
    public void EnableInput(int driverIndex)
    {
        isInputEnabled = true;
         
        carSpawner.OnCarPickedUp(thisCar);
        // thisCar.SetDriver(driverIndex);
        SetColor(currentMaterial, driverIndex);
        
    }

    public void DisableInput()
    {
        isInputEnabled = false;
    }

    // private void OnMovementPerformed(InputAction.CallbackContext context)
    // {
    // }

    // private void OnMovementCanceled(InputAction.CallbackContext context)
    // {
       
    // }

    private void Update()
    {
        if (isInputEnabled)
        {
            Vector2 movementInput = controls.Player.Move.ReadValue<Vector2>();
             moveInput = movementInput.y;
             turnInput = movementInput.x;


            //  float fwdspeed = (moveInput > 0) ? carSpeed : 0f;
            // float revSpeed = (moveInput < 0) ? -carSpeed : 0f;
            // float speed = (moveInput > 0) ? fwdspeed : revSpeed;
            //  Vector3 movement = transform.forward * speed * Time.deltaTime;
            // transform.Translate(movement, Space.World);


            //if statement about trigger/shoulder button



            float buttonP = controls.Player.Drive.ReadValue<float>();

            //add reverse, see if thats good or seperate reverse from the left trigger
            if (buttonP > 0)
            {
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
            }
           // moveInput *= moveInput > 0 ? fwdspeed : revSpeed;




            transform.position = sphereRB.transform.position;




            float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
            transform.Rotate(0f, newRotation, 0f, Space.World);
        
        }
                    Collider[] colliders = Physics.OverlapSphere(transform.position, parkingSpaceRadius, parkingSpaceLayer);
                    
            if (colliders.Length > 0)
            {
                // Car is parked





            if (moveInput == 0)
            {
                thisCar.isParked = (true);
                Debug.Log("Car is parked!");

                //stop control of the car
                //spawn player back
            }
                /*thisCar.isParked = (true);
                Debug.Log("Car is parked!");*/
            }
    }



    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }







    void ApplyMaterialToChild(string childObjectName, Material material)
    {
        Transform childTransform = transform.Find(childObjectName);
        Debug.Log(childTransform+" here");

        if (childTransform != null)
        {
            Renderer childRenderer = childTransform.GetComponent<Renderer>();

            if (childRenderer != null)
            {
                childRenderer.material = material;
            }
            else
            {
                Debug.LogError("Renderer component not found on child object: " + childObjectName);
            }
        }
    }


}
