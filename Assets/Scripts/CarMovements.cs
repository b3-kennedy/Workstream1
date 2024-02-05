
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovements : MonoBehaviour
{
    private Player1Input controls;
    private CarSpawner carSpawner;
    private bool isInputEnabled = false;

    public float carSpeed = 100f;
    public float parkingSpaceRadius = 1.0f;

    public Material[] driverMaterials = new Material[8];
    private Material currentMaterial;

    public ParticleSystem explosionParticleSystem;

    [HideInInspector]
    public CarObject thisCar;

    private int currentDriverIndex;

    public GameObject currentDriver;


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
    public AudioSource colOOF;
    public AudioSource colCRASH;
    public AudioSource colSCREAM;



    private Quaternion previousRotation;

    public float diff = 1f;
    public bool spawnDamage = false;

   public GameObject explosionEffectPrefab;

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


    }

    public void OnSwitch()
    {
        controls = new Player1Input();
        GetComponent<PlayerInput>().SwitchCurrentControlScheme(currentDriver.GetComponent<PlayerController>().controlScheme);
        controls.Enable();
    }

    private void OnDisable()
    {
        //controls.Disable();
    }
    public void SetColor(Material currentMaterial, int driverIndex)
    {
        //currentMaterial = driverMaterials[driverIndex];
        ApplyMaterialToChild("body/top", currentMaterial);
        ApplyMaterialToChild("body/body", currentMaterial);

    }
    public void EnableInput(int driverIndex, GameObject playerObject, Vector3 pos)
    {
        isInputEnabled = true;

        carSpawner.OnCarPickedUp(thisCar);

        currentDriver = playerObject;
        currentDriverIndex = driverIndex;
        SetColor(currentDriver.GetComponent<MeshRenderer>().material, driverIndex);

    }

    public void DisableInput()
    {
        if(currentDriver != null)
        {
            currentDriver.SetActive(true);
            currentDriver.GetComponent<PlayerController>().OnSpawn();
            currentDriver.transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z + 3);
            if (Camera.main.GetComponent<MultipleTargetCamera>())
            {
                for (int i = 0; i < Camera.main.GetComponent<MultipleTargetCamera>().targets.Count; i++)
                {
                    if (Camera.main.GetComponent<MultipleTargetCamera>().targets[i].gameObject == thisCar.carObject)
                    {
                        Camera.main.GetComponent<MultipleTargetCamera>().targets[i] = currentDriver.transform;
                    }
                }
            }
            currentDriver = null;
            isInputEnabled = false;
        }



    }


    private void Update()
    {
        
        if (isInputEnabled && currentDriver.GetComponent<PlayerController>().pad != null)
        {


            Vector2 stickL = currentDriver.GetComponent<PlayerController>().pad.leftStick.ReadValue();
            Vector2 stickR = currentDriver.GetComponent<PlayerController>().pad.rightStick.ReadValue();

            if (GetComponent<PlayerInput>().currentControlScheme == "GamePadLeft" && new Vector2(stickL.x, stickL.y) != Vector2.zero)
            {

                for (int i = 0; i < Gamepad.all.Count; i++)
                {
                    if (Gamepad.all[i] == currentDriver.GetComponent<PlayerController>().pad)
                    {
                        Vector2 movementInput = new Vector2(stickL.x, stickL.y);
                        moveInput = movementInput.y;
                        turnInput = movementInput.x;
                        float buttonP = controls.Player.Drive.ReadValue<float>();
                        moveInput *= moveInput > 0 ? fwdspeed : revSpeed;


                    }
                }
            }
            else if (GetComponent<PlayerInput>().currentControlScheme == "GamePadRight" && new Vector2(stickR.x, stickR.y) != Vector2.zero)
            {
                for (int i = 0; i < Gamepad.all.Count; i++)
                {
                    if (Gamepad.all[i] == currentDriver.GetComponent<PlayerController>().pad)
                    {
                        Vector2 movementInput = new Vector2(stickR.x, stickR.y);
                        moveInput = movementInput.y;
                        turnInput = movementInput.x;
                        float buttonP = controls.Player.Drive.ReadValue<float>();
                        moveInput *= moveInput > 0 ? fwdspeed : revSpeed;


                    }
                }
            }


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
        if (colliders.Length > 0 && moveInput < 1&& thisCar.isParked == false && currentDriver!=null)
        {

            
            thisCar.isParked = (true);
            foreach (Collider c in colliders)
            {
                switch (c.gameObject.name.Split('c')[0])
                {

                    case "100":
                        if (c.transform.parent.GetComponentInChildren<DoublePointParkingSpot>())
                        {
                            parkingScoreEarned = 200 * thisCar.life / 100;
                        }
                        else
                        {
                            parkingScoreEarned = 100 * thisCar.life / 100;
                        }
                        break;
                    case "200":
                        if (c.transform.parent.GetComponentInChildren<DoublePointParkingSpot>())
                        {
                            parkingScoreEarned = 400 * thisCar.life / 100;
                        }
                        else
                        {
                            parkingScoreEarned = 200 * thisCar.life / 100;
                        }
                        break;
                    case "300":
                        if (c.transform.parent.GetComponentInChildren<DoublePointParkingSpot>())
                        {
                            parkingScoreEarned = 600 * thisCar.life / 100;
                        }
                        else
                        {
                            parkingScoreEarned = 300 * thisCar.life / 100;
                        }
                        break;
                    default:
                        break;
                }
                parkingScoreFloatingText = "+ " + parkingScoreEarned;


            }

            //makes car heavy after parking
            //carRB.drag = 15;
           // carRB.mass = 15;
            sphereRB.drag = 5;
            sphereRB.mass = 5;
            transform.Rotate(0f, 0f, 0f, Space.World);
            // carRB.constraints = RigidbodyConstraints.FreezePositionY;
            /*thiscarRB.drag = 100;
            thiscarRB.mass = 100;*/

            ShowFloatingScore();
            DisableInput();
            RandomEventController.Instance.drivableCars.Remove(gameObject);
        }

    }



    private void FixedUpdate()
    {

        //sphereRB.AddForce(transform.forward * 500, ForceMode.Acceleration);

        if (isInputEnabled)
        {
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);

        }

        carRB.MoveRotation(transform.rotation);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        //Debug.Log("triggered");
        if (currentDriver != null)
        {
            if (collision.gameObject.CompareTag("Player") && !gameObject.CompareTag("freeCar"))
            {
                ReduceLifeOnDamage(10);
                Debug.Log("Player HIT: " + thisCar.life);

                colOOF.Play();
                colCRASH.Play();

                ShowFloatingLostLife();
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Cars") || collision.gameObject.CompareTag("freeCar") || collision.gameObject.CompareTag("pickedUpCar"))
            {
                ReduceLifeOnDamage(10);
                Debug.Log("Object HIT: " + thisCar.life);
                ShowFloatingLostLife();

            }
            else if (collision.gameObject.CompareTag("Walls"))
            {
                ReduceLifeOnDamage(20);
                Debug.Log("wall HIT: " + thisCar.life);
                ShowFloatingLostLife();

            }
            else if (collision.gameObject.CompareTag("TrafficCone"))
            {
                ReduceLifeOnDamage(10);
                Debug.Log("wall HIT: " + thisCar.life);
                colCRASH.Play();
                ShowFloatingLostLife();

            }
            /*else if (collision.gameObject.CompareTag("SpeedBump"))
            {
                ReduceLifeOnDamage(5);
                Debug.Log("wall HIT: " + thisCar.life);
                ShowFloatingLostLife();

            }*/
        }

        if (spawnDamage)
        {



        }

        spawnDamage = true;
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Parking"))
    //    {
    //        Debug.Log(moveInput);
    //        if(moveInput == 0)
    //        {


    //            Debug.Log("PARK");
    //        }
            
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedBump"))
        {
            Debug.Log("On Speedbump");
            fwdspeed = 40;
            colOOF.Play();


        }



        if (other.gameObject.CompareTag("freeCar"))
        {
            ReduceLifeOnDamage(10);
            Debug.Log("wall HIT: " + thisCar.life);
            colCRASH.Play();
            ShowFloatingLostLife();

        }

        if (other.CompareTag("test"))
        {
            Debug.Log("hello");
        }

        if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("Water");
            DisableInput();
            if(currentDriver != null)
            {
                currentDriver.transform.position = Vector3.zero;
            }
            Destroy(gameObject);

        }








    }

    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.CompareTag("SpeedBump"))
        {
            Debug.Log("Off Speedbump");
            fwdspeed = 150;


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


    void ShowFloatingLostLife()
    {
        if (FloatingTextPrefab != null)
        {
            var go = Instantiate(FloatingTextPrefab, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(90, 0, 0), transform);
            go.GetComponent<TextMesh>().color = Color.red;
            go.GetComponent<TextMesh>().text = "" + thisCar.life;
        }
    }
    
    void ShowFloatingScore()
    {

        if (FloatingTextPrefab != null)
        {

            var go = Instantiate(FloatingTextPrefab, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(90, 0, 0), transform);
            go.GetComponent<TextMesh>().text = parkingScoreFloatingText;
        }

        OnCarParked?.Invoke();
    }


    public void ReduceLifeOnDamage(int damage)
    {
        
        if (thisCar.life - damage <= 0 )
        {

            // colSCREAM.Play();

            DisableInput();

            ParticleSystem explosion = Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);

            Destroy(gameObject);

            //play audio

            colSCREAM.Play();

        }
        else
        {
            thisCar.life -= damage;
        }
    }

}

