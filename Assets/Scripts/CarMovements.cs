
using System;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;

public class CarMovements : MonoBehaviour
{
    private PlayerControls controls;
    private CarSpawner carSpawner;
    private bool isInputEnabled = false;

    public float carSpeed = 100f;
    public float parkingSpaceRadius = 1.0f;

    public Material[] driverMaterials = new Material[8];
    private Material currentMaterial;


    private CarObject thisCar;

    private int currentDriverIndex;

    public GameObject currentDriver;

    private Vector3 playerOriginalPosition;

    private string parkingScoreFloatingText;

    public GameObject FloatingTextPrefab;

    public bool Azine = false;


    public event Action OnCarParked;
    public int parkingScoreEarned;

    public Rigidbody sphereRB;
    public float moveInput;
    public float turnInput;
    public float fwdspeed;
    public float revSpeed;
    public float turnSpeed;

    public LayerMask parkingSpaceLayer;

    public AudioSource screechAudio;

    private Quaternion previousRotation;

    public float diff = 0.3f;


    private void Start()
    {
        carSpawner = FindObjectOfType<CarSpawner>();
        thisCar = carSpawner.GetCarObject(gameObject);


        sphereRB.transform.parent = null;

        previousRotation = transform.rotation;
    }

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();

    }

    private void OnDisable()
    {
        controls.Disable();
    }
    public void SetColor(Material currentMaterial, int driverIndex)
    {
        currentMaterial = driverMaterials[driverIndex];
        ApplyMaterialToChild("body/top", currentMaterial);
        ApplyMaterialToChild("body/body", currentMaterial);

    }
    public void EnableInput(int driverIndex, GameObject playerObject, Vector3 pos)
    {
        isInputEnabled = true;
        playerOriginalPosition = pos;

        carSpawner.OnCarPickedUp(thisCar);
       
        currentDriver = playerObject;
        currentDriverIndex = driverIndex;
        SetColor(currentMaterial, driverIndex);

    }

    public void DisableInput()
    {
        currentDriver.SetActive(true);
        currentDriver.transform.position = playerOriginalPosition;
        isInputEnabled = false;
      

    }



    private void Update()
    {
        if (isInputEnabled)
        {
         

            if (currentDriverIndex == 0)
            {


                Vector2 movementInput = controls.Player.Move.ReadValue<Vector2>();
                moveInput = movementInput.y;
                turnInput = movementInput.x;
                float buttonP = controls.Player.Drive.ReadValue<float>();


                if (!Azine)
                {
                    if (buttonP > 0)
                    {
                        moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                    }
                }
                else
                {
                    moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
                }
               
                /* if (buttonP > 0)
                 {
                     fwdspeed = 200; 
 ;
                 }
                 else
                 {
                     fwdspeed = 0;
                 }*/
            }
     
            else if (currentDriverIndex == 1) 
            {
                Vector2 movementInput = controls.Player.Move2.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
            }
            else if(currentDriverIndex == 3) { }
            else if (currentDriverIndex == 4) { }


      

            transform.position = sphereRB.transform.position;




            float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
            transform.Rotate(0f, newRotation, 0f, Space.World);

            Quaternion currentRotation = transform.rotation;
            float rotationDifference = Quaternion.Angle(previousRotation, currentRotation);

            if (rotationDifference > diff)  // Adjust the threshold as needed
            {
                screechAudio.Play();
            }

            previousRotation = currentRotation;

        }


        Collider[] colliders = Physics.OverlapSphere(transform.position, parkingSpaceRadius, parkingSpaceLayer);

        
        //  change parking conditions 
        if (colliders.Length > 0 && moveInput == 0 && thisCar.isParked == false)
        {
           

            thisCar.isParked = (true);
            foreach(Collider c in colliders) {
                parkingScoreFloatingText = "+ " + c.gameObject.name.Split('c')[0];
                switch (c.gameObject.name.Split('c')[0])
                {
                    case "100":
                        parkingScoreEarned = 100;
                        break;
                    case "200":
                        parkingScoreEarned = 200;
                        break;
                    case "300":
                        parkingScoreEarned = 300;
                        break;
                    default:
                        break;
                }


            }

           
            ShowFloatingScore();
            DisableInput();
    
        }

    }



    private void FixedUpdate()
    {
        if (isInputEnabled)
        {
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        
        }
    }





    void ApplyMaterialToChild(string childObjectName, Material material)
    {
        Transform childTransform = transform.Find(childObjectName);
     

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



    void ShowFloatingScore()
    {
        
        if (FloatingTextPrefab != null)
        {
            var go = Instantiate(FloatingTextPrefab,new Vector3(transform.position.x, 2 , transform.position.z), Quaternion.Euler(90, 0, 0), transform);
            go.GetComponent<TextMesh>().text = parkingScoreFloatingText;
        }

        OnCarParked?.Invoke();
    }




}
 
