using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    CarController car1;
    P2Control car2;

    public int p1Score;
    public int p2Score;

    public bool over = false;
    // Start is called before the first frame update
    void Start()
    {
        car1 = GameObject.FindObjectOfType<CarController>();
        car2 = GameObject.FindObjectOfType<P2Control>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        if (!over)
        {
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(200, 450, 150, 20), "Player 1 Score : " + p1Score);
            GUI.Label(new Rect(600, 450, 150, 20), "Player 2 Score : " + p2Score);

            if (p1Score + p2Score >= 29)
            {
                over = true;
            }

        }
        
        else if( over )
        {
            GUI.contentColor = Color.white;
            car1.disable = true;
            car2.disable = true;

            if (p1Score > p2Score )
            {
                GUI.Label(new Rect(400, 450, 150, 20), "Player 1 WINNER WITH : " + p1Score);

            }
            else
            {
                GUI.Label(new Rect(400, 450, 150, 20), "Player 2 WINNER WITH : " + p2Score);

            }
        }
    }
}
