
using TMPro;
using UnityEngine;

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

    public GameObject currentDriver;

    private Vector3 playerOriginalPosition;

    private string parkingScoreText = "+ 200";






    public Rigidbody sphereRB;
    public float moveInput;
    public float turnInput;

    public GameObject FloatingTextPrefab;

    public float fwdspeed;
    public float revSpeed;
    public float turnSpeed;

    public TextMeshPro Score;



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
            Vector2 movementInput = controls.Player.Move.ReadValue<Vector2>();
            moveInput = movementInput.y;
            turnInput = movementInput.x;


            //if statement about trigger/shoulder button



            float buttonP = controls.Player.Drive.ReadValue<float>();

            //add reverse, see if thats good or seperate reverse from the left trigger
            if (buttonP > 0)
            {
                moveInput *= moveInput > 0 ? fwdspeed : revSpeed;
            }
            //moveInput *= moveInput > 0 ? fwdspeed : revSpeed;




            transform.position = sphereRB.transform.position;




            float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput;
            transform.Rotate(0f, newRotation, 0f, Space.World);

        }


        Collider[] colliders = Physics.OverlapSphere(transform.position, parkingSpaceRadius, parkingSpaceLayer);


        // Car is parked , change conditions 
        if (colliders.Length > 0 && moveInput == 0 && thisCar.isParked == false)
        {
           

            thisCar.isParked = (true);
            foreach(Collider c in colliders) {
                parkingScoreText = "+ " + c.gameObject.name.Split('c')[0];
                Debug.Log(c.gameObject.name.Split('c')[0]);
            }
            
            //Debug.Log("Car is parked!");

            

            DisableInput();
            ShowFloatingScore();


            
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
        if(FloatingTextPrefab != null)
        {
            var go = Instantiate(FloatingTextPrefab,new Vector3(transform.position.x, 2 , transform.position.z), Quaternion.Euler(90, 0, 0), transform);
            go.GetComponent<TextMesh>().text = parkingScoreText;
        }
    }




}
 
