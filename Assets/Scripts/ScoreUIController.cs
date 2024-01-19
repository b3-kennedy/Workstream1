using System;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    public TMP_Text[] scoresTxt = new TMP_Text[8];
    public event Action OnEndGame;

    public string endGameMsg;
    public int[] scores;

    public int winLimit;

    public TMP_Text timerTxt;

    public float TimeLeft;
    public bool TimerOn = false;
    private void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                endGameMsg = "Time Is Up!";
                TimeLeft = 0;
                TimerOn = false;
                OnEndGame?.Invoke();
            }
        }
        for (int i = 0; i < 8; i++)
        {
            TMP_Text score = scoresTxt[i];
            scores[i] = int.Parse(score.text);
            if (scores[i] > 20000)
            {
                endGameMsg = "player #" + (i + 1) + " has won with " + scores[i] + " points!!";
                Debug.Log(endGameMsg);
                OnEndGame?.Invoke();
            }
        }

    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        if (minutes == 0 && seconds < 30) { timerTxt.color = Color.red; } else { timerTxt.color = Color.white; }

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
