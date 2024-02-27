using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class ColourMaterialPair
{
    public Color color;
    public Material material;
}

public class SelectScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject collum;
    public List<GameObject> list = new List<GameObject>();
    private int selectedSkin = 0;
    private GameObject tempGO;
    public GameObject startText;

    public ColourMaterialPair[] availableColours;

    public int index = 0;


    private void Start()
    {

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            PlayerControllerManager.Instance.controllers.Add(new Controller());
        }

        for (int i = 0; i < Gamepad.all.Count * 2; i++) 
        {
            PlayerControllerManager.Instance.players.Add(new PlayerWithController());
            //if( i % 2 != 0)
            //{
            //    PlayerControllerManager.Instance.players[i].controllerSide = PlayerWithController.ControllerSide.Right;
            //}
        }

        for(int i = 0; transform.childCount > 0; i++)
        {
            availableColours[i].color = transform.GetChild(i).GetComponent<Image>().color;
            //PlayerControllerManager.Instance.players[i].selectedMaterial = availableColours[i].material;
        }

        
        tempGO = collum;
    }

    void PlayerSetup(int i, PlayerWithController player, Controller controller, PlayerWithController.ControllerSide side)
    {

    }
    
    void Join(int i, PlayerWithController player, Controller controller)
    {
        

        if (Gamepad.all[i].buttonSouth.wasPressedThisFrame && !player.joined && !controller.right)
        {

            Debug.Log(i);
            PlayerSetup(i, player, controller, PlayerWithController.ControllerSide.Right);
            player.joined = true;
            player.card = list[index];
            player.card.transform.GetChild(2).gameObject.SetActive(false);
            //player.card.SetActive(true);
            player.controllerSide = PlayerWithController.ControllerSide.Right;
            controller.right = true;
            controller.playerCard = player.card;
            player.selectedColour = player.card.GetComponent<Image>().color;
            player.controllerIndex = i;
            player.playerNumber = index;
            player.card.transform.GetChild(0).gameObject.SetActive(true);
            player.card.transform.GetChild(3).gameObject.SetActive(true);
            player.card.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Press X/Square button to change colour";
            player.card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "P" + (player.playerNumber + 1).ToString();
            player.pad = Gamepad.all[i];
            Debug.Log(player.pad);
            //NextOption();
            if (index < PlayerControllerManager.Instance.players.Count)
            {
                index++;
            }
            
        }

        else if (Gamepad.all[i].dpad.down.wasPressedThisFrame && !player.joined && !controller.left)
        {
            player.joined = true;
            player.card = list[index];
            player.card.transform.GetChild(2).gameObject.SetActive(false);
            //player.card.SetActive(true);
            player.controllerSide = PlayerWithController.ControllerSide.Left;
            controller.left = true;
            controller.playerCard = player.card;
            player.selectedColour = player.card.GetComponent<Image>().color;
            player.controllerIndex = i;
            player.playerNumber = index;
            player.card.transform.GetChild(0).gameObject.SetActive(true);
            player.card.transform.GetChild(3).gameObject.SetActive(true);
            player.card.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Press Left Dpad button to change colour";
            player.card.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "P" + (player.playerNumber + 1).ToString();
            player.pad = Gamepad.all[i];
            Debug.Log(player.pad);
            //NextOption();
            if (index < PlayerControllerManager.Instance.players.Count)
            {
                index++;
            }
        }
    }

    void Leave(int i , Controller controller)
    {
        if (Gamepad.all[i].dpad.right.wasPressedThisFrame)
        {
            foreach (var player in PlayerControllerManager.Instance.players)
            {
                if(controller.left && (player.controllerIndex == i && player.controllerSide == PlayerWithController.ControllerSide.Left))
                {
                    player.joined = false;
                    player.card.transform.GetChild(0).gameObject.SetActive(false);
                    player.card.transform.GetChild(3).gameObject.SetActive(false);
                    player.card.transform.GetChild(2).gameObject.SetActive(true);
                    controller.left = false;
                    index--;

                }
            }
            
            //BackOption();
            
        }


        else if (Gamepad.all[i].buttonEast.wasPressedThisFrame)
        {
            foreach (var player in PlayerControllerManager.Instance.players)
            {
                if (controller.right && (player.controllerIndex == i && player.controllerSide == PlayerWithController.ControllerSide.Right))
                {
                    player.joined = false;
                    player.card.transform.GetChild(0).gameObject.SetActive(false);
                    player.card.transform.GetChild(3).gameObject.SetActive(false);
                    player.card.transform.GetChild(2).gameObject.SetActive(true);
                    controller.right = false;
                    index--;
                }
            }
            
            //BackOption();
            
        }
        
    }

    private void Update()
    {

        

        if(index < 0)
        {
            index = 0;
        }
        else if(index > 7)
        {
            index = 7;
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if(index < PlayerControllerManager.Instance.players.Count)
            {
                Join(i, PlayerControllerManager.Instance.players[index], PlayerControllerManager.Instance.controllers[i]);
            }
            
            if(index > 0)
            {
                Leave(i, PlayerControllerManager.Instance.controllers[i]);
            }
           
        }

        if(index > 0)
        {
            startText.SetActive(true);
        }
        else
        {
            startText.SetActive(false);
        }


        if (Gamepad.all[0].buttonNorth.wasPressedThisFrame && index > 0)
        {
            SceneManager.LoadScene(2);
        }

        foreach (var player in PlayerControllerManager.Instance.players)
        {
            if (player.joined)
            {
                if (player.colourIndex > availableColours.Length)
                {
                    player.colourIndex = 0;
                }

                if (player.controllerSide == PlayerWithController.ControllerSide.Right)
                {
                    if (player.pad.buttonWest.wasPressedThisFrame)
                    {
                        player.colourIndex++;
                        if(player.colourIndex > availableColours.Length-1)
                        {
                            player.colourIndex = 0;
                        }
                        player.card.GetComponent<Image>().color = availableColours[player.colourIndex].color;
                        player.selectedColour = availableColours[player.colourIndex].color;
                        player.selectedMaterial = availableColours[player.colourIndex].material;

                    }
                }
                else if (player.controllerSide == PlayerWithController.ControllerSide.Left)
                {
                    if (player.pad.dpad.left.wasPressedThisFrame)
                    {
                        player.colourIndex++;
                        if (player.colourIndex > availableColours.Length-1)
                        {
                            player.colourIndex = 0;
                        }
                        player.card.GetComponent<Image>().color = availableColours[player.colourIndex].color;
                        player.selectedColour = availableColours[player.colourIndex].color;
                        player.selectedMaterial = availableColours[player.colourIndex].material;
                    }
                }
            }
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