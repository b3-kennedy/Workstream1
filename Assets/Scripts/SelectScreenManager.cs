using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject collum;
    public List<GameObject> list = new List<GameObject>();
    private int selectedSkin = 0;
    private GameObject tempGO;

    public int index = 0;


    private void Start()
    {
        for (int i = 0; i < Gamepad.all.Count * 2; i++) 
        {
            PlayerControllerManager.Instance.players.Add(new PlayerWithController());
            if( i % 2 != 0)
            {
                PlayerControllerManager.Instance.players[i].controllerSide = PlayerWithController.ControllerSide.Right;
            }
        }
        
        tempGO = collum;
    }


    
    void Join(int i, PlayerWithController player)
    {
        if (Gamepad.all[i].buttonSouth.wasPressedThisFrame && (player.controllerSide == PlayerWithController.ControllerSide.Right &&
    !       player.joined))
        {
            player.joined = true;
            player.card = list[index];
            player.card.SetActive(true);
            
            player.controllerIndex = i;
            player.playerNumber = index;
            player.card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "P" + (player.playerNumber + 1).ToString();
            //NextOption();
            index++;
        }

        else if (Gamepad.all[i].dpad.down.wasPressedThisFrame && (player.controllerSide == PlayerWithController.ControllerSide.Left &&
            !player.joined))
        {
            player.joined = true;
            player.card = list[index];
            player.card.SetActive(true);
            
            player.controllerIndex = i;
            player.playerNumber = index;
            player.card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "P" + (player.playerNumber + 1).ToString();
            //NextOption();
            index++;
        }
    }

    void Leave(int i, PlayerWithController player)
    {
        if (player.controllerSide == PlayerWithController.ControllerSide.Left && player.controllerIndex == i)
        {
            if (Gamepad.all[i].dpad.right.wasPressedThisFrame && player.joined)
            {
                player.joined = false;
                player.card.SetActive(false);
                //BackOption();
                index--;
            }
        }

        else if (player.controllerSide == PlayerWithController.ControllerSide.Right && player.controllerIndex == i)
        {
            if (Gamepad.all[i].buttonEast.wasPressedThisFrame && player.joined)
            {
                player.joined = false;
                player.card.SetActive(false);
                //BackOption();
                index--;
            }
        }
    }

    private void Update()
    {
        for (int i = 0;i < Gamepad.all.Count;i++)
        {
            foreach (var player in PlayerControllerManager.Instance.players)
            {

                Join(i, player);
                Leave(i, player);

            }

            
        }

        if (Gamepad.all[0].startButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene(1);
        }


        //for (int i = 0; i < Gamepad.all.Count; i++)
        //{
        //    if (Gamepad.all[i].buttonSouth.isPressed)
        //    {
        //        NextOption();
        //    }
        //    else if (Gamepad.all[i].buttonEast.isPressed)
        //    {
        //        BackOption();
        //    }
        //}


        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    NextOption();
        //}
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    BackOption();
        //}
    }
    //public void NextOption()
    //{
    
        
    //    tempGO = list[selectedSkin];
    //    selectedSkin = selectedSkin + 1;
    //    tempGO.SetActive(true);
    //}
    //public void BackOption()
    //{
    //    //tempGO.SetActive(false);
    //    selectedSkin = selectedSkin - 1;
    //    tempGO = list[selectedSkin];
        
    //}
}
