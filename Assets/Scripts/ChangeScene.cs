using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void startScene()
    {
        SceneManager.LoadScene("w");
    }

    public void quit()
    {
        Application.Quit();
    }
}
