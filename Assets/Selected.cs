using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Selected : LevelSelectSelection
{

    LevelScroll levelScroll;

    private void Start()
    {

        levelScroll = transform.parent.parent.GetComponent<LevelScroll>();

    }

    public override void OnActivation()
    {
        buttonPrompt.text = "Press B/Circle to Deselect";
        for (int i = 0; i < outline.transform.childCount-1; i++)
        {
            if (outline.transform.GetChild(i).GetComponent<Image>())
            {
                outline.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
            
        }
    }

    void OnDeactivation()
    {
        buttonPrompt.text = "Press A/X to Select";
        for (int i = 0; i < outline.transform.childCount-1; i++)
        {
            if (outline.transform.GetChild(i).GetComponent<Image>())
            {
                outline.transform.GetChild(i).GetComponent<Image>().color = Color.grey;
            }
        }
    }

    private void Update()
    {
        if (activated)
        {
            inputManager.canScroll = false;

            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.left.wasPressedThisFrame)
                {
                    levelScroll.ScrollLeft();
                }
                else if (Gamepad.all[i].dpad.right.wasPressedThisFrame)
                {
                    levelScroll.ScrollRight();
                }

                if (Gamepad.all[i].buttonEast.wasPressedThisFrame)
                {
                    OnDeactivation();
                    inputManager.startBackTimer = true;
                    activated = false;
                    inputManager.canScroll = true;
                }
            }
        }
    }

}
