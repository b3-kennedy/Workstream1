using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    public TMP_Text[] scores= new TMP_Text[8];
    public event Action OnEndGame ;

    public string endGameMsg;
    void Update()
    {
        for (int i =0; i < 8; i++)
        {
            TMP_Text score = scores[i];
            int sc = int.Parse(score.text);
            if(sc > 100)
            {
                endGameMsg = "player #" + (i + 1) + " has won with " + sc + " points!!";
                Debug.Log(endGameMsg);
                OnEndGame?.Invoke();
            }
        }

    }
}
