using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    public TMP_Text[] scoresTxt= new TMP_Text[8];
    public event Action OnEndGame ;

    public string endGameMsg;
    public int[] scores;
    void Update()
    {
        for (int i =0; i < 8; i++)
        {
            TMP_Text score = scoresTxt[i];
            scores[i] = int.Parse(score.text);
            if(scores[i] > 100)
            {
                endGameMsg = "player #" + (i + 1) + " has won with " + scores[i] + " points!!";
                Debug.Log(endGameMsg);
                OnEndGame?.Invoke();
            }
        }

    }
}
