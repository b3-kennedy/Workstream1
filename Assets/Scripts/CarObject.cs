using UnityEngine;

public class CarObject
{
    public int playerIndex;
    public int carIndex;
    public Color carColor;   
    public string carName;   
    public GameObject carObject;
    private bool isParked=false;
    private float parkingScore;

    public CarObject(int index, Color color, string name,GameObject carObj)
    {
        carIndex = index;
        carColor = color;
        carName = name;
        carObject = carObj;   
         }
        public void SetIsParked()
        {
            isParked=true;
        }
        public void SetParkingScore(float score)
        {
            parkingScore = score;
        }
        
}