using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreatePlayers : MonoBehaviour
{
    public static CreatePlayers Instance;
    public GameObject playerPrefab;
    public List<GameObject> players;
    //public InputActionAsset controlScheme;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        //PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
        Debug.Log(Gamepad.all.Count);

        var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[0]);
        player1.GetComponent<PlayerController>().pad = Gamepad.all[0];
        player1.GetComponent<PlayerController>().controlScheme = "GamePadLeft";
        player1.GetComponent<PlayerController>().OnSpawn();
        players.Add(player1.gameObject);
        

        var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[0]);
        player2.GetComponent<PlayerController>().pad = Gamepad.all[0];
        player2.GetComponent<PlayerController>().controlScheme = "GamePadRight";
        player2.GetComponent<PlayerController>().OnSpawn();
        players.Add(player2.gameObject);

        var player3 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[1]);
        player3.GetComponent<PlayerController>().pad = Gamepad.all[1];
        player3.GetComponent<PlayerController>().controlScheme = "GamePadLeft";
        player3.GetComponent<PlayerController>().OnSpawn();
        players.Add(player3.gameObject);

        var player4 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[1]);
        player4.GetComponent<PlayerController>().pad = Gamepad.all[1];
        player4.GetComponent<PlayerController>().controlScheme = "GamePadRight";
        player4.GetComponent<PlayerController>().OnSpawn();
        players.Add(player4.gameObject);

        var player5 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[2]);
        player5.GetComponent<PlayerController>().pad = Gamepad.all[2];
        player5.GetComponent<PlayerController>().controlScheme = "GamePadLeft";
        player5.GetComponent<PlayerController>().OnSpawn();
        players.Add(player5.gameObject);

        var player6 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[2]);
        player6.GetComponent<PlayerController>().pad = Gamepad.all[2];
        player6.GetComponent<PlayerController>().controlScheme = "GamePadRight";
        player6.GetComponent<PlayerController>().OnSpawn();
        players.Add(player6.gameObject);







    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
