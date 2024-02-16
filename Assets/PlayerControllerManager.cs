using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class PlayerWithController
{
    public int controllerIndex;
    public enum ControllerSide {Left, Right };
    public ControllerSide controllerSide;
    public bool joined;
    public GameObject card;
    public int playerNumber;
}


public class PlayerControllerManager : MonoBehaviour
{
    public List<PlayerWithController> players;
    public static PlayerControllerManager Instance;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
