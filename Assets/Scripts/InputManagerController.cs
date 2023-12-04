using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerController : MonoBehaviour
{
    [SerializeField] private GameObject secondPlayerPrefab; // The new Player Prefab to use.
    private GameObject firstPlayerPrefab;

    [SerializeField] GameObject[] _playerPrefab;
    public PlayerInputManager inputManager;

    private bool secondPlayer;


    private void Start()
    {
        secondPlayer = false;
        firstPlayerPrefab = PlayerInputManager.instance.playerPrefab;
    }
    private void Update()
    {
        //if (GameObject.FindWithTag("Player")) //Checking for a game object with the tag
        //{
        //    PlayerInputManager.instance.playerPrefab = secondPlayerPrefab; //If yes, changes the player prefab field to your selected prefab
        //}

        if (!secondPlayer)
        {
            PlayerInputManager.instance.playerPrefab = secondPlayerPrefab;
            secondPlayer = true;
        }
        else
        {
            PlayerInputManager.instance.playerPrefab = firstPlayerPrefab;
            secondPlayer = false;
        }
    }
}
