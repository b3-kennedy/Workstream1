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
    public GameObject playerNumberText;

    //public InputActionAsset controlScheme;


    private void Awake()
    {
        Instance = this;
    }

    void SpawnPlayer(PlayerInput player, int controllerIndex, string controlScheme, PlayerWithController pwc)
    {
        player.GetComponent<PlayerController>().pad = Gamepad.all[controllerIndex];
        player.GetComponent<PlayerController>().controlScheme = controlScheme;
        player.GetComponent<PlayerController>().OnSpawn();
        players.Add(player.gameObject);
        player.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
        player.transform.position = playerSpawnParent.GetChild(index).position;
        if(player.transform.childCount > 0)
        {
            player.transform.GetChild(0).GetComponent<MeshRenderer>().material = playerMats[index];
        }
        else
        {
            player.GetComponent<MeshRenderer>().material = playerMats[index];
        }
        
        GameObject txt = Instantiate(playerNumberText);
        txt.GetComponent<TextMeshPro>().text = "P" + (pwc.playerNumber+1).ToString();
        txt.GetComponent<FollowPlayer>().target = player.transform;
        player.GetComponent<PlayerController>().playerNumberText = txt.GetComponent<FollowPlayer>();
        if(pwc.selectedMaterial != null)
        {
            player.GetComponent<MeshRenderer>().material = pwc.selectedMaterial;
        }
        

        if (Camera.main.GetComponent<MultipleTargetCamera>())
        {
            Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
        }
        ScoreUIController.Instance.playersJoined++;
        index++;
    }

    // Start is called before the first frame update
    void Start()
    {

       // Debug.Log(Gamepad.all.Count);


        foreach (var player in PlayerControllerManager.Instance.players)
        {
            if (player.joined)
            {
                if (player.controllerSide == PlayerWithController.ControllerSide.Left)
                {
                    var playerSpawn = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[player.controllerIndex]);
                    SpawnPlayer(playerSpawn, player.controllerIndex, "GamePadLeft", player);
                }
                else
                {
                    var playerSpawn = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[player.controllerIndex]);
                    SpawnPlayer(playerSpawn, player.controllerIndex, "GamePadRight", player);
                }
            }


            
        }

        //for (int i = 0; i < Gamepad.all.Count; i++)
        //{

        //    var player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadLeft", pairWithDevice: Gamepad.all[controllerIndex]);
        //    player1.GetComponent<PlayerController>().pad = Gamepad.all[i];
        //    player1.GetComponent<PlayerController>().controlScheme = "GamePadLeft";
        //    player1.GetComponent<PlayerController>().OnSpawn();
        //    players.Add(player1.gameObject);
        //    player1.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
        //    player1.transform.position = playerSpawnParent.GetChild(index).position;
        //    player1.GetComponent<MeshRenderer>().material = playerMats[index];
        //    index++;

        //    var player2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "GamePadRight", pairWithDevice: Gamepad.all[controllerIndex]);
        //    player2.GetComponent<PlayerController>().pad = Gamepad.all[i];
        //    player2.GetComponent<PlayerController>().controlScheme = "GamePadRight";
        //    player2.GetComponent<PlayerController>().OnSpawn();
        //    players.Add(player2.gameObject);
        //    player2.GetComponent<PlayerController>().scoreTextMesh = scoreTexts[index];
        //    player2.transform.position = playerSpawnParent.GetChild(index).position;
        //    player2.GetComponent<MeshRenderer>().material = playerMats[index];

        //    index++;
        //    controllerIndex++;

        //    if (Camera.main.GetComponent<MultipleTargetCamera>())
        //    {
        //        Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player1.transform);
        //        Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player2.transform);
        //    }

        //}

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
