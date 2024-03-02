using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenuButton : MenuButton
{
    public override void Activate()
    {
        SceneManager.LoadScene(3);
    }
}
