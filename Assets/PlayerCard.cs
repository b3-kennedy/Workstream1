using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCard : MonoBehaviour
{
    public int cardIndex;
    public bool cardJoined;
    public int padIndex;
    public PlayerWithController.ControllerSide side;
    SelectScreenManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = transform.parent.gameObject.GetComponent<SelectScreenManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(side == PlayerWithController.ControllerSide.Left)
        {
            if (Gamepad.all[padIndex].leftShoulder.wasPressedThisFrame && cardJoined)
            {
                Leave();
            }
        }
        else if(side == PlayerWithController.ControllerSide.Right)
        {
            if (Gamepad.all[padIndex].rightShoulder.wasPressedThisFrame && cardJoined)
            {
                Leave();
            }
        }

    }

    void Leave()
    {
        manager.joinedIndex.Remove(cardIndex);
        cardJoined = false;
        foreach (var player in PlayerControllerManager.Instance.players)
        {
            if(player.card == gameObject)
            {
                player.joined = false;
                player.card = null;
            }
        }

        manager.index = 0;
        
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        if (side == PlayerWithController.ControllerSide.Left)
        {
            PlayerControllerManager.Instance.controllers[padIndex].left = false;
        }
        else if (side == PlayerWithController.ControllerSide.Right)
        {
            PlayerControllerManager.Instance.controllers[padIndex].right = false;
        }
    }
}
