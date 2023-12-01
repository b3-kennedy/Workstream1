using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    public TMP_Text[] scores= new TMP_Text[8];
    void Start()
    {
        
    }
      
    // Update is called once per frame
    void Update()
    {
        for (int i =0; i < 8; i++)
        {
            TMP_Text score = scores[i];
            int sc = int.Parse(score.text);
            if(sc > 300)
            {
                Debug.Log("player #" + i + "has won");
            }
        }

    }
}
