
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
    public Rigidbody carRB;
    //public Rigidbody thiscarRB;


    public float moveInput;
    public float turnInput;
    public float fwdspeed;
    public float revSpeed;
    public float turnSpeed;

    public LayerMask parkingSpaceLayer;

    public AudioSource screechAudio;

    private Quaternion previousRotation;

    public float diff = 0.3f;
    public bool spawnDamage = false;

    private void Start()
    {
        carSpawner = FindObjectOfType<CarSpawner>();
        thisCar = carSpawner.GetCarObject(gameObject);


        sphereRB.transform.parent = null;
        carRB.transform.parent = null;


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
        currentDriver.transform.position =new Vector3(transform.position.x+1, transform.position.y, transform.position.z+2);
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
               
               
            }
     
            else if (currentDriverIndex == 1) 
            {
                Vector2 movementInput = controls.Player.Move2.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
                float buttonP = controls.Player.Drive1.ReadValue<float>();


                if (buttonP > 0)
                {
                    moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                }
            }
            else if (currentDriverIndex == 2)
            {
                Vector2 movementInput = controls.Player2.Move.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
                float buttonP = controls.Player2.Drive.ReadValue<float>();


                    if (buttonP > 0)
                    {
                        moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                    }
                  
            }
            else if(currentDriverIndex == 3) {

                Vector2 movementInput = controls.Player2.Move2.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
                float buttonP = controls.Player2.Drive1.ReadValue<float>();


                if (buttonP > 0)
                {
                    moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                }
            }
            else if (currentDriverIndex == 4) {

                Vector2 movementInput = controls.Player3.Move.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
                float buttonP = controls.Player3.Drive.ReadValue<float>();


                if (buttonP > 0)
                {
                    moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                }
            }
            else if (currentDriverIndex == 5)
            {

                Vector2 movementInput = controls.Player3.Move2.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
                float buttonP = controls.Player3.Drive1.ReadValue<float>();


                if (buttonP > 0)
                {
                    moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                }
            }
            else if (currentDriverIndex == 6)
            {

                Vector2 movementInput = controls.Player4.Move.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
                float buttonP = controls.Player4.Drive.ReadValue<float>();


                if (buttonP > 0)
                {
                    moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                }
            }
            else if (currentDriverIndex == 7)
            {

                Vector2 movementInput = controls.Player4.Move2.ReadValue<Vector2>();

                moveInput = movementInput.y;
                turnInput = movementInput.x;
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                float buttonP = controls.Player4.Drive1.ReadValue<float>();


                if (buttonP > 0)
                {
                    moveInput *= moveInput > 0 ? fwdspeed : revSpeed;

                }
            }
         




           /* transform.position = sphereRB.transform.position;




            float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
            transform.Rotate(0f, newRotation, 0f, Space.World);*/

            Quaternion currentRotation = transform.rotation;
            float rotationDifference = Quaternion.Angle(previousRotation, currentRotation);

            if (rotationDifference > diff)  // Adjust the threshold as needed
            {
                screechAudio.Play();
            }

            previousRotation = currentRotation;

        }


        transform.position = sphereRB.transform.position;
        float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
        transform.Rotate(0f, newRotation, 0f, Space.World);





        Collider[] colliders = Physics.OverlapSphere(transform.position, parkingSpaceRadius, parkingSpaceLayer);

        
        //  change parking conditions 
        if (colliders.Length > 0 && moveInput == 0 && thisCar.isParked == false)
        {
           

            thisCar.isParked = (true);
            foreach(Collider c in colliders) {
                
                switch (c.gameObject.name.Split('c')[0])
                {
                    case "100":
                        parkingScoreEarned = 100 * thisCar.life/100;
                        break;
                    case "200":
                        parkingScoreEarned = 200 * thisCar.life / 100;
                        break;
                    case "300":
                        parkingScoreEarned = 300 * thisCar.life / 100;
                        break;
                    default:
                        break;
                }
                parkingScoreFloatingText = "+ " + parkingScoreEarned;


            }

            //makes car heavy after parking
            carRB.drag = 5;
            carRB.mass = 5;
            /*thiscarRB.drag = 100;
            thiscarRB.mass = 100;*/

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

        carRB.MoveRotation(transform.rotation);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("triggered");
        if (collision.gameObject.CompareTag("Player"))
        {
            thisCar.life -= 1;
            Debug.Log("Player HIT: " + thisCar.life);
            showFloatingLostLife();
        }
        else if (collision.gameObject.CompareTag("CarCOLLL"))
        {
            ReduceLifeOnDamage();
            Debug.Log("Object HIT: " + thisCar.life);
            showFloatingLostLife();

        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            thisCar.life -= 10;
            Debug.Log("wall HIT: " + thisCar.life);
            showFloatingLostLife();

        }
        if (spawnDamage)
        {
            


        }

        spawnDamage = true;
    }


   /* private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.tag == "CARAC")
            {
                thisCar.life -= 5;
                Debug.Log("CAR ON CAR: " + thisCar.life);
            }      

    }
   */
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


    void showFloatingLostLife()
    {
        if (FloatingTextPrefab != null)
        {
            var go = Instantiate(FloatingTextPrefab, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(90, 0, 0), transform);
            go.GetComponent<TextMesh>().color = Color.red;
            go.GetComponent<TextMesh>().text = ""+thisCar.life;
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


    void ReduceLifeOnDamage()
    {
        thisCar.life -= 5;
    }

}
 
