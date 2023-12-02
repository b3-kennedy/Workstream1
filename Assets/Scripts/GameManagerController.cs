using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    public ScoreUIController scoreUIController;
    public GameObject mainScene;
    public GameObject endScene;
    public GameObject startScene;

    public AudioSource gameAudio;
    public AudioSource endAudio;

    public TMP_Text EndMessage;

    public PlayerControls playerControls;

    public TMP_Text[] scoresTxt = new TMP_Text[8];

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Restart.action.performed += ctx => RestartGame();
        playerControls.Start.action.performed += ctx => StartGame();


    }
    private void OnDisable() {
        playerControls.Enable();
        playerControls.Restart.action.performed -= ctx => RestartGame();
        playerControls.Start.action.performed -= ctx => StartGame();
    }

    private void StartGame()
    {
      
        mainScene.SetActive(true);
        startScene.SetActive(false);
    }

    void Start()
    {

        scoreUIController.OnEndGame += LoadEndGameScene;
        endScene.SetActive(false);
        mainScene.SetActive(false);
        startScene.SetActive(true);

    }

    private void LoadEndGameScene()
    {
        Debug.Log("game over scene load");
        EndMessage.text = scoreUIController.endGameMsg;
        for (int i = 7; i >= 0; i--)
        {
            scoresTxt[i].text = scoreUIController.scoresTxt[i].text;
        }
        endScene.SetActive(true); 
        mainScene.SetActive(false);
        gameAudio.Stop();
        endAudio.Play();


        


    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void RestartGame(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        StartGame();
    }
}
