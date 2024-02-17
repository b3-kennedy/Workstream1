using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    Gamepad pad;
    public GameObject[] buttons;
    int buttonIndex = 0;
    bool moved;
    float timer;


    private void Start()
    {
        if (Gamepad.all.Count > 0)
        {
            pad = Gamepad.all[0];
        }
    }

    void Input()
    {
        float vertical = pad.leftStick.y.value;

        if ((vertical < 0 && !moved) || (pad.dpad.down.isPressed && !moved))
        {
            buttonIndex++;
            if(buttonIndex > buttons.Length - 1)
            {
                buttonIndex = 0;
            }
            moved = true;
        }
        else if ((vertical > 0 && !moved) || (pad.dpad.up.isPressed && !moved))
        {
            buttonIndex--;
            if(buttonIndex < 0)
            {
                buttonIndex = buttons.Length - 1;
            }
            moved = true;
        }
    }

    void Selection()
    {
        foreach (GameObject button in buttons)
        {
            if (button == buttons[buttonIndex])
            {
                button.GetComponent<MenuButton>().selected = true;
            }
            else
            {
                button.GetComponent<MenuButton>().selected = false;
            }
        }
    }

    void Timer()
    {
        if (moved)
        {
            timer += Time.deltaTime;
            if (timer >= 0.25f)
            {
                moved = false;
                timer = 0;
            }
        }
    }


    private void Update()
    {
        if(pad != null)
        {

            Input();
            Selection();
            Timer();

            if (pad.aButton.isPressed)
            {
                buttons[buttonIndex].GetComponent<MenuButton>().Activate();
            }
        }
    }


    public void startScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
