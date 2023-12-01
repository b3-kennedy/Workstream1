using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    public ScoreUIController scoreUIController;
    public GameObject scene1;
    public GameObject scene2;
    public AudioSource gameAudio;
    public AudioSource endAudio;

    public TMP_Text EndMessage;
    void Start()
    {
      
        scoreUIController.OnEndGame += LoadEndGameScene;
    }

    private void LoadEndGameScene()
    {
        Debug.Log("game over scene load");
        EndMessage.text = scoreUIController.endGameMsg;
        scene2.SetActive(true); 
        scene1.SetActive(false);
        gameAudio.Stop();
        endAudio.Play();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void AddToCars(){
       
    }
}
