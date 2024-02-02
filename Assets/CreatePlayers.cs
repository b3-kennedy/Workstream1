using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CreatePlayers : MonoBehaviour
{
    public static CreatePlayers Instance;
    public GameObject playerPrefab;
    public List<GameObject> players;
    public Material[] playerMats;
    public TMP_Text[] scoreTexts;
    public Transform playerSpawnParent;
    int index = 0;
    int controllerIndex = 0;
    //public InputActionAsset controlScheme;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(Gamepad.all.Count);


        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[controllerIndex]);
            player1.GetComponent<PlayerController>().pad = Gamepad.all[i];
            player1.GetComponent<PlayerController>().controlScheme = "GamePadLeft";
            player1.GetComponent<PlayerController>().OnSpawn();
            players.Add(player1.gameObject);
            player1.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
            player1.transform.position = playerSpawnParent.GetChild(index).position;
            player1.GetComponent<MeshRenderer>().material = playerMats[index];
            index++;

            var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[controllerIndex]);
            player2.GetComponent<PlayerController>().pad = Gamepad.all[i];
            player2.GetComponent<PlayerController>().controlScheme = "GamePadRight";
            player2.GetComponent<PlayerController>().OnSpawn();
            players.Add(player2.gameObject);
            player2.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
            player2.transform.position = playerSpawnParent.GetChild(index).position;
            player2.GetComponent<MeshRenderer>().material = playerMats[index];

            index++;
            controllerIndex++;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
